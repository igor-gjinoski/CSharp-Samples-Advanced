
namespace Parallel
{
    using System;
    using System.Collections.Concurrent;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static void PopulateDictionaryParallel(ConcurrentDictionary<int, int> dictionary, int dictSize)
        {
            Parallel.For(0, dictSize, (i) => dictionary.TryAdd(i, 0));
            Parallel.For(0, dictSize,
                (i) => {
                    bool done = dictionary.TryUpdate(i, 1, 0);
                    if (!done)
                        throw new Exception("Error updating. Old value was " + dictionary[i]);
                });
        }
        static int GetTotalValueParallel(ConcurrentDictionary<int, int> dictionary)
        {
            int expectedTotal = dictionary.Count;

            int total = 0;
            Parallel.ForEach(dictionary,
                keyValPair => {
                    Interlocked.Add(ref total, keyValPair.Value);
                });
            return total;
        }

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
