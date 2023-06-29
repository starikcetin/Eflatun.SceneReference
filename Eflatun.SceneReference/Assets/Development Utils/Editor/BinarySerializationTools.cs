using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Eflatun.SceneReference.DevelopmentUtils.Editor
{
    public static class BinarySerializationTools
    {
        [MenuItem("Assets/Binary Serialization Tools/Log Binary (Base 64) of Scene References", validate = false)]
        private static void LogBinaryBase64s()
        {
            var objects = Selection.objects.Cast<SceneAsset>().ToArray();
            var sb = new StringBuilder();
            sb.AppendLine($"Binary Base64s of SceneReferences constructed from the selected scenes ({objects.Length} items):");
            foreach (var obj in objects)
            {
                var path = AssetDatabase.GetAssetPath(obj);
                var base64 = GetBinaryBase64(obj);
                sb.AppendLine($"{path}: {base64}");
            }
            Debug.Log(sb.ToString());
        }

        [MenuItem("Assets/Binary Serialization Tools/Log Binary (Base 64) of Scene References", validate = true)]
        private static bool LogBinaryBase64s_Validate()
        {
            return Selection.objects.All(x => x is SceneAsset);
        }

        [MenuItem("Assets/Binary Serialization Tools/Log and Copy Binary (Base 64) of Scene Reference", validate = false)]
        private static void LogAndCopyBinaryBase64()
        {
            var selection = Selection.activeObject as SceneAsset;
            var path = AssetDatabase.GetAssetPath(selection);
            var base64 = GetBinaryBase64(selection);
            Debug.Log($"Copying the Binary Base64 of SceneReference constructed from the selected scene ({path}): {base64}");
            EditorGUIUtility.systemCopyBuffer = base64;
        }

        [MenuItem("Assets/Binary Serialization Tools/Log and Copy Binary (Base 64) of Scene Reference", validate = true)]
        private static bool LogAndCopyBinaryBase64_Validate()
        {
            return Selection.count == 1 && Selection.activeObject is SceneAsset;
        }

        private static string GetBinaryBase64(SceneAsset scene)
        {
            var sr = new SceneReference(scene);
            return BinarySerializationUtils.SerializeToBinaryBase64(sr);
        }
    }
}
