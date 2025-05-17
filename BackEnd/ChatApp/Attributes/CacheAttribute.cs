using ChatApp.Configurations;
using ChatApp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;

namespace ChatApp.Attributes
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _cacheDuration;
        public CacheAttribute(int cacheDuration)
        {
            _cacheDuration = cacheDuration;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheConfiguration = context.HttpContext.RequestServices.GetRequiredService<RedisConfiguration>();
            if (!cacheConfiguration.Enable)
            {
                await next();
                return;
            }
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IRedisService>();
            var cacheKey = GenerateCacheKey(context);
            var cachedResponse = await cacheService.GetCacheAsync<string>(cacheKey);
            if (!string.IsNullOrEmpty(cachedResponse))
            {
                context.Result = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                return; 
            }

            var executedContext = await next();
            if (executedContext.Result is ObjectResult objectResult)
            {
                await cacheService.SetCacheAsync(cacheKey, objectResult.Value, TimeSpan.FromSeconds(_cacheDuration));
            }
        }

        private string GenerateCacheKey(ActionExecutingContext context)
        {
            var key = $"{context.HttpContext.Request.Path}";
            foreach (var (keyName, value) in context.ActionArguments)
            {
                key += $":{keyName}={value}";
            }
            return key;
        }

        
    }
}
