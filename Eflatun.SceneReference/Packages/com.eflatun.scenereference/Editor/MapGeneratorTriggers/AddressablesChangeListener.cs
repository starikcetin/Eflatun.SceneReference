#if ESR_ADDRESSABLES

using Eflatun.SceneReference.Editor.Utility;
using System.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace Eflatun.SceneReference.Editor.MapGeneratorTriggers
{
    [InitializeOnLoad]
    internal class AddressablesChangeListener : AssetPostprocessor
    {
        private static bool _isSubscribed;

        static AddressablesChangeListener()
        {
            if (AddressableAssetSettingsDefaultObject.SettingsExists)
            {
                if (!_isSubscribed)
                {
                    AddressableAssetSettingsDefaultObject.Settings.OnModification += OnAddressablesChange;
                    _isSubscribed = true;
                }
            }
            else
            {
                EditorLogger.Warn("Addressables settings not found. Skipping subscribing to addressables changes.");
            }
        }

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (AddressableAssetSettingsDefaultObject.SettingsExists)
            {
                if (!_isSubscribed)
                {
                    EditorLogger.Debug("Found addressables settings. Subscribing to addressables changes.");
                    AddressableAssetSettingsDefaultObject.Settings.OnModification += OnAddressablesChange;
                    _isSubscribed = true;
                }
            }
            else
            {
                if (_isSubscribed)
                {
                    EditorLogger.Warn("Lost addressables settings. Unsubscribing from addressables changes.");
                    _isSubscribed = false;
                }
            }
        }

        private static void OnAddressablesChange(AddressableAssetSettings s, AddressableAssetSettings.ModificationEvent e, object o)
        {
            if (e == AddressableAssetSettings.ModificationEvent.EntryAdded ||
                e == AddressableAssetSettings.ModificationEvent.EntryRemoved ||
                e == AddressableAssetSettings.ModificationEvent.EntryModified ||
                e == AddressableAssetSettings.ModificationEvent.EntryMoved ||
                e == AddressableAssetSettings.ModificationEvent.EntryCreated)
            {
                if (SettingsManager.SceneDataMaps.IsGenerationTriggerEnabled(SceneDataMapsGeneratorTriggers.AfterAddressablesChange))
                {
                    SceneDataMapsGenerator.Run();
                }
                else
                {
                    EditorLogger.Warn($"Skipping scene data maps generation after addressables changes. It is recommended to enable map generation after addressables changes, as an outdated map can result in broken scene references in runtime. You can enable it in {SettingsManager.SettingsMenuPathForDisplay}.");
                }
            }
        }
    }
}

#endif // ESR_ADDRESSABLES
