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
                //Schedule a Map generation
                EditorApplication.update += DelayedMapGeneration;
            }
        }

        /// <summary>
        /// Waits for Unity editor to finish compiling.
        /// When that condition is met, unsubscribes from unity editor update and refreshes ScenesDataMaps.
        /// </summary>
        private static void DelayedMapGeneration()
        {
            if (EditorApplication.isCompiling || EditorApplication.isUpdating)
            {
                return;
            }
            EditorApplication.update -= DelayedMapGeneration;
            

            if (SettingsManager.SceneDataMaps.IsGenerationTriggerEnabled(SceneDataMapsGeneratorTriggers.AfterSceneAssetChange))
            {
                SceneDataMapsGenerator.Run();
            }
            else
            {
                EditorLogger.Warn($"Skipping scene data maps generation after scene asset changes. It is recommended to enable map generation after scene asset changes, as an outdated map can result in broken scene references in runtime. You can enable it in {SettingsManager.SettingsMenuPathForDisplay}.");
            }
        }
    }
}
