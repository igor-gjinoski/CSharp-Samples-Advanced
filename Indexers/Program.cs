
namespace Indexers
{
    using System;

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


        public T this[uint index]
        {
            get => index < array.Length
                ? array[index]
                : throw new IndexOutOfRangeException();

            set => array[index] = value;
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
