using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;


namespace ChatApp.Infrastructure.Services
{
    public interface IRedisService
    {
        Task SetCacheAsync<T>(string cacheKey, T response, TimeSpan timeOut);
        Task<T> GetCacheAsync<T>(string cacheKey);
        Task RemoveCacheAsync(string cacheKey);
        Task<bool> IsTokenBlacklistedAsync(string token);
        Task<bool> ExistsAsync(string key);

        Task AddToSetAsync<T>(string key, T value);
        Task<bool> IsMemberOfSetAsync<T>(string key, T value);
        Task<IEnumerable<string>> GetSetAsync(string key);
        Task RemoveFromSetAsync<T>(string key, T value);
    }
    public class RedisService : IRedisService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        public RedisService(IDistributedCache distributedCache,
                            IConnectionMultiplexer connectionMultiplexer)
        {
            _distributedCache = distributedCache;
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task<T> GetCacheAsync<T>(string cacheKey)
        {
            var result = await _distributedCache.GetStringAsync(cacheKey);
            return string.IsNullOrEmpty(result) ? default(T) : JsonConvert.DeserializeObject<T>(result);
        }

        public async Task SetCacheAsync<T>(string cacheKey, T response, TimeSpan timeOut)
        {
            if (response == null) return;
            
            var serializerReponse = JsonConvert.SerializeObject(response, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            await _distributedCache.SetStringAsync(cacheKey, serializerReponse, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = timeOut
            });
        }

        public async Task<bool> IsTokenBlacklistedAsync(string token)
        {
            return await _connectionMultiplexer.GetDatabase().KeyExistsAsync($"blacklist:{token}");
        }

        public async Task RemoveCacheAsync(string cacheKey)
        {
     
            await _distributedCache.RemoveAsync(cacheKey);

            //await _distributedCache.RemoveAsync(cacheKey, connectionId);
        }

        public async Task<bool> ExistsAsync(string key)
        {
            return await _connectionMultiplexer.GetDatabase().KeyExistsAsync(key);
        }

        public async Task AddToSetAsync<T>(string key, T value)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(value);
            await _connectionMultiplexer.GetDatabase().SetAddAsync(key, json);
        }

        public async Task<bool> IsMemberOfSetAsync<T>(string key, T value)
        {
            var json = JsonConvert.SerializeObject(value);
            return await _connectionMultiplexer.GetDatabase().SetContainsAsync(key, json);
        }

        public async Task<IEnumerable<string>> GetSetAsync(string key)
        {
            var redisValues = await _connectionMultiplexer.GetDatabase().SetMembersAsync(key);
            return redisValues
                .Select(x => x.ToString().Trim('"'))
                .Where(x => !string.IsNullOrEmpty(x));
        }


        public async Task RemoveFromSetAsync<T>(string key, T value)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(value);
            await _connectionMultiplexer.GetDatabase().SetRemoveAsync(key, json);
        }
    }
}
