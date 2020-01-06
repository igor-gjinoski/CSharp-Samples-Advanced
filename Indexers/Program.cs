
namespace Indexers
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            var collection = new CustomeCollection<int>(2);
        }
    }

    class CustomeCollection<T>
        where T : notnull
    {
        private T[] array;
        private int lastUsedIndex = 0;

        public CustomeCollection()
            : this(10)
        {
        }

        public CustomeCollection(uint size)
            => array = new T[size];


        /* Indexer */
        public T this[uint index] // With uint we guarantee that passed index will be positive integer
        {
            /* Comparer<uint>.Default.Compare(index, (uint)array.Length) > 0 */
            /* Check if the index is in boundaries of the array */
            get
            {
                if (Comparer<uint>.Default.Compare(index, (uint)array.Length) > 0)
                    throw new ArgumentOutOfRangeException();
                return array[index];
            }
            set
            {
                if (Comparer<uint>.Default.Compare(index, (uint)array.Length) > 0)
                    throw new ArgumentOutOfRangeException();
                array[index] = value;
            }
        }


        /* Functionality */
        public void Add(T value)
        {
            if (lastUsedIndex >= array.Length)
            {
                T[] newArr = new T[lastUsedIndex * 2];
                array.CopyTo(newArr, 0);
                array = newArr;
            }
            array[lastUsedIndex] = value;
            lastUsedIndex++;
        }

        /* More functionality */
        // ...
        // ...
        // ...
    }
}
