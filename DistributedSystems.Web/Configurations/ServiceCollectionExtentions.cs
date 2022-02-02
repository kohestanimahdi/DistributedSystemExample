using Microsoft.Extensions.DependencyInjection;

namespace DistributedSystems.Web.Configurations
{
    public static class ServiceCollectionExtentions
    {
        public static void AddRedisCache(this IServiceCollection services, string connectionString, string instanceName)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = connectionString;
                options.InstanceName = instanceName;
            });
        }
    }
}
