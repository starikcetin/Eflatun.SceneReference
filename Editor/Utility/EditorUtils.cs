using System;
using System.IO;
using System.Linq;
using Eflatun.SceneReference.Exceptions;
using UnityEditor;

#if ESR_ADDRESSABLES
using UnityEditor.AddressableAssets;
#endif // ESR_ADDRESSABLES

namespace Eflatun.SceneReference.Editor.Utility
{
    /// <summary>
    /// Editor utilities used by Eflatun.SceneReference.
    /// </summary>
    internal static class EditorUtils
    {
        /// <summary>
        /// Returns whether the addressables package is installed in the project.
        /// </summary>
        public static bool IsAddressablesPackagePresent =>
#if ESR_ADDRESSABLES
            true
#else // ESR_ADDRESSABLES
            false
#endif // ESR_ADDRESSABLES
        ;

        private static Type _addressablesGroupsWindowType;
        /// <summary>The <see cref="Type"/> of the Addressables Groups editor window.</summary>
        public static Type AddressablesGroupsWindowType
        {
            get
            {
                if (_addressablesGroupsWindowType == null)
                {
                    var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                    foreach (var assembly in assemblies)
                    {
                        var type = assembly.GetType("UnityEditor.AddressableAssets.GUI.AddressableAssetsWindow");
                        if (type != null)
                        {
                            _addressablesGroupsWindowType = type;
                        }
                    }
                }

                if (_addressablesGroupsWindowType == null)
                {
                    throw SceneReferenceInternalException.EditorCode("48302749", "Could not find addressables groups window type.");
                }

                return _addressablesGroupsWindowType;
            }
        }

        /// <summary>
        /// Returns true if the given <paramref name="path"/> ends with the file extension ".unity".
        /// </summary>
        public static bool IsScenePath(this string path) => Path.GetExtension(path) == ".unity";

        /// <summary>
        /// Adds the scene with the given path to build settings as enabled.
        /// </summary>
        public static void AddSceneToBuild(string scenePath)
        {
            var tempScenes = EditorBuildSettings.scenes.ToList();
            tempScenes.Add(new EditorBuildSettingsScene(scenePath, true));
            EditorBuildSettings.scenes = tempScenes.ToArray();
        }

        /// <summary>
        /// Enables the scene with the given guid in build settings.
        /// </summary>
        public static void EnableSceneInBuild(string sceneGuid)
        {
            var tempScenes = EditorBuildSettings.scenes.ToList();
            tempScenes.Single(x => x.guid.ToString() == sceneGuid).enabled = true;
            EditorBuildSettings.scenes = tempScenes.ToArray();
        }

#if ESR_ADDRESSABLES
        /// <summary>
        /// Adds the scene with the given GUID to the default addressable group.
        /// </summary>
        public static void AddToDefaultAddressableGroup(string sceneGuid)
        {
            if (!AddressableAssetSettingsDefaultObject.SettingsExists)
            {
                EditorLogger.Warn("Addressables settings not found. Skipping adding to group.");
                return;
            }

            var defaultGroup = AddressableAssetSettingsDefaultObject.Settings.DefaultGroup;
            AddressableAssetSettingsDefaultObject.Settings.CreateOrMoveEntry(sceneGuid, defaultGroup);
        }
#endif // ESR_ADDRESSABLES

        public static bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        public static void DeleteFileIfExists(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (DirectoryNotFoundException)
            {
                // ignored
            }
        }
    }
}
