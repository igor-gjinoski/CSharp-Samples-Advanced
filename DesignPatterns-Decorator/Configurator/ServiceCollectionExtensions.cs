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
                .AddScoped(serviceProvider => BuildDecoratedService(serviceProvider, configCallback));
        }

        private static TServiceInterface BuildDecoratedService<TServiceInterface>(
            IServiceProvider serviceProvider,
            Action<IServiceDecoratorConfigurator<TServiceInterface>> configCallback)
        {
            var configurator = serviceProvider.GetService<IServiceDecoratorConfigurator<TServiceInterface>>();

            if (configurator is not IServiceDecoratorBuilder<TServiceInterface> builder)
            {
                throw new InvalidCastException(
                    $"Unable to cast configurator to {typeof(IServiceDecoratorBuilder<TServiceInterface>)}!");
            }

            configCallback(configurator);
            return builder.Build(serviceProvider);
        }
    }
}
