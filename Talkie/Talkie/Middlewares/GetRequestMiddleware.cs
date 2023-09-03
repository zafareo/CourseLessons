using Application.Common.Notifications;
using Application.MediatrEntities.Posts.Commands;
using MediatR;

namespace Talkie.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class GetRequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMediator _mediatr;

        public GetRequestMiddleware(RequestDelegate next, IMediator mediatr)
        {
            _next = next;
            _mediatr = mediatr;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path == "/api/Post/create")
            {
                var createdPost = await httpContext.Request.ReadFormAsync();
                CreatePostCommand createPostCommand = new()
                {
                    Name = createdPost["Name"],
                    Content = createdPost["Content"]

                };

                await _mediatr.Publish(new PostNotification { CreatePostCommand = createPostCommand });
            }
            await _next(httpContext);
        }
    }

    public static class GetRequestMiddlewareExtensions
    {
        public static IApplicationBuilder UseGetRequestMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GetRequestMiddleware>();
        }
    }
}
