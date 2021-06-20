using System;
using System.Collections.Generic;

namespace Some_Cool_Stuff
{
    public class DemoClass<T, V>
    {
        private readonly Dictionary<T, V> _dictionary;

        public DemoClass(IDictionary<T, V> dictionary)
        {
            /*
             * new(dictionary)
            */
            _dictionary = new(dictionary);
        }
    }

    class Program
    {
        static void Main()
        {
            var dict = new Dictionary<int, string>()
            {
                { 1, "1" }
            };

            /*
             * out var value 
            */
            TryGetValue(dict, 1, out var value);

            Console.WriteLine(value);
        }

        public static TValue TryGetValue<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key, out TValue value)
        {
            value = default(TValue);

            if (dictionary.ContainsKey(key))
                value = dictionary[key];
                
            return value;
        }
    }
}
