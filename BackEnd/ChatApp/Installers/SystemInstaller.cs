using AutoMapper;
using ChatApp.Configurations;
using ChatApp.Core.Data;
using ChatApp.Core.Models;
using ChatApp.Infrastructure.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

namespace ChatApp.Installers
{
    public class SystemInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Nhập 'Bearer' [space] và sau đó là token của bạn. \n\nVí dụ: \"Bearer abcdefgh12345\""
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        }, new string[] {}
                    }
                });
            });
            services.AddCors(option =>
            {
                option.AddPolicy("AllowFrontend",
                policy => policy.WithOrigins("http://localhost:5173")  // Thêm URL frontend của bạn
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials()
                       .SetIsOriginAllowed(origin => true));
            });

            services.AddControllers();
            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            var jwtConfig = configuration.GetSection("Jwt").Get<JwtConfiguration>();
            services.Configure<JwtConfiguration>(configuration.GetSection("Jwt"));

            //DI AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mapping());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}