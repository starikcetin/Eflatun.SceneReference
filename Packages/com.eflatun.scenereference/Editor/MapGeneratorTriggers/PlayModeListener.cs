using UnityEditor;

namespace Eflatun.SceneReference.Editor.MapGeneratorTriggers
{
    [InitializeOnLoad]
    internal static class PlayModeListener
    {
        private static bool ShouldRun => (SettingsManager.MapGenerationTriggers.value & MapGenerationTriggers.BeforeEnterPlayMode) == MapGenerationTriggers.BeforeEnterPlayMode;
        
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
                MapGenerator.Run();
            }
        }
    }
}
