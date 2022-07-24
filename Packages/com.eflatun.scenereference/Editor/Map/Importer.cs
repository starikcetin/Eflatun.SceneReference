using System.IO;
using System.Linq;
using UnityEditor;

namespace Eflatun.SceneReference.Editor.Map
{
    public static class Importer
    {
        [MenuItem("Eflatun/SceneReference/Import Generated Assembly")]
        private static void ImportGeneratedAssembly()
        {
            try
            {
                EditorUtility.DisplayProgressBar("Importing generated assembly", "Importing generated assembly", 0);
                EditorApplication.LockReloadAssemblies();
                CopyDirectoryRecursive(Paths.SourceFolderPath, Paths.DestinationFolderPath);
            }
            finally
            {
                AssetDatabase.Refresh();
                EditorApplication.UnlockReloadAssemblies();
                EditorUtility.ClearProgressBar();
            }
        }

        private static void CopyDirectoryRecursive(string sourcePath, string destinationPath)
        {
            Directory.CreateDirectory(destinationPath);

            foreach (var dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));
            }

            foreach (var newPath in Directory.GetFiles(sourcePath, "*.*",SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, destinationPath), true);
            }
        }

        internal static bool CheckExistsAll()
        {
            return Directory.GetDirectories(Paths.SourceFolderPath, "*", SearchOption.AllDirectories)
                .Concat(Directory.GetFiles(Paths.SourceFolderPath, "*.*",SearchOption.AllDirectories))
                .Select(x => Path.GetRelativePath(Paths.SourceFolderPath, x)) // make relative to source
                .Select(x => Path.Combine(Paths.DestinationFolderPath, x)) // make destination path
                .All(FileOrDirectoryExists);
        }

        private static bool FileOrDirectoryExists(string name)
        {
            return Directory.Exists(name) || File.Exists(name);
        }
    }
}
