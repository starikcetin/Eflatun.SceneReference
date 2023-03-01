using System.IO;
using UnityEditor;
using UnityEngine;

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

        /// <summary>
        /// Centers the given editor window relative to the the main editor window.
        /// </summary>
        public static void CenterOnMainWin(this EditorWindow window, Vector2 initialSize)
        {
            var mainWinCenter = EditorGUIUtility.GetMainWindowPosition();
            window.position = new Rect(mainWinCenter.center - initialSize / 2f, initialSize);
        }
    }
}
