using UnityEditor;
using UnityEngine;

namespace Eflatun.SceneReference.Editor
{
    /// <summary>
    /// Custom property drawer for <see cref="SceneReference"/>.
    /// </summary>
    [CustomPropertyDrawer(typeof(SceneReference))]
    public class SceneReferencePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, GUIContent.none, property);

            var sceneAssetProperty = property.FindPropertyRelative(nameof(SceneReference.sceneAsset));
            var sceneAssetGuidHexProperty = property.FindPropertyRelative(nameof(SceneReference.sceneAssetGuidHex));

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            if (sceneAssetProperty != null && sceneAssetGuidHexProperty != null)
            {
                var sceneAsset = EditorGUI.ObjectField(position, sceneAssetProperty.objectReferenceValue, typeof(SceneAsset), false);
                sceneAssetProperty.objectReferenceValue = sceneAsset;

                var sceneAssetPath = AssetDatabase.GetAssetPath(sceneAsset);
                var sceneAssetGuid = AssetDatabase.GUIDFromAssetPath(sceneAssetPath);
                sceneAssetGuidHexProperty.stringValue = sceneAssetGuid.ToString();
            }
            else
            {
                Logger.Error($"Could not locate properties of a {nameof(SceneAsset)} while trying to draw!");
            }

            EditorGUI.EndProperty();
        }
    }
}
