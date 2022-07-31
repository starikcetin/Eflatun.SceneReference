using System.Collections.Generic;
using Eflatun.SceneReference.Utility;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Scripting;

namespace Eflatun.SceneReference
{
    /// <summary>
    /// Provides the scene GUID to path map. Can be used in both editor and runtime.
    /// </summary>
    [PublicAPI]
    public static class SceneGuidToPathMapProvider
    {
        private static Dictionary<string, string> _sceneGuidToPathMap;

        /// <summary>
        /// The scene GUID to path map.
        /// </summary>
        public static IReadOnlyDictionary<string, string> SceneGuidToPathMap
        {
            get
            {
                LoadIfNotAlready();
                return _sceneGuidToPathMap;
            }
        }

        internal static void DirectAssign(Dictionary<string, string> sceneGuidToPath)
        {
            _sceneGuidToPathMap = sceneGuidToPath;
        }

        [Preserve]
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void LoadIfNotAlready()
        {
            if (_sceneGuidToPathMap == null)
            {
                Load();
            }
        }

        private static void Load()
        {
            var genFilePath = Paths.RelativeToResources.SceneGuidToPathMapFile.UnixPath.WithoutExtension();
            var genFile = Resources.Load<TextAsset>(genFilePath);

            if (genFile == null)
            {
                Logger.Error("Scene GUID to path map file not found!");
                return;
            }

            _sceneGuidToPathMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(genFile.text);
        }
    }
}
