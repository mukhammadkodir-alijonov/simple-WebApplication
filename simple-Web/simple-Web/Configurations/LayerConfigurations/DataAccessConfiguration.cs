using Microsoft.EntityFrameworkCore;
using simple_Web.DataAccess.DbContexts;
using simple_Web.DataAccess.Interfaces.Common;
using simple_Web.DataAccess.Repositories.Comman;

namespace simple_Web.Configurations.LayerConfigurations
{
    public static class ServiceLayerConfiguration
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IIdentityService, IdentityService>();

            //swagger auth
            //services.ConfigureSwaggerAuthorize();

            services.AddHttpContextAccessor();
            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(MappingConfiguration));
        }
    }
}
