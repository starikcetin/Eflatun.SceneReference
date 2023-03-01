using System.Linq;
using Eflatun.SceneReference.Editor.Utility;
using UnityEditor;
using UnityEngine;

namespace Eflatun.SceneReference.Editor
{
    internal class FixInBuildEditorWindow : EditorWindow
    {
        private static UnityEngine.Object _asset;
        private static string _path;
        private static string _guid;
        private static SceneBuildSettingsState _buildSettingsState;
        private static string _body;
        private static FixInBuildEditorWindow _instance;

        public static void Display(UnityEngine.Object asset, string path, string guid, SceneBuildSettingsState buildSettingsState)
        {
            _asset = asset;
            _path = path;
            _guid = guid;
            _buildSettingsState = buildSettingsState;

            string titleText;
            switch (_buildSettingsState)
            {
                case SceneBuildSettingsState.NotIncluded:
                    titleText = "Add Scene to Build Settings?";
                    _body = "Would you like to add the following scene to build settings?";
                    break;
                case SceneBuildSettingsState.Disabled:
                    titleText = "Enable Scene in Build Settings?";
                    _body = "Would you like to enable the following scene in build settings?";
                    break;
                case SceneBuildSettingsState.None:
                case SceneBuildSettingsState.Enabled:
                default:
                    throw SceneReferenceInternalEditorException.UnexpectedSceneBuildSettingsState("18629207", _buildSettingsState);
            }

            _instance = CreateInstance<FixInBuildEditorWindow>();
            _instance.titleContent = new GUIContent(titleText);
            var size = new Vector2(600f, 100f);
            _instance.maxSize = size;
            _instance.minSize = size;
            _instance.CenterOnMainWin(size);
            _instance.ShowModalUtility();
        }

        private void OnGUI()
        {
            const int uniformPadding = 10;
            var padding = new RectOffset(uniformPadding, uniformPadding, 0, 0);
            var area = new Rect(padding.right, padding.top, position.width - (padding.right + padding.left), position.height - (padding.top + padding.bottom));

            // padding
            GUILayout.BeginArea(area);

            // main
            EditorGUILayout.BeginVertical();

            EditorGUILayout.Space();

            EditorGUILayout.LabelField(_body, EditorStyles.wordWrappedLabel);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(_path, EditorStyles.wordWrappedLabel);

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            // buttons
            EditorGUILayout.BeginHorizontal();

            if (_buildSettingsState == SceneBuildSettingsState.Disabled)
            {
                if (GUILayout.Button("Enable in Build"))
                {
                    OnEnableInBuild();
                }
            }

            if (_buildSettingsState == SceneBuildSettingsState.NotIncluded)
            {
                if (GUILayout.Button("Add to Build as Enabled"))
                {
                    OnAddToBuild(asEnabled: true);
                }

                if (GUILayout.Button("Add to Build as Disabled"))
                {
                    OnAddToBuild(asEnabled: false);
                }
            }

            if (GUILayout.Button("Open Build Settings"))
            {
                OnOpenBuildSettings();
            }

            if (GUILayout.Button("Cancel"))
            {
                OnCancel();
            }

            // buttons
            EditorGUILayout.EndHorizontal();

            // main
            EditorGUILayout.EndVertical();

            // padding
            GUILayout.EndArea();
        }

        private void OnCancel()
        {
            Close();
        }

        private void OnOpenBuildSettings()
        {
            EditorGUIUtility.PingObject(_asset);
            GetWindow<BuildPlayerWindow>();

            Close();
        }

        private void OnAddToBuild(bool asEnabled)
        {
            var tempScenes = EditorBuildSettings.scenes.ToList();
            tempScenes.Add(new EditorBuildSettingsScene(_path, asEnabled));
            EditorBuildSettings.scenes = tempScenes.ToArray();

            Close();
        }

        private void OnEnableInBuild()
        {
            var tempScenes = EditorBuildSettings.scenes.ToList();
            tempScenes.Single(x => x.guid.ToString() == _guid).enabled = true;
            EditorBuildSettings.scenes = tempScenes.ToArray();

            Close();
        }
    }
}
