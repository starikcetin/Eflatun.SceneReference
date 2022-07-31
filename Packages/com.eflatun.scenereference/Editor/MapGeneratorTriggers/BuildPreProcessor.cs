using System;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Eflatun.SceneReference.Editor.MapGeneratorTriggers
{
    internal class BuildPreProcessor : IPreprocessBuildWithReport
    {
        private static bool ShouldRun => (SettingsManager.MapGenerationTriggers.value & MapGenerationTriggers.BeforeBuild) == MapGenerationTriggers.BeforeBuild;

        public int callbackOrder => -100;
        
        public void OnPreprocessBuild(BuildReport report)
        {
            if (!ShouldRun)
            {
                return;
            }
            
            try
            {
                MapGenerator.Run();
            }
            catch (Exception ex)
            {
                throw new BuildFailedException(ex);
            }
        }
    }
}
