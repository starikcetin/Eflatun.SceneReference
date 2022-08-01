using System.IO;
using JetBrains.Annotations;

namespace Eflatun.SceneReference.Editor.Utility
{
    /// <summary>
    /// Editor utilities used by Eflatun.SceneReference.
    /// </summary>
    [PublicAPI]
    public static class EditorUtils
    {
        /// <summary>
        /// Returns true if the given <paramref name="path"/> ends with the file extension ".unity".
        /// </summary>
        public static bool IsSceneAssetPath(this string path) => Path.GetExtension(path) == ".unity";
    }
}
