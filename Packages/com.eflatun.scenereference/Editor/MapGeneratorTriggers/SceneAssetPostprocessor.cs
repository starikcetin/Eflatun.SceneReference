using System.Collections.Generic;
using System.Linq;
using Eflatun.SceneReference.Editor.Utility;
using UnityEditor;

namespace Eflatun.SceneReference.Editor.MapGeneratorTriggers
{
    internal class SceneAssetPostprocessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (!SettingsManager.IsGenerationTriggerEnabled(MapGenerationTriggers.AfterSceneAssetChange))
            {
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
