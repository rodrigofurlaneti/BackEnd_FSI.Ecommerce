using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FSI.Ecommerce.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(assembly);

            services.AddValidatorsFromAssembly(assembly);

            services.Scan(scan => scan
                .FromAssemblyOf<ApplicationServiceCollectionExtensions>()
                .AddClasses(c => c.Where(t =>
                    t.Name.EndsWith("AppService") &&
                    t.IsClass &&
                    !t.IsAbstract))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}