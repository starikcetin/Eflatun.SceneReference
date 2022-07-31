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
            if (!SettingsManager.IsGenerationTriggerEnabled(MapGenerationTriggers.BeforeEnterPlayMode))
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
