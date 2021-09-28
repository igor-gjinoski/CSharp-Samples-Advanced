using AssemblyScanning.IoC;
using AssemblyScanning.Marker;
using AssemblyScanning.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AssemblyScanning
{
    class Program
    {
        static void Main()
        {
            IServiceCollection services = new ServiceCollection();
            IConfiguration configuration = new ConfigurationBuilder().Build();
            IServiceProvider serviceProvider = BuildServiceProvider(services, configuration);

            using var scope = serviceProvider.CreateScope();
            var scopedService = serviceProvider.GetService<IScopedService>();
            scopedService.Print();
            scope.Dispose();
        }

        public static IServiceProvider BuildServiceProvider(IServiceCollection services, IConfiguration configuration)
        {
            services.AddInstallersFromAssembly<IAssemblyMarker>(configuration);
            return services.BuildServiceProvider();
        }
    }
}
