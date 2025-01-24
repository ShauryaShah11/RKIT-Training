using ServiceLifecycleDemo.Interfaces;
using ServiceLifecycleDemo.Services;

namespace ServiceLifecycleDemo.ExtensionMethods
{
    public static class ServiceRegistrationExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IScopedService, ScopedService>();
            services.AddTransient<ITransientService, TransientService>();
            services.AddSingleton<ISingletonService, SingletonService>();
        }

        //// Register logging services
        //public static void AddLoggingServices(this IServiceCollection services)
        //{
        //    services.AddSingleton<ILogger, ConsoleLogger>();
        //}

        //// Register DbContext (Entity Framework)
        //public static void AddDbContextServices(this IServiceCollection services, string connectionString)
        //{
        //    services.AddDbContext<MyDbContext>(options => options.UseSqlServer(connectionString));
        //}
    }
}
