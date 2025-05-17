
using ChatApp.Core.Hubs;
using ChatApp.Core.Implements;
using ChatApp.Core.Interfaces;
using ChatApp.Infrastructure.Services;

namespace ChatApp.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IChatHub, ChatHub>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IChatService, ChatService>();

        }
    }
}
