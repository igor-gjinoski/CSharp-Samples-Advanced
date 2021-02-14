
namespace DesignPatterns.Structural_patterns.Decorator
{
    using System.Threading.Tasks;

    public interface ILogSaver
    {
        Task SaveLogEntry(string applicationId, string log);
    }
}
