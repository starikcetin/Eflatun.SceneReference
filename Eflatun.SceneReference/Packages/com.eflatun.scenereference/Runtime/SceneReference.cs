using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR

namespace Eflatun.SceneReference
{
    /// <summary>
    /// Provides a strongly-typed reference to a scene. Can be used in both editor and runtime.
    /// </summary>
    [PublicAPI]
    [Serializable]
    public class SceneReference : ISerializationCallbackReceiver
    {
        [SerializeField] internal UnityEngine.Object sceneAsset;
        [SerializeField] internal string sceneAssetGuidHex;

        /// <summary>
        /// Do we have a non-null and valid scene asset?
        /// </summary>
        /// <remarks>
        /// This property alone does not communicate if this <see cref="SceneReference"/> is safe to use. Check
        /// <see cref="IsSafeToUse"/> for that purpose.
        /// </remarks>
        /// <seealso cref="IsInBuildAndEnabled"/>
        /// <seealso cref="IsSafeToUse"/>
        [field: SerializeField]
        public bool IsValidSceneAsset { get; private set; }

        /// <summary>
        /// GUID of the scene asset in hex format. 
        /// </summary>
        /// <remarks>
        /// You can check <see cref="IsValidSceneAsset"/> to see if the scene asset is non-null and valid.
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

        /// <summary>
        /// Do NOT call this method. It is for <see cref="ISerializationCallbackReceiver"/>.
        /// </summary>
        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (sceneAsset == null)
            {
                IsValidSceneAsset = false;
            }
            else if (!(sceneAsset is SceneAsset))
            {
                Debug.LogError($"A {nameof(SceneReference)} is assigned a non-null asset, but it is not of type {nameof(SceneAsset)}!");
                IsValidSceneAsset = false;
            }
            else if (!AssetDatabase.Contains(sceneAsset))
            {
                Debug.LogError($"A {nameof(SceneReference)} is assigned a non-null {nameof(SceneAsset)}, but {nameof(AssetDatabase)} doesn't contain the asset!");
                IsValidSceneAsset = false;
            }
            else
            {
                IsValidSceneAsset = true;
            }
#endif // UNITY_EDITOR
        }

        /// <summary>
        /// Do NOT call this method. It is for <see cref="ISerializationCallbackReceiver"/>.
        /// </summary>
        public void OnAfterDeserialize()
        {
        }
    }
}
