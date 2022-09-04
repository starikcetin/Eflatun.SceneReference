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
        /// <exception cref="InvalidSceneReferenceException">
        /// Throws if <see cref="IsInSceneGuidToPathMap"/> is <c>false</c>.
        /// </exception>
        public string Path =>
            SceneGuidToPathMapProvider.SceneGuidToPathMap.TryGetValue(sceneAssetGuidHex, out var scenePath)
                ? scenePath
                : throw new InvalidSceneReferenceException();

        /// <summary>
        /// Build index of the scene.
        /// </summary>
        /// <exception cref="InvalidSceneReferenceException">
        /// Throws if <see cref="IsInSceneGuidToPathMap"/> is <c>false</c>.
        /// </exception>
        /// <remarks>
        /// This property will return <c>-1</c> if <see cref="IsInBuildAndEnabled"/> is <c>false</c>.
        /// </remarks>
        public int BuildIndex => SceneUtility.GetBuildIndexByScenePath(Path);

        /// <summary>
        /// Name of the scene asset.
        /// </summary>
        /// <exception cref="InvalidSceneReferenceException">
        /// Throws if <see cref="IsInSceneGuidToPathMap"/> is <c>false</c>.
        /// </exception>
        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);

        /// <summary>
        /// The <see cref="Scene"/> struct for this scene. Only valid if the scene is currently loaded.
        /// </summary>
        /// <exception cref="InvalidSceneReferenceException">
        /// Throws if <see cref="IsInSceneGuidToPathMap"/> is <c>false</c>.
        /// </exception>
        public Scene LoadedScene => SceneManager.GetSceneByPath(Path);

        /// <summary>
        /// Does the scene GUID to path map contain the scene?
        /// </summary>
        /// <remarks>
        /// This property alone does not communicate if this <see cref="SceneReference"/> is safe to use. Check <see cref="IsSafeToUse"/> for that purpose.
        /// </remarks>
        /// <seealso cref="IsInBuildAndEnabled"/>
        public bool IsInSceneGuidToPathMap => SceneGuidToPathMapProvider.SceneGuidToPathMap.ContainsKey(AssetGuidHex);

        /// <summary>
        /// Is the scene added and enabled in Build Settings?
        /// </summary>
        /// <exception cref="InvalidSceneReferenceException">
        /// Throws if <see cref="IsInSceneGuidToPathMap"/> is <c>false</c>.
        /// </exception>
        /// <remarks>
        /// This property alone does not communicate if this <see cref="SceneReference"/> is safe to use. Check <see cref="IsSafeToUse"/> for that purpose.
        /// </remarks>
        public bool IsInBuildAndEnabled => BuildIndex != -1;

        /// <summary>
        /// Is this <see cref="SceneReference"/> safe to use?
        /// </summary>
        /// <remarks>
        /// This property is equivalent to checking both <see cref="IsInSceneGuidToPathMap"/> and <see cref="IsInBuildAndEnabled"/>, with slightly better performance.
        /// </remarks>
        public bool IsSafeToUse =>
            SceneGuidToPathMapProvider.SceneGuidToPathMap.TryGetValue(AssetGuidHex, out var path)
            && SceneUtility.GetBuildIndexByScenePath(path) != -1;
    }
}
