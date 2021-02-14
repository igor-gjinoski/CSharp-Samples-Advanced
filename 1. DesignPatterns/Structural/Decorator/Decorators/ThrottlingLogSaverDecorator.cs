
namespace DesignPatterns.Structural.Decorator
{
    using System.Threading.Tasks;

    public class ThrottlingLogSaverDecorator : LogSaverDecorator
    {
        public ThrottlingLogSaverDecorator(ILogSaver logSaverDecorator)
            : base(logSaverDecorator)
        {
        }

        public override async Task SaveLogEntry(string Id, string log)
        {
            if (!QuotaReached(Id))
            {
                IncrementUserQuota();
                await _logSaverDecorator.SaveLogEntry(Id, log);
                return;
            }
            System.Console.WriteLine("Quota Reached!");
        }

        private bool QuotaReached(string Id)
        {
            bool isQuotaReached = false;

            // ... Check if quota is reached.

            return isQuotaReached;
        }

        private void IncrementUserQuota()
        {
            // ... Increment current quota with one.
        }
    }
}
