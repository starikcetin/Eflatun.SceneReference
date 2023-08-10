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
            EditorApplication.update += DelayedInitialization;
        }

        /// <summary>
        /// Wait for Editor to finish compiling before initializing. Otherwise AddressableAssetSettingsDefaultObject.Settings returns null.
        /// </summary>
        private static void DelayedInitialization()
        {
            if (EditorApplication.isCompiling || EditorApplication.isUpdating)
            {
                return;
            }

            EditorApplication.update -= DelayedInitialization; 
            //now its safe to initialize:
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
                    EditorLogger.Warn($"Skipping scene data maps generation after addressables changes. It is recommended to enable map generation after addressables changes, as an outdated map can result in broken scene references in runtime. You can enable it in {SettingsManager.SettingsMenuPathForDisplay}.");
                }
            }
        }
    }
}

#endif // ESR_ADDRESSABLES
