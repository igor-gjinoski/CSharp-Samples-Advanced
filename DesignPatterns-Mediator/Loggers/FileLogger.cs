using System.IO;
using System.Threading.Tasks;

namespace DesignPatterns_Mediator
{
    internal sealed class FileLogger : IFileLogger
    {
        public async Task LogToFile(string msg, LogType logType)
        {
            await File.AppendAllTextAsync(
                "Log.txt",
                $"Type: {logType} - Message: {msg}\n");
        }
    }
}
