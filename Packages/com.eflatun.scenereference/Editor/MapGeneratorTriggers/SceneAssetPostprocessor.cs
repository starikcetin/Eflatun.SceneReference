using System.Linq;
using Eflatun.SceneReference.Editor.Utility;
using UnityEditor;
using UnityEngine;

namespace Eflatun.SceneReference.Editor.MapGeneratorTriggers
{
    internal class SceneAssetPostprocessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (!SettingsManager.IsGenerationTriggerEnabled(MapGenerationTriggers.AfterSceneAssetChange))
            {
                Debug.LogWarning("Skipping SceneReference map generation after scene asset changes. It is recommended to enable map generation after scene asset changes, as an outdated map can result in broken scene references in runtime. You can enable it in Project Settings/Eflatun/Scene Reference.");
                
                return;
            }

            var hasSceneChange = importedAssets
                .Concat(deletedAssets)
                .Concat(movedAssets)
                .Concat(movedFromAssetPaths)
                .Any(EditorUtils.IsSceneAssetPath);
            
            if (hasSceneChange)
            {
                MapGenerator.Run();
            }
        }
    }
}
