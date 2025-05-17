using ChatApp.Configurations;
using ChatApp.Infrastructure.Services;
using StackExchange.Redis;


namespace ChatApp.Installers
{
    public class CacheInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            var redisConfiguration = new RedisConfiguration();
            configuration.GetSection("RedisConfigurations").Bind(redisConfiguration);
            services.AddSingleton(redisConfiguration);
            if (!redisConfiguration.Enable) return;

            services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisConfiguration.ConnectionString));
            services.AddStackExchangeRedisCache(opt => opt.Configuration = redisConfiguration.ConnectionString);

            services.AddScoped<IRedisService, RedisService>();
        }
    }
}
