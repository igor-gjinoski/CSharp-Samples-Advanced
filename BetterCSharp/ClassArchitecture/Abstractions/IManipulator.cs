using System.Collections.Generic;

namespace ClassArchitecture.Abstractions
{
    public interface IManipulator<T, V>
    {
        V Manipulate(IEnumerable<T> data);
    }
}
