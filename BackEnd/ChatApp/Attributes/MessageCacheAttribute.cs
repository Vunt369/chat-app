
namespace ChatApp.Attributes
{
    public class MessageCacheAttribute : BaseCacheAttribute
    {
        public override string GetCacheKey(HttpContext context)
        {
            throw new NotImplementedException();
        }

        public override TimeSpan GetExpiration()
        {
            return TimeSpan.FromSeconds(60);
        }
    }
}
