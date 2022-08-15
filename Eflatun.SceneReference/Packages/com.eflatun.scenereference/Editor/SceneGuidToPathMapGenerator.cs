using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEditor;

namespace Eflatun.SceneReference.Editor
{
    /// <summary>
    /// Generates and writes the scene GUID to path map.
    /// </summary>
    [PublicAPI]
    public static class SceneGuidToPathMapGenerator
    {
        /// <summary>
        /// Runs the generator.
        /// </summary>
        /// <remarks>
        /// The menu item "Tools/Eflatun/Scene Reference/Run Scene GUID to Path Map Generator" executes this method.
        /// </remarks>
        [MenuItem("Tools/" + Constants.MenuPrefixBase + "/Run Scene GUID to Path Map Generator", priority = -3130)]
        public static void Run()
        {
            const string dotKeepFileContent = "Add this file to version control. See for explanation: https://stackoverflow.com/a/17929518/6301627";
            
            Logger.Debug("Generating scene GUID to path map.");
            
            try
            {
                var allSceneGuids = AssetDatabase.FindAssets("t:Scene");
                var sceneGuidToPath = allSceneGuids.ToDictionary(
                    x => x, // key generator: take guids
                    AssetDatabase.GUIDToAssetPath // value generator: take paths
                );
                
                var jsonRaw = JsonConvert.SerializeObject(sceneGuidToPath, SettingsManager.SceneGuidToPathMap.JsonFormatting.value);

                Directory.CreateDirectory(Paths.Absolute.SceneGuidToPathMapFolder.PlatformPath);
                File.WriteAllText(Paths.Absolute.SceneGuidToPathMapDotKeepFile.PlatformPath, dotKeepFileContent);
                File.WriteAllText(Paths.Absolute.SceneGuidToPathMapFile.PlatformPath, jsonRaw);

                SceneGuidToPathMapProvider.DirectAssign(sceneGuidToPath);
            }
            finally
            {
                AssetDatabase.Refresh();
            }
        }
    }
}
