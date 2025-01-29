using FinalDemo.Interfaces;
using FinalDemo.Services;

namespace FinalDemo.ExtensionMethods
{
    public static class ServiceRegistrationExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IStockPriceHistoryService, StockPriceHistoryService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IOrmLiteDbFactory, OrmLiteDbFactory>();
        }
    }
}
