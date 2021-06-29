using BenchmarkDotNet.Attributes;

namespace CompileTimePerformanceWithReflection
{
    [MemoryDiagnoser]
    public class ReflectionBenchmark
    {
        [Benchmark]
        public string NoReflectionGet() => ReflectionUsage.NoReflectionGet();

        [Benchmark]
        public string TraditionalReflection() => ReflectionUsage.TraditionalReflection();
        
        [Benchmark]
        public string OptimizedTraditionalReflection() => ReflectionUsage.OptimizedTraditionalReflection();
        
        [Benchmark]
        public string CompiledDelegate() => ReflectionUsage.CompiledDelegate();
        
        [Benchmark]
        public string EmmitedVersion() => ReflectionUsage.EmmitedVersion();
    }
}
