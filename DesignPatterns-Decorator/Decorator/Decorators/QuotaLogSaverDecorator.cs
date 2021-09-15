using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.Decorator
{
    public class QuotaLogSaverDecorator : ILogSaver
    {
        private readonly ILogSaver _logSaver;

        public QuotaLogSaverDecorator(ILogSaver logSaver)
            =>
            _logSaver = logSaver;


        public async Task SaveLogEntry(string id, string log, CancellationToken cancellationToken)
        {
            if (!QuotaReached(id))
            {
                IncrementUserQuota();
                System.Console.WriteLine($"QuotaLogSaverDecorator : {id}");
                await _logSaver.SaveLogEntry(id, log, cancellationToken);
                return;
            }
            // ... Quota Reached,
            // ... throw error or else.
        }

        #region PRIVATE

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

        #endregion
    }
}
