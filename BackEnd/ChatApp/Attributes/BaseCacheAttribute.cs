using Microsoft.AspNetCore.Mvc.Filters;

namespace ChatApp.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class BaseCacheAttribute : Attribute
    {
        public abstract string GetCacheKey(HttpContext context);
        public abstract TimeSpan GetExpiration();
    }
}
