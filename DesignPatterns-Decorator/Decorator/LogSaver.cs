using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.Decorator
{
    public sealed class LogSaver : ILogSaver
    {
        public Task SaveLogEntry(string id, string log, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(null);
        }
    }
}
