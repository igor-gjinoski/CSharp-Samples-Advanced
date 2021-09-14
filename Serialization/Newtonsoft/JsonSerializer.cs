using System.IO;
using Newtonsoft.Json;

namespace Serialization
{
    public class JsonSerializer<TObject> : ISerializer<TObject>
    {
        public void Serialize(TObject obj, Stream stream)
        {
            var writer = new StreamWriter(stream);
            writer.Write(JsonConvert.SerializeObject(obj, Formatting.Indented));
            writer.Flush();
        }

        public TObject Deserialize(Stream stream)
        {
            return JsonConvert.DeserializeObject<TObject>(new StreamReader(stream).ReadToEnd());
        }
    }
}
