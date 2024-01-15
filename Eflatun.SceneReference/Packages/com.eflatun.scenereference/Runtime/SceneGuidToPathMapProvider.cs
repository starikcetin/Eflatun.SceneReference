using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Scripting;

#if !UNITY_EDITOR
using Eflatun.SceneReference.Utility;
#endif

namespace Eflatun.SceneReference
{
    /// <summary>
    /// Provides the scene GUID to path map. Can be used in both editor and runtime.
    /// </summary>
    [PublicAPI]
    public static class SceneGuidToPathMapProvider
    {
        private static Dictionary<string, string> _sceneGuidToPathMap;
        private static Dictionary<string, string> _scenePathToGuidMap;

        private static string MapJson
        {
            get
            {
#if UNITY_EDITOR
                return EditorMapStore.SceneGuidToPathMapJson;
#else
                var genFilePath = Paths.RelativeToResources.SceneGuidToPathMapFile.WithoutExtension();
                var genFile = Resources.Load<TextAsset>(genFilePath);
                return genFile.text;
#endif
            }
        }

        /// <summary>
        /// The scene GUID to path map.
        /// </summary>
        public static IReadOnlyDictionary<string, string> SceneGuidToPathMap => GetSceneGuidToPathMap(true);

        /// <summary>
        /// The scene path to GUID map.
        /// </summary>
        public static IReadOnlyDictionary<string, string> ScenePathToGuidMap => GetScenePathToGuidMap(true);

        [Preserve]
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void RuntimeInit()
        {
            LoadIfNotAlready(true);
        }

        internal static IReadOnlyDictionary<string, string> GetSceneGuidToPathMap(bool errorIfMissingDuringLoad)
        {
            LoadIfNotAlready(errorIfMissingDuringLoad);
            return _sceneGuidToPathMap;
        }

        internal static IReadOnlyDictionary<string, string> GetScenePathToGuidMap(bool errorIfMissingDuringLoad)
        {
            LoadIfNotAlready(errorIfMissingDuringLoad);
            return _scenePathToGuidMap;
        }

        internal static void DirectAssign(Dictionary<string, string> sceneGuidToPathMap)
        {
            FillWith(sceneGuidToPathMap);
        }

        private static void LoadIfNotAlready(bool errorIfMissing)
        {
            if (_sceneGuidToPathMap == null)
            {
                Load(errorIfMissing);
            }
        }

        private static void Load(bool errorIfMissing)
        {
            var json = MapJson;

            if (string.IsNullOrWhiteSpace(json))
            {
                if (errorIfMissing)
                {
                    Logger.Error("Scene GUID to path map file not found!");
                }

                return;
            }

            var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            FillWith(deserialized);
        }

        private static void FillWith(Dictionary<string, string> sceneGuidToPathMap)
        {
            _sceneGuidToPathMap = sceneGuidToPathMap;
            _scenePathToGuidMap = sceneGuidToPathMap.ToDictionary(x => x.Value, x => x.Key);
        }
    }
}
