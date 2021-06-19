using System;
using System.Collections.Generic;
using System.Linq;
using ClassArchitecture.Abstractions;
using ClassArchitecture.Implementations;

namespace ClassArchitecture
{
    public class Processor<T>
    {
        private readonly IDataProvider<T> _dataProvider;
        private readonly IManipulator<T, T> _manipulator;
        private readonly IWriter<T> _writer;

        public Processor(
            IDataProvider<T> dataProvider,
            IManipulator<T, T> manipulator,
            IWriter<T> writer)
        {
            _dataProvider = dataProvider;
            _manipulator = manipulator;
            _writer = writer;
        }

        public void Process()
        {
            IEnumerable<T> data = 
                _dataProvider.GetData()
                .ToList();

            T result = _manipulator.Manipulate(data);

            _writer.Write(result);
        }
    }

    public class Program
    {
        static void Main()
        {
            IDataProvider<int> provider = new RandomIntProvider(2, 10);
            IWriter<int> writer = new ConsoleWriter<int>();

            IManipulator<int, int> collectionSumManipulator = new CollectionSum();
            IManipulator<int, int> pythagoreanTheoremManipulator = new PythagoreanTheorem();

            //var processor = new Processor<int>(provider, collectionSumManipulator, writer);
            var processor = new Processor<int>(provider, pythagoreanTheoremManipulator, writer);
            processor.Process();
        }
    }
}
