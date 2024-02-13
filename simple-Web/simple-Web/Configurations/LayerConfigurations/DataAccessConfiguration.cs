using Microsoft.EntityFrameworkCore;
using simple_Web.DataAccess.DbContexts;
using simple_Web.DataAccess.Interfaces.Common;
using simple_Web.DataAccess.Repositories.Comman;

namespace simple_Web.Configurations.LayerConfigurations
{
    public static class DataAccessConfiguration
    {
        public static void ConfigureDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            string connectionString = configuration.GetConnectionString("DatabaseConnection")!;
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
