using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;

namespace Span
{
    class Program
    {
        static void Main()
        {
            BenchmarkRunner.Run<Benchmarks>();
        }
    }

    /// <summary>
    /// Span<T> is a ref struct that is allocated on the stack rather than on the managed heap. 
    /// Ref struct types have a number of restrictions to ensure that they cannot be promoted to the managed heap, 
    /// including that they can't be boxed and they can't be used across await and yield boundaries.
    /// </summary>
    [MemoryDiagnoser]
    public class Benchmarks
    {
        private static readonly string stringDate = "14 08 2021";

        [Benchmark]
        public (int day, int month, int year) ReadDateUsingStringSplit()
        {
            var day = int.Parse(stringDate.Substring(0, 2));
            var month = int.Parse(stringDate.Substring(3, 2));
            var year = int.Parse(stringDate.Substring(6));

            return (day, month, year);
        }

        [Benchmark]
        public (int day, int month, int year) ReadDateUsingSpan()
        {
            ReadOnlySpan<char> dateSpan = stringDate;

            var day = int.Parse(dateSpan.Slice(0, 2));
            var month = int.Parse(dateSpan.Slice(3, 2));
            var year = int.Parse(dateSpan.Slice(6));

            return (day, month, year);
        }
    }
}
