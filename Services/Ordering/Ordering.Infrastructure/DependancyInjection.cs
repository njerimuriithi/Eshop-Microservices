
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddInfrastuctureServices(this IServiceCollection services,IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");
            return services;
        }
    }
}
