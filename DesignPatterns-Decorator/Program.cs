﻿using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using DesignPatterns.Configurator;
using DesignPatterns.Decorator;
using DesignPatterns.DisposableRegister;

namespace DesignPatterns
{
    class Program
    {
        static void Main()
        {
            IServiceCollection services = new ServiceCollection();
            var serviceProvider = BuildServiceProvider(services);

            ILogSaver logSaver = serviceProvider.GetService<ILogSaver>();
            logSaver.SaveLogEntry("x1", "SOME_LOG", CancellationToken.None);
        }

        public static System.IServiceProvider BuildServiceProvider(IServiceCollection services)
        {
            services
                .AddSingleton(typeof(IDisposeRegister), typeof(DisposeRegister))
                .AddScoped(typeof(IServiceDecoratorConfigurator<>), typeof(DefaultDecoratorBuilder<>));
            
            services
                .AddScopedDecoratedService<ILogSaver>(
                    configurator =>
                    {
                        configurator
                            .AddDecorator<LogSaverDecorator>()
                            .AddDecorator<QuotaLogSaverDecorator>()
                            .AddService<LogSaver>();
                    });

            return services.BuildServiceProvider();
        }
    }
}
