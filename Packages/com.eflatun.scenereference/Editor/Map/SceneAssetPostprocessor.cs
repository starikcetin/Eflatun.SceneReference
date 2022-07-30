using System.Linq;
using UnityEditor;

namespace Eflatun.SceneReference.Editor.Map
{
    public class SceneAssetPostprocessor : AssetPostprocessor
    {
        private static bool ShouldRun => (SettingsManager.MapGenerationTriggers.value & GenerationTriggers.AfterSceneAssetChange) == GenerationTriggers.AfterSceneAssetChange;

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            if(!ShouldRun)
            {
                return;
            }

            var allChangePaths = importedAssets.Concat(deletedAssets).Concat(movedAssets).Concat(movedFromAssetPaths);
            if (allChangePaths.Any(EditorUtils.IsSceneAssetPath))
            {
                Generator.Run();
            }
        }
    }
}
