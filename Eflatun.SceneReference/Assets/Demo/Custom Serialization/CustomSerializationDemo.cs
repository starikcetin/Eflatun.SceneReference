using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using UnityEngine;

namespace Eflatun.SceneReference.Demo.CustomSerialization
{
    public class CustomSerializationDemo : MonoBehaviour
    {
        [SerializeField] private SceneReference original;

        private void Start()
        {
            ToJsonAndBack();
            ToBinaryAndBack();
            ToXmlAndBack();
        }

        private void ToBinaryAndBack()
        {
            Debug.Log("--- Binary ---");
            var bf = new BinaryFormatter();
            using var ms = new MemoryStream();

            bf.Serialize(ms, original);
            var bytes = ms.ToArray();
            var base64 = Convert.ToBase64String(bytes);
            Debug.Log($"Binary serialization (base64): {base64}");

            ms.Position = 0;
            var deserialized = (SceneReference)bf.Deserialize(ms);
            Debug.Log("Deserialized path: " + (deserialized.State != SceneReferenceState.Unsafe ? deserialized.Path : "<empty>"));
        }

        private void ToJsonAndBack()
        {
            Debug.Log("--- JSON ---");
            var json = JsonConvert.SerializeObject(original);
            Debug.Log($"JSON serialization: {json}");

            var deserialized = JsonConvert.DeserializeObject<SceneReference>(json);
            Debug.Log("Deserialized path: " + (deserialized.State != SceneReferenceState.Unsafe ? deserialized.Path : "<empty>"));
        }

        private void ToXmlAndBack()
        {
            Debug.Log("--- XML ---");
            var xmlSerializer = new XmlSerializer(typeof(SceneReference));
            var sb = new StringBuilder();
            using var xmlWriter = XmlWriter.Create(sb);
            xmlSerializer.Serialize(xmlWriter, original);
            var xml = sb.ToString();
            Debug.Log($"XML serialization: {xml}");

            using var stringReader = new StringReader(xml);
            using var xmlReader = XmlReader.Create(stringReader);
            var deserialized = (SceneReference)xmlSerializer.Deserialize(xmlReader);
            Debug.Log("Deserialized path: " + (deserialized.State != SceneReferenceState.Unsafe ? deserialized.Path : "<empty>"));
        }
    }
}
