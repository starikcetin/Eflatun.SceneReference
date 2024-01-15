using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Eflatun.SceneReference.Editor.MapGeneratorTriggers
{
    internal class BuildPostProcessor : IPostprocessBuildWithReport
    {
        public int callbackOrder => -100;

        public void OnPostprocessBuild(BuildReport report)
        {
            SceneDataMapsGenerator.CleanFileOutput();
        }
    }
}
