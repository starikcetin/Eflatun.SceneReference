using System.Linq;
using Eflatun.SceneReference.Editor.Utility;
using UnityEditor;

namespace Eflatun.SceneReference.Editor.MapGeneratorTriggers
{
    internal class SceneAssetPostprocessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            var hasSceneChange = importedAssets
                .Concat(deletedAssets)
                .Concat(movedAssets)
                .Concat(movedFromAssetPaths)
                .Any(EditorUtils.IsScenePath);

            if (hasSceneChange)
            {
                if (SettingsManager.SceneGuidToPathMap.IsGenerationTriggerEnabled(SceneGuidToPathMapGenerationTriggers.AfterSceneAssetChange))
                {
                    SceneGuidToPathMapGenerator.Run();
                }
                else
                {
                    Logger.Warn($"Skipping scene GUID to path map generation after scene asset changes. It is recommended to enable map generation after scene asset changes, as an outdated map can result in broken scene references in runtime. You can enable it in {SettingsManager.SettingsMenuPathForDisplay}.");
                }
            }
        }
    }
}
