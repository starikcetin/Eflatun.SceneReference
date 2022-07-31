using System;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Eflatun.SceneReference.Editor.MapGeneratorTriggers
{
    internal class BuildPreProcessor : IPreprocessBuildWithReport
    {
        public int callbackOrder => -100;

        public void OnPreprocessBuild(BuildReport report)
        {
            if (!SettingsManager.IsGenerationTriggerEnabled(MapGenerationTriggers.BeforeBuild))
            {
                Debug.LogWarning("Skipping SceneReference map generation before build. It is recommended to enable map generation before build, as an outdated map can result in broken scene references in runtime. You can enable it in Project Settings/Eflatun/Scene Reference.");
                
                return;
            }

            try
            {
                MapGenerator.Run();
            }
            catch (Exception ex)
            {
                if (SettingsManager.FailBuildIfMapGenerationFails.value)
                {
                    Debug.LogError("Failing the build due to SceneReference map generation failure. It is recommended to keep this behaviour enabled, as a failed map generation can result in broken scene references in runtime. However, if you know what you are doing, you can disable it in Project Settings/Eflatun/Scene Reference.");
                    
                    // Only a BuildFailedException fails the build, so wrapping the original exception.
                    throw new BuildFailedException(ex);
                }

                Debug.LogError("SceneReference map generation failed, but not failing the build. It is recommended to enable failing the build if the map generation fails, as a failed map generation can result in broken scene references in runtime. You can enable it in Project Settings/Eflatun/Scene Reference.");
                
                // Only a BuildFailedException fails the build, therefore rethrowing does not.
                throw;
            }
        }
    }
}
