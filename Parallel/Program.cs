
namespace Parallel
{
    using System;
    using System.Collections.Concurrent;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        /// <summary>
        /// Parallel.For
        /// Executes a for loop in which iterations may run in parallel.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="dictSize"></param>
        static void PopulateDictionaryParallel(ConcurrentDictionary<int, int> dictionary, int dictSize)
        {
            /* For(Int32, Int32, Action<Int32,ParallelLoopState>) */

            Parallel.For(0, dictSize, (i) => dictionary.TryAdd(i, 0));
            Parallel.For(0, dictSize,
                (i) => {
                    bool done = dictionary.TryUpdate(i, 1, 0);
                    if (!done)
                        throw new Exception("Error updating. Old value was " + dictionary[i]);
                });
        }

        /// <summary>
        /// Parallel.ForEach
        /// Executes a foreach (For Each in Visual Basic) operation in which iterations may run in parallel.
        /// </summary>
        /// <param name="dictionary"></param>
        static int GetTotalValueParallel(ConcurrentDictionary<int, int> dictionary)
        {
            int expectedTotal = dictionary.Count;

            /* ForEach<TSource,TLocal>(IEnumerable<TSource>, ParallelOptions, Func<TLocal>, Func<TSource,ParallelLoopState,TLocal,TLocal>, Action<TLocal>) */

            int total = 0;
            Parallel.ForEach(dictionary,
                keyValPair => {
                    Interlocked.Add(ref total, keyValPair.Value);
                });
            return total;
        }

        /// <summary>
        /// ConcurrentDictionary<TKey,TValue>
        /// Represents a thread-safe collection of key/value pairs that can be accessed by multiple threads concurrently.
        /// </summary>
        static void Main()
        {
            Stopwatch stopwatch = new Stopwatch();

            var dictionarySize = 10;
            var dictionary = new ConcurrentDictionary<int, int>();

            stopwatch.Start();
            PopulateDictionaryParallel(dictionary, dictionarySize);
            stopwatch.Stop();

            Console.WriteLine(
                string.Format($"Time taken to build dictionary (ms): {stopwatch.ElapsedMilliseconds}"));

            stopwatch.Restart();
            int total = GetTotalValueParallel(dictionary);
            stopwatch.Stop();

            Console.WriteLine(
                string.Format($"Time taken to enumerate dictionary (ms): {stopwatch.ElapsedMilliseconds}"));
        }
    }
}
