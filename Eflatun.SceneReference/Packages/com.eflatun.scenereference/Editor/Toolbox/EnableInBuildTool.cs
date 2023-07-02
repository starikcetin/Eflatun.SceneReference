using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Eflatun.SceneReference.Editor.Toolbox
{
    public class EnableInBuildTool : ITool
    {
        private readonly string _scenePath;
        private readonly string _sceneGuid;
        private readonly Object _sceneAsset;

        public EnableInBuildTool(string scenePath, string sceneGuid, UnityEngine.Object sceneAsset)
        {
            _scenePath = scenePath;
            _sceneGuid = sceneGuid;
            _sceneAsset = sceneAsset;
        }

        public void Draw()
        {
            if (GUILayout.Button("Enable in build...", EditorStyles.toolbarButton))
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

            var title = "Enable Scene in Build Settings?";
            var body = $"Would you like to enable the following scene in build settings?\n\n{_scenePath}";

            switch (EditorUtility.DisplayDialogComplex(title, body, "Enable in Build", "Cancel", "Open Build Settings"))
            {
                case 0:
                {
                    tempScenes.Single(x => x.guid.ToString() == _sceneGuid).enabled = true;
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
