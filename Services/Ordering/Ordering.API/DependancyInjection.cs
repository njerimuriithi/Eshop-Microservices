namespace Ordering.API
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
           
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            return app;
        }
    }
}
