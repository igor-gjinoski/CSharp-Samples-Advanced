using System.IO;

namespace Serialization
{
    public interface ISerializer<TObject>
    {
        void Serialize(TObject obj, Stream stream);

        TObject Deserialize(Stream stream);
    }
}
