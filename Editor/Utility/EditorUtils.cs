using System.IO;

namespace Eflatun.SceneReference.Editor.Utility
{
    /// <summary>
    /// Editor utilities used by Eflatun.SceneReference.
    /// </summary>
    internal static class EditorUtils
    {
        /// <summary>
        /// Returns true if the given <paramref name="path"/> ends with the file extension ".unity".
        /// </summary>
        public static bool IsScenePath(this string path) => Path.GetExtension(path) == ".unity";
    }
}
