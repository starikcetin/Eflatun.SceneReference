using UnityEditor;
using UnityEditor.PackageManager;

// TODO: run the generator only if there is a change in the packages we are interested in.

namespace Eflatun.SceneReference.Editor.MapGeneratorTriggers
{
    [InitializeOnLoad]
    internal static class PackagesChangeListener
    {
        static PackagesChangeListener()
        {
            UnityEditor.PackageManager.Events.registeredPackages += OnRegisteredPackages;
        }

        private static void OnRegisteredPackages(PackageRegistrationEventArgs obj)
        {
            if (SettingsManager.SceneDataMaps.IsGenerationTriggerEnabled(SceneDataMapsGeneratorTriggers.AfterPackagesResolve))
            {
                SceneDataMapsGenerator.Run(false);
            }
            else
            {
                EditorLogger.Warn($"Skipping scene data maps generation after packages resolve. It is recommended to enable map generation after packages resolve, as an outdated map can result in broken scene references in runtime. You can enable it in {SettingsManager.SettingsMenuPathForDisplay}.");
            }
        }
    }
}
