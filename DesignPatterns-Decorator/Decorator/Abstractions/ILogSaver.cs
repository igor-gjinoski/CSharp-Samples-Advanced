using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.Decorator
{
    public interface ILogSaver
    {
        Task SaveLogEntry(string id, string log, CancellationToken cancellationToken);
    }
}
