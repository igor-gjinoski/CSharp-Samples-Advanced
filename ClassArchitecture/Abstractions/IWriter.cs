using System.Collections.Generic;

namespace ClassArchitecture.Abstractions
{
    public interface IWriter<T>
    {
        void Write();

        void Write(T data);

        void Write(IEnumerable<T> data);
    }
}
