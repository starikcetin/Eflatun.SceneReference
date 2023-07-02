using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Eflatun.SceneReference.Editor.Toolbox
{
    public class AddToBuildTool : ITool
    {
        private readonly string _scenePath;
        private readonly UnityEngine.Object _sceneAsset;

        public AddToBuildTool(string scenePath, UnityEngine.Object sceneAsset)
        {
            _scenePath = scenePath;
            _sceneAsset = sceneAsset;
        }

        public void Draw()
        {
            if (GUILayout.Button("Add to build...", EditorStyles.toolbarButton))
            {
                Perform();
            }
        }

        public float GetHeight()
        {
            return EditorGUIUtility.singleLineHeight;
        }

        private void Perform()
        {
            var changed = false;
            var tempScenes = EditorBuildSettings.scenes.ToList();

            var title = "Add Scene to Build Settings?";
            var body = $"Would you like to add the following scene to build settings?\n\n{_scenePath}";

            switch (EditorUtility.DisplayDialogComplex(title, body, "Add to Build", "Cancel", "Open Build Settings"))
            {
                case 0:
                {
                    tempScenes.Add(new EditorBuildSettingsScene(_scenePath, true));
                    changed = true;
                    break;
                }
                case 1:
                {
                    // 1 is cancel
                    break;
                }
                case 2:
                {
                    EditorGUIUtility.PingObject(_sceneAsset);
                    EditorWindow.GetWindow<BuildPlayerWindow>();
                    break;
                }
            }

            if (changed)
            {
                EditorBuildSettings.scenes = tempScenes.ToArray();
            }
        }
    }
}
