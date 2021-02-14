
namespace StructuralPatterns_Decorator
{
    class Program
    {
        static void Main()
        {
            ILogSaver logSaver = 
                new ThrottlingLogSaverDecorator(
                    new LogSaver());

            logSaver.SaveLogEntry("x1", "Log-1");
            logSaver.SaveLogEntry("x1", "Log-2");
            logSaver.SaveLogEntry("x1", "Log-3");
            logSaver.SaveLogEntry("x1", "Log-4");
        }
    }
}
