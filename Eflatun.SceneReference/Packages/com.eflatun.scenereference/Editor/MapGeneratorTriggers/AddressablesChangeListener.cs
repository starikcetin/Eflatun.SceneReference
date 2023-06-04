#if EFLATUN_SCENEREFERENCE_ADDRESSABLES_PACKAGE_PRESENT

using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;

// TODO: we should also generate addressable map immediately after we enable the addressable support, since addressable change trigger will not run then

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
                if (SettingsManager.AddressablesSupport.IsAddressablesChangeGenerationTriggerEnabled)
                {
                    SceneGuidToPathMapGenerator.Run();
                }
                else
                {
                    // TODO: write better warning
                    Logger.Warn("Addressables change generation trigger is disabled.");
                }
            }
        }
    }
}

#endif // EFLATUN_SCENEREFERENCE_ADDRESSABLES_PACKAGE_PRESENT
