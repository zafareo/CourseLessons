
using Application;
using GapKo_p;
using Infrastructure;
using System.Threading.RateLimiting;
using Talkie.Middlewares;

namespace Talkie
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddInfrastructureService(builder.Configuration);
            builder.Services.AddApplicationService();
            builder.Services.AddWebUIService();
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddResponseCaching();
            #region FixedLimiter
            //builder.Services.AddRateLimiter(options =>
            //{
            //    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            //    {
            //        return RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpContext.Request.Headers.Host.ToString(), partition =>
            //            new FixedWindowRateLimiterOptions
            //            {
            //                PermitLimit = 5,
            //                AutoReplenishment = true,
            //                Window = TimeSpan.FromSeconds(10),
            //                QueueLimit = 5,
            //                QueueProcessingOrder = QueueProcessingOrder.NewestFirst
            //            });
            //    });

            //    options.OnRejected = async (context, token) =>
            //    {
            //        context.HttpContext.Response.StatusCode = 429;
            //        await context.HttpContext.Response.WriteAsync("Too many requests. Please try later again... ", cancellationToken: token);
            //    };
            //});
            #endregion
            #region Sliding Limiter
            //builder.Services.AddRateLimiter(options =>
            //{
            //    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            //    {
            //        return RateLimitPartition.GetSlidingWindowLimiter(partitionKey: httpContext.Request.Headers.Host.ToString(), partition =>
            //            new SlidingWindowRateLimiterOptions
            //            {
            //                PermitLimit = 10,
            //                Window = TimeSpan.FromSeconds(20),  
            //                SegmentsPerWindow = 2,
            //                QueueLimit = 10,
            //                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            //                AutoReplenishment = true
            //            });
            //    });

            //    options.OnRejected = async (context, token) =>
            //    {
            //        context.HttpContext.Response.StatusCode = 429;
            //        await context.HttpContext.Response.WriteAsync("Too many requests. Please try later again... ", cancellationToken: token);
            //    };
            //});
            #endregion
            #region Concurrent Limiter
            //builder.Services.AddRateLimiter(options =>
            //{
            //    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            //    {
            //        return RateLimitPartition.GetConcurrencyLimiter(partitionKey: httpContext.Request.Headers.Host.ToString(), partition =>
            //            new ConcurrencyLimiterOptions
            //            {                            
            //                PermitLimit = 10,              
            //                QueueLimit = 10,
            //                QueueProcessingOrder = QueueProcessingOrder.OldestFirst
            //            });
            //    });

            //    options.OnRejected = async (context, token) =>
            //    {
            //        context.HttpContext.Response.StatusCode = 429;
            //        await context.HttpContext.Response.WriteAsync("Too many requests. Please try later again... ", cancellationToken: token);
            //    };
            //});
            #endregion
            #region TokenBucket Limiter
            //builder.Services.AddRateLimiter(options =>
            //{
            //    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            //    {
            //        return RateLimitPartition.GetTokenBucketLimiter(partitionKey: httpContext.Request.Headers.Host.ToString(), partition =>
            //            new TokenBucketRateLimiterOptions
            //            {
            //                TokenLimit = 4,
            //                TokensPerPeriod = 3,
            //                ReplenishmentPeriod = TimeSpan.FromSeconds(5),
            //                AutoReplenishment = true,
            //                QueueLimit = 4,
            //                QueueProcessingOrder = QueueProcessingOrder.OldestFirst
            //            });
            //    });

            //    options.OnRejected = async (context, token) =>
            //    {
            //        context.HttpContext.Response.StatusCode = 429;
            //        await context.HttpContext.Response.WriteAsync("Too many requests. Please try later again... ", cancellationToken: token);
            //    };
            //});
            #endregion

            //builder.Services.AddOutputCache();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.DisplayRequestDuration();
                });
            }

            app.UseHttpsRedirection();          
            app.UseAuthorization();
            app.UseGetRequestMiddleware();
            app.UseTaggMiddleware();
            app.UseResponseCaching();
            app.UseRateLimiter();            
            //  app.UseOutputCache();
            app.MapControllers();

            app.Run();
        }
    }
}