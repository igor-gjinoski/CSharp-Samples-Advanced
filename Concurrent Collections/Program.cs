
namespace Concurrent_Collections
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Collections.Concurrent;

    public class A
    {
        public string Name { get; set; }

        public A MemberwiseCloneA()
        {
            return (A)this.MemberwiseClone();
        }
    }

    class Program
    {
        static void Main()
        {
            var queue = new ConcurrentQueue<string>();

            Task firstTask = Task.Run(() => AddElement(queue, "TA"));
            Task secondTask = Task.Run(() => AddElement(queue, "DSA"));
            Task.WaitAll(firstTask, secondTask);
            // Print(queue);

            var first = new A { Name = "Test" };
            var second = first;
            first.Name = "New";
            Console.WriteLine(first.Name);
            Console.WriteLine(second.Name);

            // Example of Concurrent Dincionary
            Concurrent_Dictionary();
        }

        static void Print<T>(ConcurrentQueue<T> queue)
        {
            using (IEnumerator<T> enumerator = queue.GetEnumerator())
            {
                while (enumerator.MoveNext())
                    Console.WriteLine(enumerator.Current);
            }
        }

        static void AddElement<T>(ConcurrentQueue<T> queue, T element)
        {
            for (int index = 1; index <= 5; index++)
            {
                var value = (T)Convert.ChangeType($"ID {index}: {element}", typeof(T));
                queue.Enqueue(value);
            }
        }

        public static void Concurrent_Dictionary()
        {
            var dictionary = new ConcurrentDictionary<int, string>();

            // Add element to ConcurrentDictionary
            dictionary[0] = "A";
            dictionary[1] = "B";
            dictionary.TryAdd(2, "C"); // Thread Safe
            dictionary.TryAdd(3, "D"); // Thread Safe

            // Print all elements
            PrintDictionary(dictionary);

            // Remove operation
            string value;
            bool success = dictionary.TryRemove(2, out value);
            if (success)
                Console.WriteLine($"Removed value: {value}");

            // Get operation
            dictionary.TryGetValue(3, out value);
            Console.WriteLine($"Get value: {value}");

            /* AddOrUpdate(index, 
                           default value if index not exist, 
                           delegate for the new value) */
            value = dictionary.AddOrUpdate(2, "default", (key, old) => old = "T"); // Thread Safe

            /* GetOrAdd(key to look up,
                        value to add if key is missing) */
            value = dictionary.GetOrAdd(4, "default"); // Thread Safe

            PrintDictionary(dictionary);
        }

        static void PrintDictionary<TKey, TValue>(ConcurrentDictionary<TKey, TValue> dictionary)
        {
            foreach (var item in dictionary)
                Console.WriteLine($"Key: {item.Key} - Value: {item.Value}");
        }
    }
}
