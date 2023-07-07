using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Eflatun.SceneReference.DevelopmentUtils
{
    public static class BinarySerializationUtils
    {
        public static string SerializeToBinaryBase64(object source)
        {
            var bf = new BinaryFormatter();
            using var ms = new MemoryStream();
            bf.Serialize(ms, source);
            var serializedBytes = ms.ToArray();
            return Convert.ToBase64String(serializedBytes);
        }

        public static T DeserializeFromBinaryBase64<T>(string binaryBase64) where T : class
        {
            var bytes = Convert.FromBase64String(binaryBase64);
            var bf = new BinaryFormatter();
            using var ms = new MemoryStream(bytes);
            return bf.Deserialize(ms) as T;
        }
    }
}
