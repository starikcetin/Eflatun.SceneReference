using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEditor;

#if ESR_ADDRESSABLES
using UnityEditor.AddressableAssets;
#endif // ESR_ADDRESSABLES

namespace Eflatun.SceneReference.Editor
{
    /// <summary>
    /// Generates and writes the scene data maps.
    /// </summary>
    [PublicAPI]
    public static class SceneDataMapsGenerator
    {
        private const string DotKeepFileContent = "Add this file to version control. See for explanation: https://stackoverflow.com/a/17929518/6301627";

        /// <summary>
        /// Runs the generator.
        /// </summary>
        /// <remarks>
        /// The menu item "Tools/Eflatun/Scene Reference/Generate Scene Data Maps" executes this method.
        /// </remarks>
        [MenuItem("Tools/" + Constants.MenuPrefixBase + "/Generate Scene Data Maps", priority = -3130)]
        public static void Run()
        {
            try
            {
                Logger.Debug("Generating scene data maps.");

                WriteScaffolding();

                var allSceneGuids = AssetDatabase.FindAssets("t:Scene");

                var sceneGuidToPathMap = GenerateSceneGuidToPathMap(allSceneGuids);
                WriteSceneGuidToPathMap(sceneGuidToPathMap);

                var sceneGuidToAddressMap = GenerateSceneGuidToAddressMap(allSceneGuids);
                WriteSceneGuidToAddressMap(sceneGuidToAddressMap);
            }
            finally
            {
                AssetDatabase.Refresh();
            }
        }

        private static void WriteScaffolding()
        {
            Directory.CreateDirectory(Paths.Absolute.SceneDataMapsFolder.PlatformPath);
            File.WriteAllText(Paths.Absolute.SceneDataMapsDotKeepFile.PlatformPath, DotKeepFileContent);
        }

        private static Dictionary<string, string> GenerateSceneGuidToPathMap(string[] allSceneGuids)
        {
            var sceneGuidToPathMap = allSceneGuids.ToDictionary(
                x => x, // key generator: take guids
                AssetDatabase.GUIDToAssetPath // value generator: take paths
            );
            return sceneGuidToPathMap;
        }

        private static void WriteSceneGuidToPathMap(Dictionary<string, string> sceneGuidToPathMap)
        {
            var jsonRaw = JsonConvert.SerializeObject(sceneGuidToPathMap, SettingsManager.SceneDataMaps.JsonFormatting.value);
            File.WriteAllText(Paths.Absolute.SceneGuidToPathMapFile.PlatformPath, jsonRaw);

            SceneGuidToPathMapProvider.DirectAssign(sceneGuidToPathMap);
        }

        private static Dictionary<string, string> GenerateSceneGuidToAddressMap(string[] allSceneGuids)
        {
#if ESR_ADDRESSABLES
            var addressableSettings = AddressableAssetSettingsDefaultObject.Settings;

            var addressableSceneAssetEntries = allSceneGuids
                .Select(addressableSettings.FindAssetEntry)
                .Where(x => x != null);

            var sceneGuidToAddressMap = addressableSceneAssetEntries.ToDictionary(
                x => x.guid, // key generator: take guids
                x => x.address // value generator: take addresses
            );

            return sceneGuidToAddressMap;
#else // ESR_ADDRESSABLES
            return new Dictionary<string, string>();
#endif // ESR_ADDRESSABLES
        }

        private static void WriteSceneGuidToAddressMap(Dictionary<string, string> sceneGuidToAddressMap)
        {
            var jsonRaw = JsonConvert.SerializeObject(sceneGuidToAddressMap, SettingsManager.SceneDataMaps.JsonFormatting.value);
            File.WriteAllText(Paths.Absolute.SceneGuidToAddressMapFile.PlatformPath, jsonRaw);

            SceneGuidToAddressMapProvider.DirectAssign(sceneGuidToAddressMap);
        }
    }
}
