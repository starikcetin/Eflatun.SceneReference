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
        /// <remarks>
        /// Will be all 0s if the scene asset is null or invalid. Check <see cref="IsValidSceneAsset"/> to see if the
        /// scene asset is non-null and valid.
        /// </remarks>
        public string AssetGuidHex => sceneAssetGuidHex;

        /// <summary>
        /// Path to the scene asset.
        /// </summary>
        /// <exception cref="InvalidSceneAssetException"></exception>
        /// <exception cref="OutdatedSceneGuidToPathMapException"></exception>
        public string Path
        {
            get
            {
                if (!IsValidSceneAsset)
                {
                    throw new InvalidSceneAssetException("The scene assigned to this SceneReference is null or invalid.");
                }

                if (!SceneGuidToPathMapProvider.SceneGuidToPathMap.TryGetValue(sceneAssetGuidHex, out var scenePath))
                {
                    throw new OutdatedSceneGuidToPathMapException("The scene assigned to this SceneReference is valid, but it is not in the scene in the scene GUID to path map. Make sure the scene GUID to path map is up to date.");
                }

                return scenePath;
            }
        }

        /// <summary>
        /// Build index of the scene.
        /// </summary>
        /// <exception cref="InvalidSceneAssetException"></exception>
        /// <exception cref="OutdatedSceneGuidToPathMapException"></exception>
        public int BuildIndex => SceneUtility.GetBuildIndexByScenePath(Path);

        /// <summary>
        /// Name of the scene asset.
        /// </summary>
        /// <exception cref="InvalidSceneAssetException"></exception>
        /// <exception cref="OutdatedSceneGuidToPathMapException"></exception>
        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);

        /// <summary>
        /// The <see cref="Scene"/> struct for this scene. Only valid if the scene is currently loaded.
        /// </summary>
        /// <exception cref="InvalidSceneAssetException"></exception>
        /// <exception cref="OutdatedSceneGuidToPathMapException"></exception>
        public Scene LoadedScene => SceneManager.GetSceneByPath(Path);

        /// <summary>
        /// Do we have a non-null and valid scene asset?
        /// </summary>
        /// <remarks>
        /// This property alone does not communicate if this <see cref="SceneReference"/> is safe to use. Check
        /// <see cref="IsSafeToUse"/> for that purpose.
        /// </remarks>
        /// <seealso cref="IsInBuildAndEnabled"/>
        /// <seealso cref="IsSafeToUse"/>
        public bool IsValidSceneAsset => sceneAsset != null;

        /// <summary>
        /// Is the scene added and enabled in Build Settings?
        /// </summary>
        /// <exception cref="InvalidSceneAssetException"></exception>
        /// <exception cref="OutdatedSceneGuidToPathMapException"></exception>
        /// <remarks>
        /// This property alone does not communicate if this <see cref="SceneReference"/> is safe to use. Check
        /// <see cref="IsSafeToUse"/> for that purpose.
        /// </remarks>
        /// <seealso cref="IsValidSceneAsset"/>
        /// <seealso cref="IsSafeToUse"/>
        public bool IsInBuildAndEnabled => BuildIndex != -1;

        /// <summary>
        /// Do we have a non-null and valid scene asset AND is the scene added and enabled in Build Settings?
        /// </summary>
        /// <remarks>
        /// If this property is true, then this <see cref="SceneReference"/> is safe to use.
        /// </remarks>
        /// <exception cref="OutdatedSceneGuidToPathMapException"></exception>
        /// <seealso cref="IsValidSceneAsset"/>
        /// <seealso cref="IsInBuildAndEnabled"/>
        // Does not throw InvalidSceneAssetException, because we are checking for IsValidSceneAsset first.
        public bool IsSafeToUse => IsValidSceneAsset && IsInBuildAndEnabled;
    }
}
