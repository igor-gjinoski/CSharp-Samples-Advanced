using AssemblyScanning.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AssemblyScanning.IoC.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IScopedService, ScopedService>();
        }
    }
}
