using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
