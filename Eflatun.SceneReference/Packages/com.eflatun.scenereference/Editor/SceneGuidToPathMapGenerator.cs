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
    /// Generates and writes the scene GUID to path map.
    /// </summary>
    [PublicAPI]
    public static class SceneGuidToPathMapGenerator
    {
        private const string DotKeepFileContent = "Add this file to version control. See for explanation: https://stackoverflow.com/a/17929518/6301627";

        /// <summary>
        /// Runs the generator.
        /// </summary>
        /// <remarks>
        /// The menu item "Tools/Eflatun/Scene Reference/Run Scene GUID to Path Map Generator" executes this method.
        /// </remarks>
        [MenuItem("Tools/" + Constants.MenuPrefixBase + "/Run Scene GUID to Path Map Generator", priority = -3130)]
        public static void Run()
        {
            try
            {
                Logger.Debug("Generating maps.");

                WriteScaffolding();

                var allSceneGuids = AssetDatabase.FindAssets("t:Scene");

                var sceneGuidToPathMap = GenerateSceneGuidToPathMap(allSceneGuids);
                WriteSceneGuidToPathMap(sceneGuidToPathMap);

                var addressableSceneGuidToAddressMap = GenerateAddressableSceneGuidToAddressMap(allSceneGuids);
                WriteAddressableSceneGuidToAddressMap(addressableSceneGuidToAddressMap);
            }
            finally
            {
                AssetDatabase.Refresh();
            }
        }

        private static void WriteScaffolding()
        {
            Directory.CreateDirectory(Paths.Absolute.SceneGuidToPathMapFolder.PlatformPath);
            File.WriteAllText(Paths.Absolute.SceneGuidToPathMapDotKeepFile.PlatformPath, DotKeepFileContent);
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
            var jsonRaw = JsonConvert.SerializeObject(sceneGuidToPathMap, SettingsManager.SceneGuidToPathMap.JsonFormatting.value);
            File.WriteAllText(Paths.Absolute.SceneGuidToPathMapFile.PlatformPath, jsonRaw);

            SceneGuidToPathMapProvider.DirectAssign(sceneGuidToPathMap);
        }

        private static Dictionary<string, string> GenerateAddressableSceneGuidToAddressMap(string[] allSceneGuids)
        {
#if ESR_ADDRESSABLES
            var addressableSettings = AddressableAssetSettingsDefaultObject.Settings;

            var addressableSceneAssetEntries = allSceneGuids
                .Select(addressableSettings.FindAssetEntry)
                .Where(x => x != null);

            var addressableSceneGuidToAddressMap = addressableSceneAssetEntries.ToDictionary(
                x => x.guid, // key generator: take guids
                x => x.address // value generator: take addresses
            );

            return addressableSceneGuidToAddressMap;
#else // ESR_ADDRESSABLES
            return new Dictionary<string, string>();
#endif // ESR_ADDRESSABLES
        }

        private static void WriteAddressableSceneGuidToAddressMap(Dictionary<string, string> addressableSceneGuidToAddressMap)
        {
            var jsonRaw = JsonConvert.SerializeObject(addressableSceneGuidToAddressMap, SettingsManager.SceneGuidToPathMap.JsonFormatting.value);
            File.WriteAllText(Paths.Absolute.AddressableSceneGuidToAddressMapFile.PlatformPath, jsonRaw);

            AddressableSceneGuidToAddressMapProvider.DirectAssign(addressableSceneGuidToAddressMap);
        }
    }
}
