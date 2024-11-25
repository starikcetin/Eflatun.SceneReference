using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Eflatun.SceneReference.Demo.EditorWindowUsage
{
    public class EditorWindowUsageDemo : EditorWindow
    {
        [MenuItem("Eflatun.SceneReference Demo/Editor Window Usage")]
        public static void ShowWindow()
        {
            GetWindow<EditorWindowUsageDemo>(false, "Eflatun.SceneReference Editor Window Usage Demo", true);
        }

        private const string FieldName_PrivateScene = "privateScene";
        private const string FieldName_PublicScene = "PublicScene";

        private DataHolder _dataHolder;
        private SerializedObject _serializedObject;
        private SerializedProperty _serializedPropPrivateScene;
        private SerializedProperty _SerializedPropPublicScene;
        private FieldInfo _fieldInfoPrivateScene;

        private string _newPathPrivateSceneFieldInfo;
        private string _newPathPrivateSceneBoxedValue;
        private string _newPathPublicSceneDirect;

        private void OnEnable()
        {
            _dataHolder = DataHolder.Instance;
            _serializedObject = new SerializedObject(_dataHolder);
            _serializedPropPrivateScene = _serializedObject.FindProperty(FieldName_PrivateScene);
            _SerializedPropPublicScene = _serializedObject.FindProperty(FieldName_PublicScene);
            _fieldInfoPrivateScene = _dataHolder.GetType().GetField(FieldName_PrivateScene, BindingFlags.NonPublic | BindingFlags.Instance);
        }

        private void OnGUI()
        {
            _serializedObject.Update();

            DrawPrivateScenePropertyField();

            EditorGUILayout.Space();
            DrawPrivateSceneReflection();

            EditorGUILayout.Space();
            DrawPrivateSceneBoxedValue();

            EditorGUILayout.Space();
            DrawPublicScenePropertyField();

            EditorGUILayout.Space();
            DrawPublicSceneDirect();

            _serializedObject.ApplyModifiedProperties();
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
            var sceneFieldValue = _fieldInfoPrivateScene.GetValue(_dataHolder) as SceneReference;
            var currentPath = GetPathOrNull(sceneFieldValue);
            EditorGUILayout.LabelField("Current Path", currentPath);
            _newPathPrivateSceneFieldInfo = EditorGUILayout.TextField("New Path", _newPathPrivateSceneFieldInfo);
            if (GUILayout.Button("Set Path"))
            {
                _fieldInfoPrivateScene.SetValue(_dataHolder, SceneReference.FromScenePath(_newPathPrivateSceneFieldInfo));
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
            var sceneFieldValue = _dataHolder.PublicScene;
            var currentPath = GetPathOrNull(sceneFieldValue);
            EditorGUILayout.LabelField("Current Path", currentPath);
            _newPathPublicSceneDirect = EditorGUILayout.TextField("New Path", _newPathPublicSceneDirect);
            if (GUILayout.Button("Set Path"))
            {
                _dataHolder.PublicScene = SceneReference.FromScenePath(_newPathPublicSceneDirect);
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
