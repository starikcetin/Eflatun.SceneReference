using UnityEditor;

namespace Eflatun.SceneReference.Editor.Packages.com.eflatun.scenereference.Editor
{
    internal class ScaffoldingEnsurer : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            SceneDataMapsGenerator.EnsureScaffolding();
        }
    }
}
