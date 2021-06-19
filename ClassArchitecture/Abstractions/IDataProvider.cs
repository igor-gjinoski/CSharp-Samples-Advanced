using System.Collections.Generic;

namespace ClassArchitecture.Abstractions
{
    public interface IDataProvider<T>
    {
        IEnumerable<T> GetData();
    }
}
