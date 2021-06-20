using DesignPatterns_Mediator.Log;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DesignPatterns_Mediator
{
    class Program
    {
        public static IServiceProvider BuildServiceProvider(IServiceCollection services)
        {
            services.AddSingleton<IFileLogger, FileLogger>();
            services.AddSingleton<IConsoleLogger, ConsoleLogger>();

            return services.BuildServiceProvider();
        }

        static void Main()
        {
            IServiceCollection services = new ServiceCollection();
            var serviceProvider = BuildServiceProvider(services);

            IFileLogger fileLoggerService = serviceProvider.GetService<IFileLogger>();
            IConsoleLogger consoleLoggerService = serviceProvider.GetService<IConsoleLogger>();

            string infoLogMsg = "INFO log message";
            string warnLogMsg = "WARN log message";
            string errorLogMsg = "ERROR log message";

            var logger = new Logger(fileLoggerService, consoleLoggerService);
            logger.Log(infoLogMsg, LogType.INFO);
            logger.Log(warnLogMsg, LogType.WARN);
            logger.Log(errorLogMsg, LogType.ERROR);
        }
    }
}
