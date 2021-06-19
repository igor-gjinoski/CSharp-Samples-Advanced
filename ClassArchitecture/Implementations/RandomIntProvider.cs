using System;
using System.Collections.Generic;
using System.Linq;
using ClassArchitecture.Abstractions;

namespace ClassArchitecture.Implementations
{
    public class RandomIntProvider : IDataProvider<int>
    {
        private readonly Random _random = new Random();
        private readonly int _count;
        private readonly int _max;

        /// <summary>Get collection of random int values.
        /// <list type="bullet">
        /// <item>
        /// <description>count: collection size</description>
        /// </item>
        /// <item>
        /// <description>max: max value of the elements</description>
        /// </item>
        /// </list>
        /// </summary>
        public RandomIntProvider(int count, int max)
        {
            _random = new Random();
            _count = count;
            _max = max;
        }

        public IEnumerable<int> GetData()
        {
            return Enumerable.Range(0, _count)
                .Select(x => x = _random.Next(_max));
        }
    }
}
