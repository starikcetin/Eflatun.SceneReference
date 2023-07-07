using System;
using Eflatun.SceneReference.Editor.Utility;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Eflatun.SceneReference.Editor.Toolbox
{
    /// <summary>
    /// Before: In build and disabled.<br/>
    /// After: In build and enabled.
    /// </summary>
    internal class EnableInBuildTool : ITool
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

        public void Draw(Action closeToolbox)
        {
            if (GUILayout.Button("Enable in build...", EditorStyles.toolbarButton))
            {
                // Prevents an editor crash. https://issuetracker.unity3d.com/issues/crash-on-guiview-oninputevent-when-opening-an-editorwindow-from-a-genericmenu-in-a-popup-window
                closeToolbox.Invoke();

                Perform();
            }
        }

        public float GetHeight()
        {
            return EditorStyles.toolbarButton.fixedHeight;
        }

        private void Perform()
        {
            var title = "Enable Scene in Build Settings?";
            var body = $"Would you like to enable the following scene in build settings?\n\n{_scenePath}";

            switch (EditorUtility.DisplayDialogComplex(title, body, "Enable in Build", "Cancel", "Open Build Settings"))
            {
                case 0:
                {
                    EditorUtils.EnableSceneInBuild(_sceneGuid);
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
        }
    }
}
