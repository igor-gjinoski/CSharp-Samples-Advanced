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
            var serviceProvider = new ServiceCollection()
                .AddMediator(ServiceLifetime.Scoped, typeof(Program))
                .BuildServiceProvider();

            IMediator mediator = serviceProvider.GetRequiredService<IMediator>();

            await mediator.SendAsync(GetRequest());
        }

        static ConsoleLogRequest GetRequest()
            => new ConsoleLogRequest()
            {
                Data = "TestLog"
            };

    }
}
