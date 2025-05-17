
using ChatApp.Configurations;
using ChatApp.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Installers
{
    public class DatabaseInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            var sqlConfiguration = new SqlConfiguration();
            configuration.GetSection("Databases:SqlConfigurations").Bind(sqlConfiguration);
            services.AddSingleton(sqlConfiguration);

            //DI for SQL
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(sqlConfiguration.ConnectionString, b => b.MigrationsAssembly("ChatApp"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }, ServiceLifetime.Transient);
        }
      
    }
}
