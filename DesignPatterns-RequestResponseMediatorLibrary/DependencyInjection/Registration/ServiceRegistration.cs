using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using DesignPatterns_RequestResponseMediatorLibrary.Abstractions;

namespace DesignPatterns_RequestResponseMediatorLibrary.DependencyInjection.Registration
{
    public static class ServiceRegistrar
    {
        public static void AddRequiredServices(IServiceCollection services, MediatorServiceConfiguration serviceConfiguration)
        {
            services.TryAdd(
                new ServiceDescriptor(typeof(IMediator),
                serviceConfiguration.MediatorImplementationType,
                serviceConfiguration.Lifetime));
        }


        public static void AddMediatorClasses(IServiceCollection services, IEnumerable<Assembly> assembliesToScan)
        {
            var handlerInfo = new ConcurrentDictionary<Type, Type>();
            var typesRequests = new List<Type>();
            var typesHandlers = new List<Type>();

            foreach (var assembly in assembliesToScan)
            {
                typesRequests.AddRange(GetClassesImplementingInterface(assembly, typeof(IRequest<>)));
                typesHandlers.AddRange(GetClassesImplementingInterface(assembly, typeof(IHandler<,>)));
            }

            typesRequests.ForEach(type =>
            {
                handlerInfo[type] =
                    typesHandlers.SingleOrDefault(handler =>
                    {
                        Type handlerRequestArgument = handler.GetInterface("IHandler`2")!.GetGenericArguments()[0];

                        return (type == handlerRequestArgument || 
                                type.IsSubclassOf(handlerRequestArgument));
                    });
            });

            var serviceDescriptor = typesHandlers.Select(x => new ServiceDescriptor(x, x, ServiceLifetime.Scoped));
            services.TryAdd(serviceDescriptor);
            services.AddSingleton(serviceProvider => handlerInfo);
        }


        private static IEnumerable<Type> GetClassesImplementingInterface(Assembly assembly, Type interfaceType)
        {
            return assembly.ExportedTypes
                    .Where(type =>
                    {
                        var implementRequestType = type
                            .GetInterfaces()
                            .Any(@interface => @interface.IsGenericType &&
                                               @interface.GetGenericTypeDefinition() == interfaceType);

                        return implementRequestType;
                    });
        }
    }
}
