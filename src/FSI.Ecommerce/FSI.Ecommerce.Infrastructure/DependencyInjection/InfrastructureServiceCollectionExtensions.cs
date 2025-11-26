using FSI.Ecommerce.Application.Interfaces.Services;
using FSI.Ecommerce.Domain.Interfaces;
using FSI.Ecommerce.Infrastructure.Persistence;
using FSI.Ecommerce.Infrastructure.Repositories;
using FSI.Ecommerce.Infrastructure.Security;
using FSI.Ecommerce.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FSI.Ecommerce.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // -------------------------
            // DbContext / MySQL
            // -------------------------
            var connectionString = configuration.GetConnectionString("EcommerceDatabase");

            services.AddDbContext<EcommerceDbContext>(options =>
            {
                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString));
            });

            // -------------------------
            // JWT Settings (SEM Bind, SEM IConfigurationSection overload)
            // -------------------------
            var jwtSection = configuration.GetSection(JwtSettings.SectionName);

            services.Configure<JwtSettings>(options =>
            {
                options.Issuer = jwtSection["Issuer"] ?? string.Empty;
                options.Audience = jwtSection["Audience"] ?? string.Empty;
                options.SecretKey = jwtSection["SecretKey"] ?? string.Empty;

                // tenta converter ExpirationMinutes com fallback
                if (int.TryParse(jwtSection["ExpirationMinutes"], out var expiration))
                    options.ExpirationMinutes = expiration;
                else
                    options.ExpirationMinutes = 60;
            });

            // -------------------------
            // Serviços de segurança
            // -------------------------
            services.AddScoped<ITokenService, TokenService>();

            // -------------------------
            // Unit of Work
            // -------------------------
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            // -------------------------
            // Repositórios
            // -------------------------
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentTransactionRepository, PaymentTransactionRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            return services;
        }
    }
}
