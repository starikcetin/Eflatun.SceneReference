using UnityEditor;

namespace Eflatun.SceneReference.Editor.MapGeneratorTriggers
{
    [InitializeOnLoad]
    internal static class PlayModeListener
    {
        static PlayModeListener()
        {
            EditorApplication.playModeStateChanged += EditorApplication_OnPlayModeStateChanged;
        }

        private static void EditorApplication_OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                if (SettingsManager.SceneDataMaps.IsGenerationTriggerEnabled(SceneDataMapsGeneratorTriggers.BeforeEnterPlayMode))
                {
                    SceneDataMapsGenerator.Run(false);
                }
                else
                {
                    // DESIGN: If 'Clear On Play' is enabled, this will get cleared before user gets a chance to see 
                    // it. It is easy to fix: just log in EnteredPlayMode. But that is overengineering and I don't want
                    // to do it.
                    EditorLogger.Warn($"Skipping scene data maps generation before play mode. It is recommended to enable map generation before play mode, as an outdated map can result in broken scene references in runtime. You can enable it in {SettingsManager.SettingsMenuPathForDisplay}.");
                }
            }
        }
    }
}
