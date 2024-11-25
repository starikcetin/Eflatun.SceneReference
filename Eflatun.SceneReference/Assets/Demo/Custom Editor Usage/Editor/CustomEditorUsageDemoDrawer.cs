using NUnit.Framework.Internal;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Eflatun.SceneReference.Demo.CustomEditorUsage.Editor
{
    [CustomEditor(typeof(CustomEditorUsageDemo))]
    public class CustomEditorUsageDemoDrawer : UnityEditor.Editor
    {
        private const string FieldName_PrivateScene = "privateScene";
        private const string FieldName_PublicScene = "PublicScene";

        private CustomEditorUsageDemo _targetCasted;
        private SerializedProperty _serializedPropPrivateScene;
        private SerializedProperty _SerializedPropPublicScene;
        private FieldInfo _fieldInfoPrivateScene;

        private string _newPathPrivateSceneFieldInfo;
        private string _newPathPrivateSceneBoxedValue;
        private string _newPathPublicSceneDirect;

        private void OnEnable()
        {
            _targetCasted = (CustomEditorUsageDemo)target;
            _serializedPropPrivateScene = serializedObject.FindProperty(FieldName_PrivateScene);
            _SerializedPropPublicScene = serializedObject.FindProperty(FieldName_PublicScene);
            _fieldInfoPrivateScene = target.GetType().GetField(FieldName_PrivateScene, BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            DrawPrivateScenePropertyField();
            
            EditorGUILayout.Space();
            DrawPrivateSceneReflection();

            EditorGUILayout.Space();
            DrawPrivateSceneBoxedValue();

            EditorGUILayout.Space();
            DrawPublicScenePropertyField();
            
            EditorGUILayout.Space();
            DrawPublicSceneDirect();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawPrivateScenePropertyField()
        {
            EditorGUILayout.LabelField("Private Field - PropertyField");
            EditorGUILayout.PropertyField(_serializedPropPrivateScene);
        }

        private void DrawPublicScenePropertyField()
        {
            EditorGUILayout.LabelField("Public Field - PropertyField");
            EditorGUILayout.PropertyField(_SerializedPropPublicScene);
        }

        private void DrawPrivateSceneReflection()
        {
            EditorGUILayout.LabelField("Private Field - Via Reflection");
            var sceneFieldValue = _fieldInfoPrivateScene.GetValue(_targetCasted) as SceneReference;
            var currentPath = GetPathOrNull(sceneFieldValue);
            EditorGUILayout.LabelField("Current Path", currentPath);
            _newPathPrivateSceneFieldInfo = EditorGUILayout.TextField("New Path", _newPathPrivateSceneFieldInfo);
            if (GUILayout.Button("Set Path"))
            {
                _fieldInfoPrivateScene.SetValue(_targetCasted, SceneReference.FromScenePath(_newPathPrivateSceneFieldInfo));
            }
        }

        private void DrawPrivateSceneBoxedValue()
        {
            EditorGUILayout.LabelField("Private Field - Via boxedValue");
#if UNITY_2022_1_OR_NEWER
            var boxedValue = _serializedPropPrivateScene.boxedValue as SceneReference;
            var currentPath = GetPathOrNull(boxedValue);
            EditorGUILayout.LabelField("Current Path", currentPath);
            _newPathPrivateSceneBoxedValue = EditorGUILayout.TextField("New Path", _newPathPrivateSceneBoxedValue);
            if (GUILayout.Button("Set Path"))
            {
                _serializedPropPrivateScene.boxedValue = SceneReference.FromScenePath(_newPathPrivateSceneBoxedValue);
            }
#else // UNITY_2022_1_OR_NEWER
            EditorGUILayout.LabelField("SerializedProperty.boxedValue is only available in Unity 2022.1 or newer.");
#endif // UNITY_2022_1_OR_NEWER
        }

        private void DrawPublicSceneDirect()
        {
            EditorGUILayout.LabelField("Public Field - Direct Access");
            var sceneFieldValue = _targetCasted.PublicScene;
            var currentPath = GetPathOrNull(sceneFieldValue);
            EditorGUILayout.LabelField("Current Path", currentPath);
            _newPathPublicSceneDirect = EditorGUILayout.TextField("New Path", _newPathPublicSceneDirect);
            if (GUILayout.Button("Set Path"))
            {
                _targetCasted.PublicScene = SceneReference.FromScenePath(_newPathPublicSceneDirect);
            }
        }

        private string GetPathOrNull(SceneReference sceneRef)
        {
            if (sceneRef == null)
            {
                return null;
            }

            if (sceneRef.UnsafeReason == SceneReferenceUnsafeReason.Empty)
            {
                return null;
            }

            return sceneRef.Path;
        }
    }
}
