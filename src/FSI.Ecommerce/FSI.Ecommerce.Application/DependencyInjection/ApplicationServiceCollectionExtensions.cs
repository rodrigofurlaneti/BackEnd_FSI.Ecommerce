using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FSI.Ecommerce.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(c => c.Where(t => t.Name.EndsWith("AppService")))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}
