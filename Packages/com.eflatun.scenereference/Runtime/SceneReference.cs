using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Eflatun.SceneReference
{
    [Serializable]
    public class SceneReference
    {
        [SerializeField] internal UnityEngine.Object sceneAsset;
        [SerializeField] internal string sceneAssetGuidHex;

        public GUID Guid => new GUID(sceneAssetGuidHex);
        public string Path => Map.SceneGuidToScenePath[sceneAssetGuidHex];
        public int BuildIndex => SceneUtility.GetBuildIndexByScenePath(Path);
        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);
        public Scene LoadedScene => SceneManager.GetSceneByPath(Path);
    }
}
