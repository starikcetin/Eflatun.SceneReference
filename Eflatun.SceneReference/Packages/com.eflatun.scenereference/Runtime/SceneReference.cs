using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Eflatun.SceneReference
{
    /// <summary>
    /// Provides a strongly-typed reference to a scene. Can be used in both editor and runtime.
    /// </summary>
    [PublicAPI]
    [Serializable]
    public class SceneReference
    {
        [SerializeField] internal UnityEngine.Object sceneAsset;
        [SerializeField] internal string sceneAssetGuidHex;

        /// <summary>
        /// GUID of the scene asset in hex format. 
        /// </summary>
        public string AssetGuidHex => sceneAssetGuidHex;

        /// <summary>
        /// Path to the scene asset.
        /// </summary>
        /// <exception cref="InvalidSceneReferenceException"></exception>
        public string Path =>
            SceneGuidToPathMapProvider.SceneGuidToPathMap.TryGetValue(sceneAssetGuidHex, out var scenePath)
                ? scenePath
                : throw new InvalidSceneReferenceException();

        /// <summary>
        /// Build index of the scene.
        /// </summary>
        /// <exception cref="InvalidSceneReferenceException"></exception>
        public int BuildIndex => SceneUtility.GetBuildIndexByScenePath(Path);

        /// <summary>
        /// Name of the scene asset.
        /// </summary>
        /// <exception cref="InvalidSceneReferenceException"></exception>
        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);

        /// <summary>
        /// The <see cref="Scene"/> struct for this scene. Only valid if the scene is currently loaded.
        /// </summary>
        /// <exception cref="InvalidSceneReferenceException"></exception>
        public Scene LoadedScene => SceneManager.GetSceneByPath(Path);

        /// <summary>
        /// Does the Scene GUID to Path Map contain <see cref="AssetGuidHex"/>?
        /// </summary>
        public bool IsSceneValid => SceneGuidToPathMapProvider.SceneGuidToPathMap.ContainsKey(sceneAssetGuidHex);

        /// <summary>
        /// Is the scene added and enabled in Build Settings?
        /// </summary>
        /// <exception cref="InvalidSceneReferenceException"></exception>
        public bool IsInBuildAndEnabled => BuildIndex != -1;

        /// <summary>
        /// Does the Scene GUID to Path Map contain <see cref="AssetGuidHex"/> AND is the scene added and enabled in Build Settings?
        /// </summary>
        /// <remarks>
        /// If this property is true, then this <see cref="SceneReference"/> is safe to use.
        /// </remarks>
        public bool IsSafeToUse =>
            SceneGuidToPathMapProvider.SceneGuidToPathMap.TryGetValue(sceneAssetGuidHex, out var path)
            && SceneUtility.GetBuildIndexByScenePath(path) != -1;
    }
}
