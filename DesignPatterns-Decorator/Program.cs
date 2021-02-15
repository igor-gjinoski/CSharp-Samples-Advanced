
namespace DesignPatterns
{
    using DesignPatterns.Structural.Decorator;

    class Program
    {
        static void Main()
        {
            var decorator = 
                new ThrottlingLogSaverDecorator(
                    new TraceLogSaverDecorator(
                        new LogSaver()));

            decorator.SaveLogEntry("x1", "LOG")
                .GetAwaiter();
        }
    }
}
