using System;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Eflatun.SceneReference.Editor.Map
{
    public class BuildPreProcessor : IPreprocessBuildWithReport
    {
        private static bool ShouldRun => (SettingsManager.MapGenerationTriggers.value & GenerationTriggers.BeforeBuild) == GenerationTriggers.BeforeBuild;

        public int callbackOrder => -100;
        
        public void OnPreprocessBuild(BuildReport report)
        {
            if (!ShouldRun)
            {
                return;
            }
            
            try
            {
                Generator.Run();
            }
            catch (Exception ex)
            {
                throw new BuildFailedException(ex);
            }
        }
    }
}
