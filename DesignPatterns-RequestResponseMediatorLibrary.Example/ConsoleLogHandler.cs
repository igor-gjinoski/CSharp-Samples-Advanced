using DesignPatterns_RequestResponseMediatorLibrary.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns_RequestResponseMediatorLibrary.Example
{
    public class ConsoleLogHandler : IHandler<ConsoleLogRequest, bool>
    {
        public async Task<bool> HandleAsync(ConsoleLogRequest request, CancellationToken cancellationToken)
        {
            System.Console.WriteLine(request?.Data);

            return 
                await Task.FromResult(true);
        }
    }
}
