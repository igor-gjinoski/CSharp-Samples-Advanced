using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AssemblyScanning.IoC
{
    public static class InstallerExtensions
    {
        public static void AddInstallersFromAssembly<TMarker>(this IServiceCollection services, IConfiguration configuration)
        {
            AddInstallersFromAssemblies(services, configuration, typeof(TMarker));
        }


        public static void AddInstallersFromAssemblies(this IServiceCollection services, IConfiguration configuration, params Type[] assemblyMarkers)
        {
            var assemblies = assemblyMarkers.Select(x => x.Assembly).ToArray();
            AddInstallersFromAssemblies(services, configuration, assemblies);
        }


        public static void AddInstallersFromAssemblies(this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                var installerTypes =
                    assembly.DefinedTypes.Where(typeInfo =>
                        typeof(IInstaller).IsAssignableFrom(typeInfo) &&
                        !typeInfo.IsInterface &&
                        !typeInfo.IsAbstract);

                var installers = installerTypes.Select(Activator.CreateInstance).Cast<IInstaller>();

                foreach (var installer in installers)
                    installer.AddServices(services, configuration);
            }
        }
    }
}
