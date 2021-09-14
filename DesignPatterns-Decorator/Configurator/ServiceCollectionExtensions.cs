using Microsoft.Extensions.DependencyInjection;
using System;

namespace DesignPatterns.Configurator
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddScopedDecoratedService<TServiceInterface>(
            this IServiceCollection services,
            Action<IServiceDecoratorConfigurator<TServiceInterface>> configCallback)
            where TServiceInterface : class
        {
            return services
                .AddScoped(serviceProvider =>
                    BuildDecoratedService(serviceProvider, configCallback));
        }


        private static TServiceInterface BuildDecoratedService<TServiceInterface>(
            IServiceProvider serviceProvider,
            Action<IServiceDecoratorConfigurator<TServiceInterface>> configCallback)
        {
            var configurator = serviceProvider.GetService<IServiceDecoratorConfigurator<TServiceInterface>>();

            if (!(configurator is IServiceDecoratorBuilder<TServiceInterface> builder))
            {
            }

            configCallback(configurator);
            return builder.Build(serviceProvider);
        }
    }
}
