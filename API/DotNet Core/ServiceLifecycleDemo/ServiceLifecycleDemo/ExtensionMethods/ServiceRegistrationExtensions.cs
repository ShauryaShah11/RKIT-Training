using ServiceLifecycleDemo.Interfaces;
using ServiceLifecycleDemo.Services;

namespace ServiceLifecycleDemo.ExtensionMethods
{
    /// <summary>
    /// Provides extension methods for registering application services in the dependency injection container.
    /// </summary>
    public static class ServiceRegistrationExtensions
    {
        /// <summary>
        /// Adds application services to the dependency injection container.
        /// Registers Scoped, Transient, and Singleton services.
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to.</param>
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IScopedService, ScopedService>();
            services.AddTransient<ITransientService, TransientService>();
            services.AddSingleton<ISingletonService, SingletonService>();
        }

        //// Uncomment and use the following methods if needed:

        ///// <summary>
        ///// Registers logging services in the application.
        ///// </summary>
        ///// <param name="services">The IServiceCollection to add services to.</param>
        //public static void AddLoggingServices(this IServiceCollection services)
        //{
        //    services.AddSingleton<ILogger, ConsoleLogger>();
        //}

        ///// <summary>
        ///// Registers DbContext for Entity Framework with a provided connection string.
        ///// </summary>
        ///// <param name="services">The IServiceCollection to add services to.</param>
        ///// <param name="connectionString">The database connection string.</param>
        //public static void AddDbContextServices(this IServiceCollection services, string connectionString)
        //{
        //    services.AddDbContext<MyDbContext>(options => options.UseSqlServer(connectionString));
        //}
    }
}
