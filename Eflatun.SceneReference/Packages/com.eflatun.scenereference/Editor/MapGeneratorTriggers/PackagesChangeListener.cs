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
                // TODO: write better warning
                Logger.Warn("AfterPackageResolve trigger disabled!");
            }
        }
    }
}
