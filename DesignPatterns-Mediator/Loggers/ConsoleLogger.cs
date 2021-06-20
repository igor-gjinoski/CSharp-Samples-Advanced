
namespace DesignPatterns_Mediator
{
    internal sealed class ConsoleLogger : IConsoleLogger
    {
        public void LogToConsole(string msg, LogType logType)
        {
            System.Console.WriteLine($"Type: {logType} - Message: {msg}");
        }
    }
}
