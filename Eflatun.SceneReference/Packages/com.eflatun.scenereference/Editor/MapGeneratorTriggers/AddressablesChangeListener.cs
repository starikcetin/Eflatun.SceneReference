#if ESR_ADDRESSABLES

using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;

namespace Eflatun.SceneReference.Editor.MapGeneratorTriggers
{
    [InitializeOnLoad]
    internal class AddressablesChangeListener
    {
        static AddressablesChangeListener()
        {
            AddressableAssetSettingsDefaultObject.Settings.OnModification += OnAddressablesChange;
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
                    Logger.Warn($"Skipping scene data maps generation after addressables changes. It is recommended to enable map generation after addressables changes, as an outdated map can result in broken scene references in runtime. You can enable it in {SettingsManager.SettingsMenuPathForDisplay}.");
                }
            }
        }
    }
}

#endif // ESR_ADDRESSABLES
