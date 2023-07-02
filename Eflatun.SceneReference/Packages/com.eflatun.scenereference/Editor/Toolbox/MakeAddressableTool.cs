#if ESR_ADDRESSABLES

using System;
using Eflatun.SceneReference.Editor.Utility;
using UnityEditor;
using UnityEngine;

namespace Eflatun.SceneReference.Editor.Toolbox
{
    /// <summary>
    /// Before: Not in build and not addressable.<br/>
    /// After: Addressable.
    /// </summary>
    internal class MakeAddressableTool : ITool
    {
        private readonly string _scenePath;
        private readonly string _sceneGuid;
        private readonly UnityEngine.Object _sceneAsset;

        public MakeAddressableTool(string scenePath, string sceneGuid, UnityEngine.Object sceneAsset)
        {
            _scenePath = scenePath;
            _sceneGuid = sceneGuid;
            _sceneAsset = sceneAsset;
        }

        public void Draw(Action closeToolbox)
        {
            if (GUILayout.Button("Make addressable...", EditorStyles.toolbarButton))
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
            var title = "Make the Scene Addressable?";
            var body = $"Would you like to make the following scene addressable?\n\n{_scenePath}";

            switch (EditorUtility.DisplayDialogComplex(title, body, "Add to Default Group", "Cancel", "Open Addressable Groups Window"))
            {
                case 0:
                {
                    EditorUtils.AddToDefaultAddressableGroup(_sceneGuid);
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
                    EditorWindow.GetWindow(EditorUtils.AddressablesGroupsWindowType);
                    break;
                }
            }
        }
    }
}

#endif
