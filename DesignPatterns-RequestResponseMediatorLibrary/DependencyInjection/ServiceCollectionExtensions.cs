using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DesignPatterns_RequestResponseMediatorLibrary.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DesignPatterns_RequestResponseMediatorLibrary.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services, ServiceLifetime lifetime, params Type[] markers)
        {
            var handlerInfo = new Dictionary<Type, Type>();

            foreach (var marker in markers)
            {
                var assembly = marker.Assembly;
                var requests = GetClassesImplementingInterface(assembly, typeof(IRequest<>));
                var handlers = GetClassesImplementingInterface(assembly, typeof(IHandler<,>));

                requests.ForEach(type =>
                {
                    // IHandler`2
                    // GetInterface search for IHandler with two generic arguments
                    handlerInfo[type] =
                        handlers.SingleOrDefault(x => type == x.GetInterface("IHandler`2")!
                                                               .GetGenericArguments()[0]);
                });

                var serviceDescriptor = handlers.Select(x => new ServiceDescriptor(x, x, lifetime));
                services.TryAdd(serviceDescriptor);
            }

            services.AddSingleton<IMediator>(x => 
                new Mediator(x.GetRequiredService, handlerInfo));

            return services;
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

                        return !type.IsInterface && !type.IsAbstract && implementRequestType;
                    });
        }


        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var element in list)
                action(element);
        }
    }
}
