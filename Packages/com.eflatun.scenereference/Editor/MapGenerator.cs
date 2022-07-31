using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Eflatun.SceneReference.Editor
{
    public static class MapGenerator
    {
        [MenuItem("Eflatun/SceneReference/Generate Scene Map")]
        public static void Run()
        {
            Debug.Log("Regenerating scene map.");
            try
            {
                var allSceneGuids = AssetDatabase.FindAssets("t:Scene");
                var dictionary = allSceneGuids.ToDictionary(
                    x => x, // key generator: take guids
                    AssetDatabase.GUIDToAssetPath // value generator: take paths
                );
                
                var jsonRaw = JsonConvert.SerializeObject(dictionary, SettingsManager.MapJsonFormatting.value);
                
                Directory.CreateDirectory(Paths.RelativeToAssets.GenFolder.PlatformPath);
                File.WriteAllText(Paths.RelativeToAssets.GenFile.PlatformPath, jsonRaw);
            }
            finally
            {
                AssetDatabase.Refresh();
            }
        }
    }
}
