using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace EnumPerformanceTrap
{
    class Program
    {
        static void Main()
        {
            // ToString() allocates unnecessary memory - see compiler Gen0 from benchmarks.
            // You don't have to use it, because we already have the value at compile time.
            // nameof() is faster and does not allocate new memory.
            BenchmarkRunner.Run<Benckmarks>();
        }
    }

    [MemoryDiagnoser]
    public class Benckmarks
    {
        [Benchmark]
        public string EnumToString()
        {
            return LogType.INFO.ToString();
        }

        [Benchmark]
        public string NameofEnum()
        {
            return nameof(LogType.INFO);
        }
    }

    public enum LogType
    {
        INFO,
        WARN,
        ERROR
    }
}
