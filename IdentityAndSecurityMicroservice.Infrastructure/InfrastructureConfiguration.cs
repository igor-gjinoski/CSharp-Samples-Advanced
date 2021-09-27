using IdentityAndSecurityMicroservice.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityAndSecurityMicroservice.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddDatabase(configuration)
                .AddIdentity()
                .AddServices();
        }

        private static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            return services;
        }


        private static IServiceCollection AddIdentity(
            this IServiceCollection services)
        {
            return services;
        }


        private static IServiceCollection AddServices(
            this IServiceCollection services)
        {
            return services.AddScoped<IIdentityService, IdentityService>();
        }
    }
}
