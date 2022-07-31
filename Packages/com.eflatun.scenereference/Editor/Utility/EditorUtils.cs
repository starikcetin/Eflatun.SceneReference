using System.IO;

namespace Eflatun.SceneReference.Editor.Utility
{
    public static class EditorUtils
    {
        public static bool IsSceneAssetPath(this string path) => Path.GetExtension(path) == ".unity";
    }
}
