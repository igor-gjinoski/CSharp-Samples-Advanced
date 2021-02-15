
namespace DesignPatterns.Structural.Decorator
{
    using System.Threading.Tasks;

    public interface ILogSaver
    {
        Task SaveLogEntry(string Id, string log);
    }
}
