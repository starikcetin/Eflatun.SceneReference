using UnityEditor;
using UnityEditor.PackageManager;

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
                SceneDataMapsGenerator.Run();
            }
            else
            {
                Logger.Warn($"Skipping scene data maps generation after package changes. It is recommended to enable map generation after package changes, as an outdated map can result in broken scene references in runtime. You can enable it in {SettingsManager.SettingsMenuPathForDisplay}.");
            }
        }
    }
}
