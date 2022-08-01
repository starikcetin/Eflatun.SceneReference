using System;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Eflatun.SceneReference.Editor.MapGeneratorTriggers
{
    internal class BuildPreProcessor : IPreprocessBuildWithReport
    {
        public int callbackOrder => -100;

        public void OnPreprocessBuild(BuildReport report)
        {
            if (!SettingsManager.SceneGuidToPathMap.IsGenerationTriggerEnabled(SceneGuidToPathMapGenerationTriggers.BeforeBuild))
            {
                Logger.Warn($"Skipping scene GUID to path map generation before build. It is recommended to enable map generation before build, as an outdated map can result in broken scene references in runtime. You can enable it in {SettingsManager.SettingsMenuPathForDisplay}.");

                return;
            }

            try
            {
                SceneGuidToPathMapGenerator.Run();
            }
            catch (Exception ex)
            {
                if (SettingsManager.SceneGuidToPathMap.FailBuildIfGenerationFails.value)
                {
                    Logger.Error($"Failing the build due to failure during scene GUID to path map generation. It is recommended to keep this behaviour enabled, as a failed map generation can result in broken scene references in runtime. However, if you know what you are doing, you can disable it in {SettingsManager.SettingsMenuPathForDisplay}.");

                    // Only a BuildFailedException fails the build, so wrapping the original exception.
                    throw new BuildFailedException(ex);
                }

                Logger.Error($"Scene GUID to path map generation failed, but not failing the build. It is recommended to enable failing the build if the map generation fails, as a failed map generation can result in broken scene references in runtime. You can enable it in {SettingsManager.SettingsMenuPathForDisplay}.");

                // Only a BuildFailedException fails the build, therefore rethrowing does not.
                throw;
            }
        }
    }
}
