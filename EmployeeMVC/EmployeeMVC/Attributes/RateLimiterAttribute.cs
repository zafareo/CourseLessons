using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace EmployeeMVC.Attributes
{
    public static class RateLimiterAttribute
    {
        public static IServiceCollection AddRateLimiterService(this IServiceCollection services)
        {
            services.AddRateLimiter(rateLimiterOptions =>
            {
                rateLimiterOptions.AddFixedWindowLimiter("SlidingLimiter", options =>
                {
                    options.PermitLimit = 10;
                    options.Window = TimeSpan.FromSeconds(10);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = 5;
                });
                //rateLimiterOptions.AddSlidingWindowLimiter("SlidingLimiter", opt =>
                //{

                //    opt.Window = TimeSpan.FromSeconds(1000);
                //    opt.PermitLimit = 3;
                //    opt.SegmentsPerWindow = 2;
                //    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                //    opt.QueueLimit = 0;

                //    opt.AutoReplenishment = true;


                //}).RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                //rateLimiterOptions.AddTokenBucketLimiter("TokenBucketLimiter", options =>
                //{
                //    options.TokenLimit = 100;
                //    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                //    options.QueueLimit = 5;
                //    options.ReplenishmentPeriod = TimeSpan.FromSeconds(10);
                //    options.TokensPerPeriod = 20;
                //    options.AutoReplenishment = true;
                //});
                //rateLimiterOptions.AddConcurrencyLimiter("ConcurrencyLimiter", options =>
                //{
                //    options.PermitLimit = 10;
                //    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                //    options.QueueLimit = 5;
                //});

            });
            return services;
        }
    }
}
