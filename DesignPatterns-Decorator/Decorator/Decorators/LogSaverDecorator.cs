using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.Decorator
{
    public class LogSaverDecorator : ILogSaver
    {
        private readonly ILogSaver _logSaver;

        public LogSaverDecorator(ILogSaver logSaver)
            =>
            _logSaver = logSaver;


        public async Task SaveLogEntry(string id, string log, CancellationToken cancellationToken)
        {
            await _logSaver.SaveLogEntry(id, log, cancellationToken);
        }
    }
}
