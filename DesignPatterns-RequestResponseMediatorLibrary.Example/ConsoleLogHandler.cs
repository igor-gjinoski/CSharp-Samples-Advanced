using DesignPatterns_RequestResponseMediatorLibrary.Abstractions;
using System.Threading.Tasks;

namespace DesignPatterns_RequestResponseMediatorLibrary.Example
{
    public class ConsoleLogHandler : IHandler<ConsoleLogRequest, bool>
    {
        public async Task<bool> HandleAsync(ConsoleLogRequest request)
        {
            System.Console.WriteLine(request?.Data);

            return 
                await Task.FromResult(true);
        }
    }
}
