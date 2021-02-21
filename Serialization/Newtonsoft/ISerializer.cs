using System.IO;

namespace Serialization
{
    public interface ISerializer<T>
    {
        void Serialize(T obj, Stream stream);

        T Deserialize(Stream stream);
    }
}
