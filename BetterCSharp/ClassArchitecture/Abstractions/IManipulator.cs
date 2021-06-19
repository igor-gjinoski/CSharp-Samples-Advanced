using System.Collections.Generic;

namespace ClassArchitecture.Abstractions
{
    public interface IManipulator<T>
    {
        T Manipulate(IEnumerable<T> data);
    }
}
