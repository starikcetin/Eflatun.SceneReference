using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Eflatun.SceneReference
{
    [Serializable, PublicAPI]
    public class SceneReference
    {
        [SerializeField] internal UnityEngine.Object sceneAsset;
        [SerializeField] internal string sceneAssetGuidHex;

        public GUID Guid => new(sceneAssetGuidHex);
        public string Path => Map.SceneGuidToScenePath[sceneAssetGuidHex];
        public int BuildIndex => SceneUtility.GetBuildIndexByScenePath(Path);
        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);
        public Scene LoadedScene => SceneManager.GetSceneByPath(Path);
    }
}
