#if EFLATUN_SCENEREFERENCE_ADDRESSABLES_PACKAGE_PRESENT

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
            AddressableAssetSettingsDefaultObject.Settings.OnModification += OnSettingsModificationCustom;
        }

        private static void OnSettingsModificationCustom(AddressableAssetSettings s, AddressableAssetSettings.ModificationEvent e, object o)
        {
            if (e == AddressableAssetSettings.ModificationEvent.EntryAdded ||
                e == AddressableAssetSettings.ModificationEvent.EntryRemoved ||
                e == AddressableAssetSettings.ModificationEvent.EntryModified ||
                e == AddressableAssetSettings.ModificationEvent.EntryMoved ||
                e == AddressableAssetSettings.ModificationEvent.EntryCreated)
            {
                if (SettingsManager.SceneGuidToPathMap.IsGenerationTriggerEnabled(SceneGuidToPathMapGenerationTriggers.AfterAddressablesChange))
                {
                    SceneGuidToPathMapGenerator.Run();
                }
                else
                {
                    Logger.Warn($"Skipping scene GUID to path map generation after addressables changes. It is recommended to enable map generation after addressables changes, as an outdated map can result in broken scene references in runtime. You can enable it in {SettingsManager.SettingsMenuPathForDisplay}.");
                }
            }
        }
    }
}

#endif // EFLATUN_SCENEREFERENCE_ADDRESSABLES_PACKAGE_PRESENT
