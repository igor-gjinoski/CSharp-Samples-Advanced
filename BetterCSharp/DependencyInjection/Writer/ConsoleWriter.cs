using DependencyInjection.Abstractions;

namespace DependencyInjection.Writer
{
    public class ConsoleWriter<T> : IWriter<T>
    {
        private System.Action<T> WriteToConsole = delegate (T x)
        {
            System.Console.WriteLine(x);
        };

        public void Write(T data)
        {
            WriteToConsole(data);
        }
    }
}
