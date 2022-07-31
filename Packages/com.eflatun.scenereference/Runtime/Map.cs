using System.Collections.Generic;
using System.IO;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Scripting;

namespace Eflatun.SceneReference
{
    [Preserve]
    public static class Map
    {
        public static Dictionary<string, string> SceneGuidToScenePath { get; private set; }

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

            SceneGuidToScenePath = JsonConvert.DeserializeObject<Dictionary<string, string>>(genFile.text);
        }
    }
}
