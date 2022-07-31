using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Eflatun.SceneReference
{
    [Serializable, PublicAPI]
    public class SceneReference
    {
        [SerializeField] internal UnityEngine.Object sceneAsset;
        [SerializeField] internal string sceneAssetGuidHex;

        public string AssetGuidHex => sceneAssetGuidHex;
        public string Path => SceneGuidToScenePathMap.Map[sceneAssetGuidHex];
        public int BuildIndex => SceneUtility.GetBuildIndexByScenePath(Path);
        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);
        public Scene LoadedScene => SceneManager.GetSceneByPath(Path);
    }
}
