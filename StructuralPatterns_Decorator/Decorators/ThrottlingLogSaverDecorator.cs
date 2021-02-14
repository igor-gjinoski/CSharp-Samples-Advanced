
namespace StructuralPatterns_Decorator
{
    using System.Linq;
    using System.Threading.Tasks;
    using static StructuralPatterns_Decorator.DB.WannabeDb;

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
                IncrementUserQuota(Id);
                await _logSaverDecorator.SaveLogEntry(Id, log);
                return;
            }
            System.Console.WriteLine("Quota Reached!");
        }

        private bool QuotaReached(string Id)
        {
            return Db.Where(x => x.Id == Id)
                     .FirstOrDefault().Quota >= QuotaLimit;
        }

        private void IncrementUserQuota(string Id)
        {
            Db.Where(x => x.Id == Id)
                     .FirstOrDefault().Quota += 1;
        }
    }
}
