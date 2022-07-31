using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Eflatun.SceneReference.Editor
{
    /// <summary>
    /// Generates and writes the scene map.
    /// </summary>
    public static class MapGenerator
    {
        /// <summary>
        /// Runs the generator.
        /// </summary>
        /// <remarks>
        /// The menu item "Eflatun/Scene Reference/Run Scene Map Generator" executes this method.
        /// </remarks>
        [MenuItem("Eflatun/Scene Reference/Run Scene Map Generator")]
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
                
                Directory.CreateDirectory(Paths.RelativeToAssets.MapFolder.PlatformPath);
                File.WriteAllText(Paths.RelativeToAssets.MapFile.PlatformPath, jsonRaw);
            }
            finally
            {
                AssetDatabase.Refresh();
            }
        }
    }
}
