using System.IO;
using UnityEditor;

namespace Eflatun.SceneReference.Editor.Map
{
    public class SceneModificationProcessor : AssetModificationProcessor
    {
        private static bool ShouldRun => (SettingsManager.MapGenerationTriggers.value & GenerationTriggers.AfterSceneAssetChange) == GenerationTriggers.AfterSceneAssetChange; 
        
        private static void OnWillCreateAsset(string assetName)
        {
            if(!ShouldRun)
            {
                return;
            }

            if (Path.GetExtension(assetName) == ".unity")
            {
                Generator.RunNextEditorFrame();
            }
        }

        private static AssetMoveResult OnWillMoveAsset(string sourcePath, string destinationPath)
        {
            if(!ShouldRun)
            {
                return AssetMoveResult.DidNotMove;
            }

            if (Path.GetExtension(destinationPath) == ".unity")
            {
                Generator.RunNextEditorFrame();
            }

            return AssetMoveResult.DidNotMove;
        }

        public static AssetDeleteResult OnWillDeleteAsset(string assetPath, RemoveAssetOptions removeAssetOptions)
        {
            if(!ShouldRun)
            {
                return AssetDeleteResult.DidNotDelete;
            }

            if (Path.GetExtension(assetPath) == ".unity")
            {
                Generator.RunNextEditorFrame();
            }

            return AssetDeleteResult.DidNotDelete;
        }
    }
}
