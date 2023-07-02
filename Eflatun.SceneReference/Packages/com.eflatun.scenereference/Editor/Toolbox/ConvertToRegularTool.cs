using UnityEditor;

namespace Eflatun.SceneReference.Editor.Toolbox
{
    public class ConvertToRegularTool : ITool
    {
        public void Draw()
        {
        }

        public float GetHeight()
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
