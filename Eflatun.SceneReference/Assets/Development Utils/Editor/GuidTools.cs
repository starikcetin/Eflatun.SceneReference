using System.Text;
using UnityEditor;
using UnityEngine;

namespace Eflatun.SceneReference.DevelopmentUtils.Editor
{
    public static class GuidTools
    {
        [MenuItem("Assets/GUID Tools/Log GUIDs")]
        private static void LogGuids()
        {
            var guids = Selection.assetGUIDs;
            var sb = new StringBuilder();
            sb.AppendLine($"GUIDs of the selected assets ({guids.Length} items):");
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                sb.AppendLine($"{path}: {guid}");
            }
            Debug.Log(sb.ToString());
        }

        [MenuItem("Assets/GUID Tools/Log and Copy GUID", validate = false)]
        private static void LogAndCopyGuid()
        {
            var selection = Selection.activeObject;
            var path = AssetDatabase.GetAssetPath(selection);
            var guid = AssetDatabase.GUIDFromAssetPath(path);
            Debug.Log($"Copying the GUID of the selected asset ({path}): {guid}");
            EditorGUIUtility.systemCopyBuffer = guid.ToString();
        }

        [MenuItem("Assets/GUID Tools/Log and Copy GUID", validate = true)]
        private static bool LogAndCopyGuid_Validate()
        {
            return Selection.count == 1;
        }
    }
}
