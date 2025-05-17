
namespace ChatApp.Installers
{
    public class SignalrInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSignalR(otp =>
            {
  
            });
        }
    }
}
