using Serialization.StreamGenerators;

namespace Serialization
{
    class Program
    {
        static void Main()
        {
            string[] stringArray = new[] { "some", "array", "values" };

            var serializer = new JsonSerializer<string[]>();
            var streamGenerator = new StandardStreamGenerator();

            // Create json file from C# object
            serializer.Serialize(stringArray, streamGenerator.GetWriteStream(@"D:\json.txt"));

            // Deserialize json file into C# object
            string[] deserializedArray = 
                serializer.Deserialize(streamGenerator.GetReadStream(@"D:\json.txt"));
        }
    }
}
