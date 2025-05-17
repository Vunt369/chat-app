using ChatApp.Configurations;
using ChatApp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChatApp.Attributes
{
    public class BlacklistedTokenAttribute : Attribute, IAsyncActionFilter
    {
        public BlacklistedTokenAttribute()
        {
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
            var token = GetTokenFromRequest(context.HttpContext.Request);
            var cacheResponse = await cacheService.IsTokenBlacklistedAsync(token);
            if (cacheResponse)
            {
                var contentResult = new ContentResult()
                {
                    Content = "Token has been revoked. Please log in again.",
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                context.Result = contentResult;
                return;
            }
            await next();
        }

        private static string GetTokenFromRequest(HttpRequest request)
        {
            var authHeader = request.Headers["Authorization"].FirstOrDefault();
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                return authHeader.Substring("Bearer ".Length).Trim();
            }
            return null;
        }
    }
}