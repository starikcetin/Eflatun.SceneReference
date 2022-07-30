using UnityEditor;

namespace Eflatun.SceneReference.Editor.Map
{
    [InitializeOnLoad]
    public static class PlayModeListener
    {
        private static bool ShouldRun => (SettingsManager.MapGenerationTriggers.value & GenerationTriggers.BeforeEnterPlayMode) == GenerationTriggers.BeforeEnterPlayMode;
        
        static PlayModeListener()
        {
            EditorApplication.playModeStateChanged += EditorApplication_OnPlayModeStateChanged;
        }

        private static void EditorApplication_OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (!ShouldRun)
            {
                return;
            }

            if (state == PlayModeStateChange.ExitingEditMode)
            {
                Generator.Run();
            }
        }
    }
}
