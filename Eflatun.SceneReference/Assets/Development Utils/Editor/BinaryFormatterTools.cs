using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Eflatun.SceneReference.DevelopmentUtils.Editor
{
    public static class BinaryFormatterTools
    {
        [MenuItem("Assets/BinaryFormatter Tools/Log BinaryFormatter Output (Base 64) of Scene References", validate = false)]
        private static void LogBinaryFormatterBase64s()
        {
            var objects = Selection.objects.Cast<SceneAsset>().ToArray();
            var sb = new StringBuilder();
            sb.AppendLine($"BinaryFormatter outputs (Base64) of SceneReferences constructed from the selected scenes ({objects.Length} items):");
            foreach (var obj in objects)
            {
                var path = AssetDatabase.GetAssetPath(obj);
                var sceneReference = new SceneReference(obj);
                var base64 = BinaryFormatterUtils.SerializeToBase64ViaBinaryFormatter(sceneReference);
                sb.AppendLine($"{path}: {base64}");
            }
            Debug.Log(sb.ToString());
        }

        [MenuItem("Assets/BinaryFormatter Tools/Log BinaryFormatter Output (Base 64) of Scene References", validate = true)]
        private static bool LogBinaryFormatterBase64s_Validate()
        {
            return Selection.objects.All(x => x is SceneAsset);
        }

        [MenuItem("Assets/BinaryFormatter Tools/Log and Copy BinaryFormatter Output (Base 64) of Scene Reference", validate = false)]
        private static void LogAndCopyBinaryFormatterBase64()
        {
            var selection = Selection.activeObject as SceneAsset;
            var path = AssetDatabase.GetAssetPath(selection);
            var sceneReference = new SceneReference(selection);
            var base64 = BinaryFormatterUtils.SerializeToBase64ViaBinaryFormatter(sceneReference);
            Debug.Log($"Copying the BinaryFormatter output (Base64) of SceneReference constructed from the selected scene ({path}): {base64}");
            EditorGUIUtility.systemCopyBuffer = base64;
        }

        [MenuItem("Assets/BinaryFormatter Tools/Log and Copy BinaryFormatter Output (Base 64) of Scene Reference", validate = true)]
        private static bool LogAndCopyBinaryFormatterBase64_Validate()
        {
            return Selection.count == 1 && Selection.activeObject is SceneAsset;
        }
    }
}
