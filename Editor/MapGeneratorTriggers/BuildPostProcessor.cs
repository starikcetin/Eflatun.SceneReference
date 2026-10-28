using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Eflatun.SceneReference.Editor.MapGeneratorTriggers
{
    internal class BuildPostProcessor :
// Backwards compatibility: IPostprocessBuildWithContext, which Unity also invokes when the build fails or gets cancelled, is only available in Unity 6000.3 and newer (https://github.com/starikcetin/Eflatun.SceneReference/issues/125).
#if UNITY_6000_3_OR_NEWER
        IPostprocessBuildWithContext
#else // UNITY_6000_3_OR_NEWER
        IPostprocessBuildWithReport
#endif // UNITY_6000_3_OR_NEWER
    {
        public int callbackOrder => -100;

#if UNITY_6000_3_OR_NEWER
        public void OnPostprocessBuild(BuildCallbackContext ctx)
        {
            // Unity also invokes this for content-only builds, such as AssetBundle builds, which do not output the files.
            if (!ctx.IsPlayerBuild)
            {
                return;
            }

            CleanFileOutputIfTriggerEnabled();
        }
#else // UNITY_6000_3_OR_NEWER
        public void OnPostprocessBuild(BuildReport report)
        {
            CleanFileOutputIfTriggerEnabled();
        }
#endif // UNITY_6000_3_OR_NEWER

        private static void CleanFileOutputIfTriggerEnabled()
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
