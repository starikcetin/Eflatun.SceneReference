using System.Collections.Generic;
using Eflatun.SceneReference.Utility;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Scripting;

namespace Eflatun.SceneReference
{
    /// <summary>
    /// The parsed map. Can be used in both editor and runtime.
    /// </summary>
    [PublicAPI]
    public static class Map
    {
        private static Dictionary<string, string> _sceneGuidToScenePath;
        
        /// <summary>
        /// A map of scene GUID hex values to scene paths.
        /// </summary>
        public static IReadOnlyDictionary<string, string> SceneGuidToScenePath
        {
            get
            {
#if UNITY_EDITOR
                // In editor, it needs to be loaded from scratch every time to make sure we get the latest values.
                LoadMap();
#else
                // In runtime the file can't change, so only load it if we haven't loaded it already.
                LoadMapIfNotAlready();
#endif

                return _sceneGuidToScenePath;
            }
        }

        [Preserve]
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void LoadMapIfNotAlready()
        {
            if (_sceneGuidToScenePath == null)
            {
                LoadMap();
            }
        }
        
        private static void LoadMap()
        {
            var genFilePath = Paths.RelativeToResources.MapFile.UnixPath.WithoutExtension();
            var genFile = Resources.Load<TextAsset>(genFilePath);

            if (genFile == null)
            {
                Debug.LogError("Generated file not found!");
                return;
            }

            _sceneGuidToScenePath = JsonConvert.DeserializeObject<Dictionary<string, string>>(genFile.text);
        }
    }
}
