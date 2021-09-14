using Microsoft.Extensions.DependencyInjection;
using DesignPatterns.Decorator;
using System.Threading;

namespace DesignPatterns
{
    class Program
    {
        static void Main()
        {
            var decorator = 
                new QuotaLogSaverDecorator(
                    new LogSaverDecorator(
                        new LogSaver()));

            decorator
                .SaveLogEntry("x1", "SOME_LOG", CancellationToken.None)
                .GetAwaiter();
        }
    }
}
