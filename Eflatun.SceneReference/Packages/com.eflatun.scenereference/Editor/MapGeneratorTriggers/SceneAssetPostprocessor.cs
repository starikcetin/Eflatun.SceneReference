using Eflatun.SceneReference.Editor.Utility;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Eflatun.SceneReference.Editor.MapGeneratorTriggers
{
    internal class SceneAssetPostprocessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            var hasSceneChange = GetCreatedAssets(importedAssets)
                .Concat(deletedAssets)
                .Concat(movedAssets)
                .Concat(movedFromAssetPaths)
                .Any(EditorUtils.IsScenePath);

            if (hasSceneChange)
            {
                if (SettingsManager.SceneDataMaps.IsGenerationTriggerEnabled(SceneDataMapsGeneratorTriggers.AfterSceneAssetChange))
                {
                    SceneDataMapsGenerator.Run(false);
                }
                else
                {
                    EditorLogger.Warn($"Skipping scene data maps generation after scene asset changes. It is recommended to enable map generation after scene asset changes, as an outdated map can result in broken scene references in runtime. You can enable it in {SettingsManager.SettingsMenuPathForDisplay}.");
                }
            }
        }

        private static IEnumerable<string> GetCreatedAssets(string[] importedAssets)
        {
            // If we don't have a map, then we should treat all imported assets as created assets.
            var sceneGuidToPathMap = SceneGuidToPathMapProvider.GetSceneGuidToPathMap(false);
            return sceneGuidToPathMap == null ? importedAssets : importedAssets.Except(sceneGuidToPathMap.Values);
        }
    }
}
