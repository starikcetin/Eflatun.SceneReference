using System.Linq;
using Eflatun.SceneReference.Editor.Utility;
using UnityEditor;

namespace Eflatun.SceneReference.Editor.MapGeneratorTriggers
{
    internal class SceneAssetPostprocessor : AssetPostprocessor
    {
        private static bool ShouldRun => (SettingsManager.MapGenerationTriggers.value & MapGenerationTriggers.AfterSceneAssetChange) == MapGenerationTriggers.AfterSceneAssetChange;

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            if(!ShouldRun)
            {
                return;
            }

            var allChangePaths = importedAssets.Concat(deletedAssets).Concat(movedAssets).Concat(movedFromAssetPaths);
            if (allChangePaths.Any(EditorUtils.IsSceneAssetPath))
            {
                MapGenerator.Run();
            }
        }
    }
}
