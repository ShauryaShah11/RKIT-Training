using FinalDemo.Helpers;
using FinalDemo.Interfaces;
using FinalDemo.Services;

namespace FinalDemo.ExtensionMethods
{
    public static class ServiceRegistrationExtensions
    {
        /// <summary>
        /// Registers the application's services into the IServiceCollection.
        /// This method adds scoped services for business logic, including order, stock, user, and stock price history services.
        /// It also adds a singleton service for the OrmLite database factory.
        /// </summary>
        /// <param name="services">The IServiceCollection to which the services are added.</param>
        /// <param name="configuration">The application's configuration, used to initialize the OrmLiteDbFactory.</param>
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Registering business logic services with scoped lifetime
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IStockPriceHistoryService, StockPriceHistoryService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IUserService, UserService>();

            services.AddSingleton<JwtHelper>();

            // Registering the OrmLiteDbFactory as a singleton with the configuration
            services.AddSingleton<IOrmLiteDbFactory, OrmLiteDbFactory>(sp => new OrmLiteDbFactory(configuration));
        }
    }
}
