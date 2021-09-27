using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AssemblyScanning.IoC
{
    public interface IInstaller
    {
        public int Order => -1;
        void AddServices(IServiceCollection services, IConfiguration configuration);
    }
}
