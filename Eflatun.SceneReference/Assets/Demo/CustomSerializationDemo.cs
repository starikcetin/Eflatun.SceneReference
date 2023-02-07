using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Eflatun.SceneReference;
using Newtonsoft.Json;
using UnityEngine;

public class CustomSerializationDemo : MonoBehaviour
{
    [SerializeField] private SceneReference original;

    private void Start()
    {
        ToJsonAndBack();
        ToBinaryAndBack();
    }

    private void ToBinaryAndBack()
    {
        Debug.Log("--- Binary ---");
        var bf = new BinaryFormatter();
        using var ms = new MemoryStream();

        bf.Serialize(ms, original);
        Debug.Log($"Binary serialization: {ms.ToArray().Aggregate("", (agg, cur) => agg + cur)}");

        ms.Position = 0;
        var deserialized = (SceneReference)bf.Deserialize(ms);
        Debug.Log("Deserialized path: " + (deserialized is { HasValue: true, IsInSceneGuidToPathMap: true } ? deserialized.Path : "<empty>"));
    }

    private void ToJsonAndBack()
    {
        Debug.Log("--- JSON ---");
        var json = JsonConvert.SerializeObject(original);
        Debug.Log($"JSON serialization: {json}");

        var deserialized = JsonConvert.DeserializeObject<SceneReference>(json);
        Debug.Log("Deserialized path: " + (deserialized is { HasValue: true, IsInSceneGuidToPathMap: true } ? deserialized.Path : "<empty>"));
    }
}
