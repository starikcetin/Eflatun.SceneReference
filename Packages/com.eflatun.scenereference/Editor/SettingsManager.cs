using Eflatun.SceneReference.Utility;
using UnityEditor;
using UnityEditor.SettingsManagement;
using Newtonsoft.Json;

namespace Eflatun.SceneReference.Editor
{
    /// <summary>
    /// Manages and contains settings for Scene Reference.
    /// </summary>
    /// <remarks>
    /// Changing the settings from code may have unintended consequences. Make sure you now what you are doing.
    /// </remarks>
    public static class SettingsManager
    {
        private static readonly Settings Settings = new("com.eflatun.scenereference");

        [SettingsProvider]
        private static SettingsProvider CreateUserSettingsProvider() => new UserSettingsProvider("Project/Eflatun/Scene Reference", Settings, new[] { typeof(SettingsManager).Assembly }, SettingsScope.Project);

        /// <remarks><inheritdoc cref="SettingsManager"/></remarks>
        [field: UserSetting("Map", "Generation Triggers", "Controls when the scene map will be regenerated.\n\n• After Scene Asset Change: Regenerate the map every time a scene asset changes (deleted, created, moved, renamed).\n\n• Before Enter Play Mode: Regenerate the map before entering play mode in the editor.\n\n• Before Build: Regenerate the map before a build.\n\nIt is recommended that you leave this option at 'All' unless you are debugging something. Failure to regenerate the map when needed will result it broken scene references in runtime.")]
        public static UserSetting<MapGenerationTriggers> MapGenerationTriggers { get; }
            = new(Settings, "MapGenerationTriggers", Editor.MapGenerationTriggers.All, SettingsScope.Project);

        /// <remarks><inheritdoc cref="SettingsManager"/></remarks>
        [field: UserSetting("Map", "JSON Formatting", "Controls the generator's json formatting.\n\nIt is recommended to leave this option at Indented, as it will help with version control and make the file human-readable.")]
        public static UserSetting<Formatting> MapJsonFormatting { get; }
            = new(Settings, "MapJsonFormatting", Formatting.Indented, SettingsScope.Project);

        /// <remarks><inheritdoc cref="SettingsManager"/></remarks>
        [field: UserSetting("Map", "Fail Build If Generation Fails", "Should we fail a build if scene map generation fails?\n\nOnly relevant if Before Build Generation Trigger is enabled.\n\nIt is recommended to leave this option at true, as a failed scene map generation will result in broken scene references at runtime.")]
        public static UserSetting<bool> FailBuildIfMapGenerationFails { get; }
            = new(Settings, "FailBuildIfMapGenerationFails", true, SettingsScope.Project);

        public static bool IsGenerationTriggerEnabled(MapGenerationTriggers trigger) =>
            MapGenerationTriggers.value.IncludesFlag(trigger);
    }
}
