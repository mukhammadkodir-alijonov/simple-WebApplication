namespace simple_Web.Configurations.LayerConfigurations
{
    public class ServiceLayerConfiguration
    {
            services.AddHttpContextAccessor();
            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(MappingConfiguration));
    }
}
