using BenchmarkDotNet.Running;

namespace CompileTimePerformanceWithReflection
{
    class Program
    {
        static void Main()
        {
            BenchmarkRunner.Run<ReflectionBenchmark>();
        }
    }
}
