using System.IO;
using System.IO.Compression;

namespace Serialization.StreamGenerators
{
    public abstract class StreamGenerator
    {
        public abstract Stream GetReadStream(string filePath);

        public abstract Stream GetWriteStream(string filePath);
    }

    public class StandardStreamGenerator : StreamGenerator
    {
        public override Stream GetReadStream(string filePath)
        {
            return new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }

        public override Stream GetWriteStream(string filePath)
        {
            return new FileStream(filePath, FileMode.Create, FileAccess.Write);
        }
    }
}
