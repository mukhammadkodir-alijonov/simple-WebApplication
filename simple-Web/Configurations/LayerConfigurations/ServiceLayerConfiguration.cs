using simple_Web.DataAccess.Interfaces.Common;
using simple_Web.DataAccess.Repositories.Comman;
using simple_Web.Service.Interfaces;
using simple_Web.Service.Interfaces.Common;
using simple_Web.Service.Services.Common;
using simple_Web.Service.Services;

namespace simple_Web.Configurations.LayerConfigurations
{
    public static class ServiceLayerConfiguration
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAuthService, AuthService>();
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
