using Eflatun.SceneReference.Editor.Map;
using UnityEditor;
using UnityEditor.SettingsManagement;
using Newtonsoft.Json;

namespace Eflatun.SceneReference.Editor
{
    public static class SettingsManager
    {
        private static readonly Settings Instance = new("com.eflatun.scenereference");

        [SettingsProvider]
        private static SettingsProvider CreateUserSettingsProvider() => new UserSettingsProvider("Project/Eflatun/Scene Reference", Instance, new [] { typeof(SettingsManager).Assembly }, SettingsScope.Project);
        
        [field: UserSetting("Generator", "Map Regeneration Triggers", "Controls when the scene map will be regenerated.\n\n• After Scene Asset Change: Regenerate the map every time a scene asset changes (deleted, created, moved, renamed).\n\n• Before Enter Play Mode: Regenerate the map before entering play mode in the editor.\n\n• Before Build: Regenerate the map before a build.\n\nIt is recommended that you leave this option at 'All' unless you are debugging something. Failure to regenerate the map when needed will result it broken scene references in runtime.")] 
        public static UserSetting<GenerationTriggers> MapGenerationTriggers { get; } = new(Instance, "MapRegenerationTriggers", GenerationTriggers.All, SettingsScope.Project);
        
                
        [field: UserSetting("Generator", "Json Formatting", "Controls the generator's json formatting. It is recommended to leave this option at Indented, as it will help with version control and make the file human-readable.")] 
        public static UserSetting<Formatting> JsonFormatting { get; } = new(Instance, "JsonFormatting", Formatting.Indented, SettingsScope.Project);
    }
}
