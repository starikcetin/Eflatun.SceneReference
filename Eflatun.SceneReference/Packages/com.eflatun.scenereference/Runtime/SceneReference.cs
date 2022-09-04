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
        // An invalid GUID hex contains all zeros. A GUID hex has 32 chars.
        private const string AllZeroGuidHex = "00000000000000000000000000000000";

        [SerializeField] internal UnityEngine.Object sceneAsset = null;
        [SerializeField] internal string sceneAssetGuidHex = AllZeroGuidHex;

        /// <summary>
        /// GUID of the scene asset in hex format. 
        /// </summary>
        public string AssetGuidHex => sceneAssetGuidHex;

        /// <summary>
        /// Path to the scene asset.
        /// </summary>
        /// <exception cref="EmptySceneReferenceException">Throws if <see cref="HasValue"/> is <c>false</c>.</exception>
        /// <exception cref="InvalidSceneReferenceException">Throws if <see cref="IsInSceneGuidToPathMap"/> is <c>false</c>.</exception>
        public string Path
        {
            get
            {
                if (!HasValue)
                {
                    throw new EmptySceneReferenceException();
                }

                if (!SceneGuidToPathMapProvider.SceneGuidToPathMap.TryGetValue(sceneAssetGuidHex, out var scenePath))
                {
                    throw new InvalidSceneReferenceException();
                }

                return scenePath;
            }
        }

        /// <summary>
        /// Build index of the scene.
        /// </summary>
        /// <exception cref="EmptySceneReferenceException">Throws if <see cref="HasValue"/> is <c>false</c>.</exception>
        /// <exception cref="InvalidSceneReferenceException">Throws if <see cref="IsInSceneGuidToPathMap"/> is <c>false</c>.</exception>
        /// <remarks>
        /// This property will return <c>-1</c> if <see cref="IsInBuildAndEnabled"/> is <c>false</c>.
        /// </remarks>
        public int BuildIndex => SceneUtility.GetBuildIndexByScenePath(Path);

        /// <summary>
        /// Name of the scene asset.
        /// </summary>
        /// <exception cref="EmptySceneReferenceException">Throws if <see cref="HasValue"/> is <c>false</c>.</exception>
        /// <exception cref="InvalidSceneReferenceException">Throws if <see cref="IsInSceneGuidToPathMap"/> is <c>false</c>.</exception>
        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);

        /// <summary>
        /// The <see cref="Scene"/> struct for this scene. Only valid if the scene is currently loaded.
        /// </summary>
        /// <exception cref="EmptySceneReferenceException">Throws if <see cref="HasValue"/> is <c>false</c>.</exception>
        /// <exception cref="InvalidSceneReferenceException">Throws if <see cref="IsInSceneGuidToPathMap"/> is <c>false</c>.</exception>
        public Scene LoadedScene => SceneManager.GetSceneByPath(Path);

        /// <summary>
        /// Is this <see cref="SceneReference"/> assigned something?
        /// </summary>
        /// <remarks>
        /// This property alone does not communicate if this <see cref="SceneReference"/> is safe to use. Check <see cref="IsSafeToUse"/> for that purpose.
        /// </remarks>
        /// <seealso cref="IsInSceneGuidToPathMap"/>
        /// <seealso cref="IsInBuildAndEnabled"/>
        /// <seealso cref="IsSafeToUse"/>
        public bool HasValue
        {
            get
            {
                if (string.IsNullOrWhiteSpace(AssetGuidHex))
                {
                    // internal exceptions should not be documented as part of the public API
                    throw SceneReferenceInternalException.AssetGuidHexNullOrWhiteSpace("54783205", AssetGuidHex);
                }

                return AssetGuidHex != AllZeroGuidHex;
            }
        }

        /// <summary>
        /// Does the scene GUID to path map contain the scene?
        /// </summary>
        /// <exception cref="EmptySceneReferenceException">Throws if <see cref="HasValue"/> is <c>false</c>.</exception>
        /// <remarks>
        /// This property alone does not communicate if this <see cref="SceneReference"/> is safe to use. Check <see cref="IsSafeToUse"/> for that purpose.
        /// </remarks>
        /// <seealso cref="HasValue"/>
        /// <seealso cref="IsInBuildAndEnabled"/>
        /// <seealso cref="IsSafeToUse"/>
        public bool IsInSceneGuidToPathMap
        {
            get
            {
                if (!HasValue)
                {
                    throw new EmptySceneReferenceException();
                }
                
                return SceneGuidToPathMapProvider.SceneGuidToPathMap.ContainsKey(AssetGuidHex);
            }
        }

        /// <summary>
        /// Is the scene added and enabled in Build Settings?
        /// </summary>
        /// <exception cref="EmptySceneReferenceException">Throws if <see cref="HasValue"/> is <c>false</c>.</exception>
        /// <exception cref="InvalidSceneReferenceException">Throws if <see cref="IsInSceneGuidToPathMap"/> is <c>false</c>.</exception>
        /// <remarks>
        /// This property alone does not communicate if this <see cref="SceneReference"/> is safe to use. Check <see cref="IsSafeToUse"/> for that purpose.
        /// </remarks>
        /// <seealso cref="HasValue"/>
        /// <seealso cref="IsInSceneGuidToPathMap"/>
        /// <seealso cref="IsSafeToUse"/>
        public bool IsInBuildAndEnabled => BuildIndex != -1;

        /// <summary>
        /// Is this <see cref="SceneReference"/> safe to use?
        /// </summary>
        /// <remarks>
        /// This property is equivalent to checking all three of <see cref="HasValue"/>, <see cref="IsInSceneGuidToPathMap"/>, and <see cref="IsInBuildAndEnabled"/>, with slightly better performance.
        /// </remarks>
        /// <seealso cref="HasValue"/>
        /// <seealso cref="IsInSceneGuidToPathMap"/>
        /// <seealso cref="IsInBuildAndEnabled"/>
        public bool IsSafeToUse =>
            HasValue
            && SceneGuidToPathMapProvider.SceneGuidToPathMap.TryGetValue(AssetGuidHex, out var path)
            && SceneUtility.GetBuildIndexByScenePath(path) != -1;
    }
}
