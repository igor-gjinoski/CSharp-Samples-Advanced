
namespace DesignPatterns.Structural.Decorator
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class TraceLogSaverDecorator : LogSaverDecorator
    {
        public TraceLogSaverDecorator(ILogSaver logSaverDecorator)
            : base(logSaverDecorator)
        {
        }

        public override async Task SaveLogEntry(string Id, string log)
        {
            var sw = Stopwatch.StartNew();

            try
            {
                await _logSaverDecorator.SaveLogEntry(Id, log);
            }
            finally
            {
                System.Console.WriteLine($"Operation complete in: {sw.ElapsedMilliseconds} Milliseconds");
            }
        }
    }
}
