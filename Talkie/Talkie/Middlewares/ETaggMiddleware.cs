﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Talkie.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ETaggMiddleware
    {
        private readonly RequestDelegate _next;

        public ETaggMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Method == "GET")
            {
                var response = httpContext.Response;
                var originalStream = response.Body;

                using var ms = new MemoryStream();
                response.Body = ms;

                await _next(httpContext);

                if (IsEtagSupported(response))
                {
                    string checksum = CalculateHash(ms);

                    if (httpContext.Request.Headers.TryGetValue(HeaderNames.IfNoneMatch, out var etag) && checksum == etag)
                    {
                        response.StatusCode = StatusCodes.Status304NotModified;
                        return;
                    }
                    response.Headers[HeaderNames.ETag] = checksum;
                }

                ms.Position = 0;
                await ms.CopyToAsync(originalStream);
            }
            else await _next(httpContext);
        }

        private static bool IsEtagSupported(HttpResponse response)
        {
            if (response.StatusCode != StatusCodes.Status200OK)
                return false;

           
            if (response.Body.Length > 20 * 1024)
                return false;

            if (response.Headers.ContainsKey(HeaderNames.ETag))
                return false;

            return true;
        }

        private static string CalculateHash(MemoryStream ms)
        {
            string checksum = "";

            using (var algo = SHA1.Create())
            {
                ms.Position = 0;
                byte[] bytes = algo.ComputeHash(ms);
                checksum = $"\"{WebEncoders.Base64UrlEncode(bytes)}\"";
            }

            return checksum;
        }
    }

    public static class ETaggMiddlewareExtensions
    {
        public static IApplicationBuilder UseTaggMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ETaggMiddleware>();
        }
    }
}
