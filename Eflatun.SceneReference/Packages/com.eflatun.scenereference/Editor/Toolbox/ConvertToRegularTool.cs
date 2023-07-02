using System;
using UnityEditor;

namespace Eflatun.SceneReference.Editor.Toolbox
{
    /// <summary>
    /// Before: Addressable.<br/>
    /// After: In build and enabled.
    /// </summary>
    internal class ConvertToRegularTool : ITool
    {
        public void Draw(Action closeToolbox)
        {
        }

        public float GetHeight()
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
