using System.Threading.Tasks;

namespace DesignPatterns_Mediator
{
    public interface IFileLogger
    {
        Task LogToFile(string msg, LogType logType);
    }
}
