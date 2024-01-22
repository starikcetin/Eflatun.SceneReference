using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Eflatun.SceneReference.Editor.MapGeneratorTriggers
{
    internal class BuildPostProcessor : IPostprocessBuildWithReport
    {
        public int callbackOrder => -100;

        public void OnPostprocessBuild(BuildReport report)
        {
            if (!SettingsManager.SceneDataMaps.IsGenerationTriggerEnabled(SceneDataMapsGeneratorTriggers.BeforeBuild))
            {
                // do not log anything, as a warning is already logged in BuildPreProcessor
                return;
            }

            SceneDataMapsGenerator.CleanFileOutput();
        }
    }
}
