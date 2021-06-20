
namespace DesignPatterns_Mediator
{
    public class Logger : ILogger
    {
        private IFileLogger _fileLogger;
        private IConsoleLogger _consoleLogger;

        public Logger(IFileLogger fileLogger, IConsoleLogger consoleLogger)
        {
            _fileLogger = fileLogger;
            _consoleLogger = consoleLogger;
        }

        public void Log(string msg, LogType logType)
        {
            _fileLogger.LogToFile(msg, logType);
            _consoleLogger.LogToConsole(msg, logType);
        }
    }
}
