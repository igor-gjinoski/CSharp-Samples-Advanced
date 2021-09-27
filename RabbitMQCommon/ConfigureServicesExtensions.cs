using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;

namespace RabbitMQCommon
{
    public static class ConfigureServicesExtensions
    {
        public static IServiceCollection AddMassTransitMessaging(this IServiceCollection services, params System.Type[] consumers)
        {
            services
                .AddMassTransit(mt =>
                {
                    consumers.ForEach(consumer => mt.AddConsumer(consumer));

                    mt.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host("localhost", "/", h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });

                        consumers.ForEach(consumer =>
                            cfg.ReceiveEndpoint(consumer.FullName, endpoint =>
                            {
                                endpoint.ConfigureConsumer(context, consumer);
                            }));
                    });
                }).AddMassTransitHostedService();

            return services;
        }

        private static void ForEach<T>(this IEnumerable<T> enumerable, System.Action<T> action)
        {
            foreach (T item in enumerable)
                action(item);
        }
    }
}
