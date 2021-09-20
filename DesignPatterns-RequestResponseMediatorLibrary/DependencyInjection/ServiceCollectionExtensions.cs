using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using DesignPatterns_RequestResponseMediatorLibrary.DependencyInjection.Registration;

namespace DesignPatterns_RequestResponseMediatorLibrary.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services, params Assembly[] assemblies)
        {
            return services.AddMediator(assemblies, configuration: null);
        }


        public static IServiceCollection AddMediator(this IServiceCollection services, Action<MediatorServiceConfiguration> configuration, params Assembly[] assemblies)
        {
            return services.AddMediator(assemblies, configuration);
        }


        private static IServiceCollection AddMediator(this IServiceCollection services, IEnumerable<Assembly> assemblies, Action<MediatorServiceConfiguration> configuration)
        {
            ValidateAssemblyCount(assemblies);

            var serviceConfig = new MediatorServiceConfiguration();
            configuration?.Invoke(serviceConfig);

            ServiceRegistrar.AddRequiredServices(services, serviceConfig);
            ServiceRegistrar.AddMediatorClasses(services, assemblies);

            return services;
        }


        private static void ValidateAssemblyCount(IEnumerable<Assembly> assemblies)
        {
            if (!assemblies.Any())
            {
                throw new ArgumentException("No assemblies found to scan. Supply at least one assembly to scan for handlers.");
            }
        }
    }
}
