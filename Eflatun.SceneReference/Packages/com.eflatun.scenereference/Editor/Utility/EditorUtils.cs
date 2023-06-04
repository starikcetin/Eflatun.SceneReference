using System.IO;

namespace Eflatun.SceneReference.Editor.Utility
{
    /// <summary>
    /// Editor utilities used by Eflatun.SceneReference.
    /// </summary>
    internal static class EditorUtils
    {
        /// <summary>
        /// Returns whether the addressables package is installed in the project.
        /// </summary>
        public static bool IsAddressablesPackagePresent =>
#if EFLATUN_SCENEREFERENCE_ADDRESSABLES_PACKAGE_PRESENT
            true
#else // EFLATUN_SCENEREFERENCE_ADDRESSABLES_PACKAGE_PRESENT
            false
#endif // EFLATUN_SCENEREFERENCE_ADDRESSABLES_PACKAGE_PRESENT
        ;

        /// <summary>
        /// Returns true if the given <paramref name="path"/> ends with the file extension ".unity".
        /// </summary>
        public static bool IsScenePath(this string path) => Path.GetExtension(path) == ".unity";
    }
}
