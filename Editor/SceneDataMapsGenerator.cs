using Eflatun.SceneReference.Editor.Utility;
using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        [MenuItem("Tools/" + Constants.MenuPrefixBase + "/Generate Scene Data Maps (Don't Output Files)", priority = -3130)]
        private static void RunNoOutputMenuItem()
        {
            Run(false);
        }

        [MenuItem("Tools/" + Constants.MenuPrefixBase + "/Generate Scene Data Maps (Output Files)", priority = -3130)]
        private static void RunAndOutputMenuItem()
        {
            Run(true);
        }

        /// <summary>
        /// Runs the generator.
        /// </summary>
        /// <remarks>
        /// The menu item "Tools/Eflatun/Scene Reference/Generate Scene Data Maps" executes this method.
        /// </remarks>
        public static void Run(bool outputFiles)
        {
            try
            {
                EditorLogger.Debug("Generating scene data maps.");

                if (outputFiles)
                {
                    Directory.CreateDirectory(Paths.ResourcesFolder.PlatformPath);
                }

                var allSceneGuids = AssetDatabase.FindAssets("t:Scene");

                var sceneGuidToPathMap = GenerateSceneGuidToPathMap(allSceneGuids);
                WriteSceneGuidToPathMap(sceneGuidToPathMap, outputFiles);

                var sceneGuidToAddressMap = GenerateSceneGuidToAddressMap(allSceneGuids);
                WriteSceneGuidToAddressMap(sceneGuidToAddressMap, outputFiles);
            }
            finally
            {
                AssetDatabase.Refresh();
            }
        }

        internal static void CleanFileOutput()
        {
            EditorUtils.DeleteFileIfExists(Paths.Absolute.SceneGuidToPathMapFile.PlatformPath);
            EditorUtils.DeleteFileIfExists(Paths.Absolute.SceneGuidToPathMapMetaFile.PlatformPath);

            EditorUtils.DeleteFileIfExists(Paths.Absolute.SceneGuidToAddressMapFile.PlatformPath);
            EditorUtils.DeleteFileIfExists(Paths.Absolute.SceneGuidToAddressMapMetaFile.PlatformPath);

            if (Directory.Exists(Paths.ResourcesFolder.PlatformPath) && EditorUtils.IsDirectoryEmpty(Paths.ResourcesFolder.PlatformPath))
            {
                Directory.Delete(Paths.ResourcesFolder.PlatformPath);
                EditorUtils.DeleteFileIfExists(Paths.ResourcesMetaFile.PlatformPath);
            }
        }

        private static Dictionary<string, string> GenerateSceneGuidToPathMap(string[] allSceneGuids)
        {
            var sceneGuidToPathMap = allSceneGuids.ToDictionary(
                x => x, // key generator: take guids
                AssetDatabase.GUIDToAssetPath // value generator: take paths
            );
            return sceneGuidToPathMap;
        }

        private static void WriteSceneGuidToPathMap(Dictionary<string, string> sceneGuidToPathMap, bool outputFiles)
        {
            var jsonRaw = JsonConvert.SerializeObject(sceneGuidToPathMap, SettingsManager.SceneDataMaps.JsonFormatting.value);

            if (outputFiles)
            {
                File.WriteAllText(Paths.Absolute.SceneGuidToPathMapFile.PlatformPath, jsonRaw);
            }

            EditorMapStore.SceneGuidToPathMapJson = jsonRaw;
            SceneGuidToPathMapProvider.FillWith(sceneGuidToPathMap);
        }

        private static Dictionary<string, string> GenerateSceneGuidToAddressMap(string[] allSceneGuids)
        {
#if ESR_ADDRESSABLES
            if (!AddressableAssetSettingsDefaultObject.SettingsExists)
            {
                EditorLogger.Warn("Addressables settings not found. Skipping map generation.");
                return new Dictionary<string, string>();
            }

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

        private static void WriteSceneGuidToAddressMap(Dictionary<string, string> sceneGuidToAddressMap, bool outputFiles)
        {
            var jsonRaw = JsonConvert.SerializeObject(sceneGuidToAddressMap, SettingsManager.SceneDataMaps.JsonFormatting.value);

            if (outputFiles)
            {
                File.WriteAllText(Paths.Absolute.SceneGuidToAddressMapFile.PlatformPath, jsonRaw);
            }

            EditorMapStore.SceneGuidToAddressMapJson = jsonRaw;
            SceneGuidToAddressMapProvider.FillWith(sceneGuidToAddressMap);
        }
    }
}
