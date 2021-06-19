using System;
using System.Collections.Generic;
using ClassArchitecture.Abstractions;

namespace ClassArchitecture.Implementations
{
    public class ConsoleWriter<T> : IWriter<T>
    {
        public void Write()
            =>
            Console.WriteLine();


        public void Write(T data)
            =>
            Console.WriteLine(data);


        public void Write(IEnumerable<T> data)
        {
            ForEach(data, x => Console.Write($"{x} "));

            void ForEach(IEnumerable<T> source, Action<T> action)
            {
                foreach (var item in source)
                    action(item);
            }
        }
    }
}
