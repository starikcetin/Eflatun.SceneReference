using UnityEditor;
using UnityEditor.PackageManager;

namespace Eflatun.SceneReference.Editor.MapGeneratorTriggers
{
    [InitializeOnLoad]
    internal static class PackagesChangeListener
    {
        static PackagesChangeListener()
        {
            UnityEditor.PackageManager.Events.registeredPackages += RegisteredPackagesEventHandler;
        }

        private static void RegisteredPackagesEventHandler(PackageRegistrationEventArgs obj)
        {
            if (SettingsManager.SceneGuidToPathMap.IsGenerationTriggerEnabled(SceneGuidToPathMapGenerationTriggers.AfterPackageResolve))
            {
                SceneGuidToPathMapGenerator.Run();
            }
            else
            {
                Logger.Warn($"Skipping scene GUID to path map generation after package changes. It is recommended to enable map generation after package changes, as an outdated map can result in broken scene references in runtime. You can enable it in {SettingsManager.SettingsMenuPathForDisplay}.");
            }
        }
    }
}
