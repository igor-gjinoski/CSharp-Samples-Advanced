
namespace StructuralPatterns_Decorator
{
    using StructuralPatterns_Decorator.DB;
    using System.Collections.Generic;

    class Program
    {
        static void Main()
        {
            WannabeDb.Db.Add(
                new LogEntry() 
                { 
                    Id = "x1", 
                    Quota = 0, 
                    Logs = new List<string>() 
                });

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
