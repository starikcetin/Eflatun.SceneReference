using System.IO;
using UnityEditor;
using UnityEngine;

namespace Eflatun.SceneReference.Editor.Map
{
    public class SceneModificationProcessor : AssetModificationProcessor
    {
        private static void OnWillCreateAsset(string assetName)
        {
            Debug.Log("create: " + assetName);

            if (Path.GetExtension(assetName) == ".unity")
            {
                GeneratorRunner.SetChanged();
            }
        }

        private static AssetMoveResult OnWillMoveAsset(string sourcePath, string destinationPath)
        {
            Debug.Log("move to: " + destinationPath);

            if (Path.GetExtension(destinationPath) == ".unity")
            {
                GeneratorRunner.SetChanged();
            }

            return AssetMoveResult.DidNotMove;
        }

        public static AssetDeleteResult OnWillDeleteAsset(string assetPath, RemoveAssetOptions removeAssetOptions)
        {
            Debug.Log("delete: " + assetPath);

            if (Path.GetExtension(assetPath) == ".unity")
            {
                GeneratorRunner.SetChanged();
            }

            return AssetDeleteResult.DidNotDelete;
        }
    }
}
