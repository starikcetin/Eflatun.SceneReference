#if ESR_ADDRESSABLES

using System;
using UnityEditor;

namespace Eflatun.SceneReference.Editor.Toolbox
{
    /// <summary>
    /// Before: In build (enabled or disabled).<br/>
    /// After: Addressable.
    /// </summary>
    internal class ConvertToAddressableTool : ITool
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

#endif
