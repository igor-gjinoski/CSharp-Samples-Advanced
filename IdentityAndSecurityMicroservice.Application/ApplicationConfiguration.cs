using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityAndSecurityMicroservice.Application
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApplicationSettings>(
                     configuration.GetSection(nameof(ApplicationSettings)),
                     options => options.BindNonPublicProperties = true);

            services.Configure<IdentityServerSettings>(
                     configuration.GetSection(nameof(IdentityServerSettings)));

            return services;
        }
    }
}
