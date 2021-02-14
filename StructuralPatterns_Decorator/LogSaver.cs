
namespace StructuralPatterns_Decorator
{
    using System.Linq;
    using System.Threading.Tasks;
    using static StructuralPatterns_Decorator.DB.WannabeDb;

    public sealed class LogSaver : ILogSaver
    {
        public Task SaveLogEntry(string Id, string log)
        {
            Db.Where(x => x.Id == Id)
                .FirstOrDefault()
                .Logs
                .Add(log);

            return Task.FromResult<object>(null);
        }
    }
}
