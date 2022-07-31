using System.Collections.Generic;
using Eflatun.SceneReference.Utility;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Scripting;

namespace Eflatun.SceneReference
{
    [Preserve]
    public static class SceneGuidToScenePathMap
    {
        private static Dictionary<string, string> _map;
        public static IReadOnlyDictionary<string, string> Map => _map;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void ParseMap()
        {
            var genFilePath = Paths.RelativeToResources.GenFile.UnixPath.WithoutExtension();
            var genFile = Resources.Load<TextAsset>(genFilePath);

            if (genFile == null)
            {
                Debug.LogError("Generated file not found!");
                return;
            }

            _map = JsonConvert.DeserializeObject<Dictionary<string, string>>(genFile.text);
        }
    }
}
