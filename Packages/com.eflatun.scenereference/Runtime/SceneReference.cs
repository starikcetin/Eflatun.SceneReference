using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Eflatun.SceneReference
{
    /// <summary>
    /// Provides a strongly-typed reference to a scene to be used in runtime.
    /// </summary>
    [Serializable, PublicAPI]
    public class SceneReference
    {
        [SerializeField] internal UnityEngine.Object sceneAsset;
        [SerializeField] internal string sceneAssetGuidHex;

        /// <summary>
        /// Scene asset's GUID in hex format.
        /// </summary>
        public string AssetGuidHex => sceneAssetGuidHex;
        
        /// <summary>
        /// Path to the scene asset.
        /// </summary>
        public string Path => Map.SceneGuidToScenePath[sceneAssetGuidHex];
        
        /// <summary>
        /// Build index of the scene.
        /// </summary>
        public int BuildIndex => SceneUtility.GetBuildIndexByScenePath(Path);
        
        /// <summary>
        /// Name of the scene asset.
        /// </summary>
        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);
        
        /// <summary>
        /// The <see cref="Scene"/> struct for this scene. Only valid if the scene is currently loaded.
        /// </summary>
        public Scene LoadedScene => SceneManager.GetSceneByPath(Path);
    }
}
