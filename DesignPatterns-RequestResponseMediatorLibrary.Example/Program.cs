using System.Threading;
using System.Threading.Tasks;
using DesignPatterns_RequestResponseMediatorLibrary.Abstractions;
using DesignPatterns_RequestResponseMediatorLibrary.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace DesignPatterns_RequestResponseMediatorLibrary.Example
{
    class Program
    {
        static async Task Main()
        {
            var assemblies = typeof(Program).Assembly;

            var serviceProvider = new ServiceCollection()
                .AddMediator(config => config.AsSingleton(), assemblies)
                .BuildServiceProvider();

            IMediator mediator = serviceProvider.GetRequiredService<IMediator>();

            await mediator.SendAsync(GetRequest(), CancellationToken.None);
        }

        static ConsoleLogRequest GetRequest()
            => new ConsoleLogRequest()
            {
                Data = "TestLog"
            };

    }
}
