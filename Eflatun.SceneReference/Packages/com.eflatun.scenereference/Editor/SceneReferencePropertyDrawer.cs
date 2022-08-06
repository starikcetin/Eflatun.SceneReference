using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Eflatun.SceneReference.Editor
{
    /// <summary>
    /// Custom property drawer for <see cref="SceneReference"/>.
    /// </summary>
    [PublicAPI]
    [CustomPropertyDrawer(typeof(SceneReference))]
    public class SceneReferencePropertyDrawer : PropertyDrawer
    {
        private SerializedProperty _sceneAssetProperty;
        private SerializedProperty _sceneAssetGuidHexProperty;

        private string SceneAssetGuidHex => _sceneAssetGuidHexProperty.stringValue;

        private Object SceneAsset
        {
            get => _sceneAssetProperty.objectReferenceValue;
            set
            {
                _sceneAssetProperty.objectReferenceValue = value;
                
                var sceneAssetPath = AssetDatabase.GetAssetPath(value);
                var sceneAssetGuid = AssetDatabase.GUIDFromAssetPath(sceneAssetPath);
                _sceneAssetGuidHexProperty.stringValue = sceneAssetGuid.ToString();
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _sceneAssetProperty = property.FindPropertyRelative(nameof(SceneReference.sceneAsset));
            _sceneAssetGuidHexProperty = property.FindPropertyRelative(nameof(SceneReference.sceneAssetGuidHex));
            
            var prevColor = GUI.color;
            if (SceneAsset)
            {
                var sceneInBuildSettings = EditorBuildSettings.scenes.FirstOrDefault(x => x.guid.ToString() == SceneAssetGuidHex);
                if (sceneInBuildSettings == null)
                {
                    GUI.color = Color.red;
                }
                else if (!sceneInBuildSettings.enabled)
                {
                    GUI.color = Color.yellow;
                }   
            }
            
            EditorGUI.BeginProperty(position, GUIContent.none, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            SceneAsset = EditorGUI.ObjectField(position, SceneAsset, typeof(SceneAsset), false);

            GUI.color = prevColor;
            EditorGUI.EndProperty();
        }
    }
}
