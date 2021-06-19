using ClassArchitecture.Abstractions;
using System.Collections.Generic;

namespace ClassArchitecture.Implementations
{
    public class CollectionSum : IManipulator<int, int>
    {
        public int Manipulate(IEnumerable<int> data)
        {
            using var enumerator =
                data.GetEnumerator();

            int result = 0;
            while (enumerator.MoveNext())
            {
                result += enumerator.Current;
            }
            return result;
        }
    }
}
