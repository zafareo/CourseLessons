using LazyCache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace Talkie.Attributes
{
    public class LazyCacheAttribute : ActionFilterAttribute
    {
        private static IAppCache _cache;
        private string _Key = "MyKey";
        private bool _IsCached = false;
        private readonly int _SlidingTime;
        private readonly int _AbsoluteExpirationRelativeToNow;
        public LazyCacheAttribute( int SlidingTime, int AbsoluteExpirationRelativeToNow)
        {
            _AbsoluteExpirationRelativeToNow = AbsoluteExpirationRelativeToNow;
            _SlidingTime = SlidingTime;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _cache = context.HttpContext.RequestServices.GetRequiredService<IAppCache>();
            _Key = context.HttpContext.Request.Path;
            _IsCached = _cache.TryGetValue(_Key, out IActionResult res);
                              
            if (_IsCached)
            {
                await base.OnActionExecutionAsync(context, next);
            }
            else
            {
                context.Result = res;
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_AbsoluteExpirationRelativeToNow),
                SlidingExpiration = TimeSpan.FromSeconds(_SlidingTime)

            };
            _cache.Add(_Key, context.Result, options);

            base.OnActionExecuted(context);
        }

        //public override void OnResultExecuted(ResultExecutedContext context)
        //{
        //    if (_IsCached)
        //    {   
        //        var options = new MemoryCacheEntryOptions
        //        {
        //            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_AbsoluteExpirationRelativeToNow),
        //            SlidingExpiration = TimeSpan.FromSeconds(_SlidingTime)
                
        //        };
        //        _cache.Add(_Key, context.Result, options);
        //    }
                    
        //    base.OnResultExecuted(context);
        //}
    }
}
