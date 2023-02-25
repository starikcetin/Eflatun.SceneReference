using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Eflatun.SceneReference.Tests.Runtime
{
    public class SceneReferenceCustomSerializationTests
    {
        private const string SceneName = "TestScene_Enabled";
        private const string SceneGuid = @"e3f2c1473b766c34ba5b37779d71787e";

        private const string JsonRaw = @"{""sceneAssetGuidHex"":""e3f2c1473b766c34ba5b37779d71787e""}";

        private const string Base64 = @"AAEAAAD/////AQAAAAAAAAAMAgAAAE1FZmxhdHVuLlNjZW5lUmVmZXJlbmNlLCBWZXJzaW9uPTAuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbAUBAAAAJUVmbGF0dW4uU2NlbmVSZWZlcmVuY2UuU2NlbmVSZWZlcmVuY2UBAAAAEXNjZW5lQXNzZXRHdWlkSGV4AQIAAAAGAwAAACBlM2YyYzE0NzNiNzY2YzM0YmE1YjM3Nzc5ZDcxNzg3ZQs=";

        private const string XmlRaw = @"<?xml version=""1.0"" encoding=""utf-16""?><Eflatun.SceneReference.SceneReference>e3f2c1473b766c34ba5b37779d71787e</Eflatun.SceneReference.SceneReference>";

        [Test]
        public void SerializesToJson()
        {
            var sceneRef = new SceneReference(SceneGuid);
            var json = JsonConvert.SerializeObject(sceneRef, Newtonsoft.Json.Formatting.None);

            Assert.AreEqual(JsonRaw, json);
        }

        [Test]
        public void DeserializesFromJson()
        {
            var deserialized = JsonConvert.DeserializeObject<SceneReference>(JsonRaw);

            Assert.IsNotNull(deserialized);
            Assert.AreEqual(SceneGuid, deserialized.sceneAssetGuidHex);

#if UNITY_EDITOR
            Assert.IsNotNull(deserialized.sceneAsset);
            Assert.AreEqual(SceneName, deserialized.sceneAsset.name);
#endif // UNITY_EDITOR
        }

        [Test]
        public void SerializesToBinary()
        {
            var sceneRef = new SceneReference(SceneGuid);

            var bf = new BinaryFormatter();
            using var ms = new MemoryStream();
            bf.Serialize(ms, sceneRef);
            var serializedBytes = ms.ToArray();
            var serializedBase64 = Convert.ToBase64String(serializedBytes);

            Assert.AreEqual(Base64, serializedBase64);
        }

        [Test]
        public void DeserializesFromBinary()
        {
            var bytes = Convert.FromBase64String(Base64);

            var bf = new BinaryFormatter();
            using var ms = new MemoryStream(bytes);
            var deserialized = bf.Deserialize(ms) as SceneReference;

            Assert.IsNotNull(deserialized);
            Assert.AreEqual(SceneGuid, deserialized.sceneAssetGuidHex);

#if UNITY_EDITOR
            Assert.IsNotNull(deserialized.sceneAsset);
            Assert.AreEqual(SceneName, deserialized.sceneAsset.name);
#endif // UNITY_EDITOR
        }

        [Test]
        public void SerializesToXml()
        {
            var sceneRef = new SceneReference(SceneGuid);

            var xmlSerializer = new XmlSerializer(typeof(SceneReference));
            var sb = new StringBuilder();
            using var xmlWriter = XmlWriter.Create(sb);
            xmlSerializer.Serialize(xmlWriter, sceneRef);
            var xml = sb.ToString();

            Assert.AreEqual(XmlRaw, xml);
        }

        [Test]
        public void DeserializesFromXml()
        {
            var xmlSerializer = new XmlSerializer(typeof(SceneReference));
            using var stringReader = new StringReader(XmlRaw);
            using var xmlReader = XmlReader.Create(stringReader);
            var deserialized = xmlSerializer.Deserialize(xmlReader) as SceneReference;

            Assert.IsNotNull(deserialized);
            Assert.AreEqual(SceneGuid, deserialized.sceneAssetGuidHex);

#if UNITY_EDITOR
            Assert.IsNotNull(deserialized.sceneAsset);
            Assert.AreEqual(SceneName, deserialized.sceneAsset.name);
#endif // UNITY_EDITOR
        }
    }
}
