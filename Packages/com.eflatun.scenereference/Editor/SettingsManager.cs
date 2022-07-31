using System.Reflection;
using Eflatun.SceneReference.Utility;
using JetBrains.Annotations;
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
    [PublicAPI]
    public static class SettingsManager
    {
        private const string SettingsMenuPath = "Project/" + Constants.MenuPrefixBase;
        internal const string SettingsMenuPathForDisplay = "Project Settings/" + SettingsMenuPath;
        
        private static readonly Assembly ContainingAssembly = typeof(SettingsManager).Assembly;
        private static readonly Settings Settings = new(Constants.PackageNameReverseDomain);

        [SettingsProvider]
        private static SettingsProvider CreateUserSettingsProvider() => new UserSettingsProvider(SettingsMenuPath, Settings, new[] { ContainingAssembly }, SettingsScope.Project);

        /// <summary>
        /// Settings regarding the scene GUID to path map.
        /// </summary>
        /// <remarks><inheritdoc cref="SettingsManager"/></remarks>
        [PublicAPI]
        public static class SceneGuidToPathMap
        {
            private const string CategoryName = "Scene GUID To Path Map";

            /// <summary>
            /// Controls when the scene GUID to path map gets regenerated.<br/>
            /// • After Scene Asset Change: Regenerate the map every time a scene asset changes (delete, create, move, rename).<br/>
            /// • Before Enter Play Mode: Regenerate the map before entering play mode in the editor.<br/>
            /// • Before Build: Regenerate the map before a build.<br/>
            /// It is recommended that you leave this option at 'All' unless you are debugging something. Failure to generate the map when needed can result in broken scene references in runtime.
            /// </summary>
            /// <remarks><inheritdoc cref="SettingsManager"/></remarks>
            /// <seealso cref="IsGenerationTriggerEnabled"/>
            [field: UserSetting(CategoryName, "Generation Triggers", "Controls when the scene GUID to path map gets regenerated.\n\n• After Scene Asset Change: Regenerate the map every time a scene asset changes (delete, create, move, rename).\n\n• Before Enter Play Mode: Regenerate the map before entering play mode in the editor.\n\n• Before Build: Regenerate the map before a build.\n\nIt is recommended that you leave this option at 'All' unless you are debugging something. Failure to generate the map when needed can result in broken scene references in runtime.")]
            public static UserSetting<SceneGuidToPathMapGenerationTriggers> GenerationTriggers { get; }
                = new(Settings, "SceneGuidToPathMap.GenerationTriggers", SceneGuidToPathMapGenerationTriggers.All, SettingsScope.Project);

            /// <summary>
            /// Controls the scene GUID to path map generator's JSON formatting.<br/>
            /// It is recommended to leave this option at 'Indented', as it will help with version control and make the generated file human-readable.
            /// </summary>
            /// <remarks><inheritdoc cref="SettingsManager"/></remarks>
            [field: UserSetting(CategoryName, "JSON Formatting", "Controls the scene GUID to path map generator's JSON formatting.\n\nIt is recommended to leave this option at 'Indented', as it will help with version control and make the generated file human-readable.")]
            public static UserSetting<Formatting> JsonFormatting { get; }
                = new(Settings, "SceneGuidToPathMap.JsonFormatting", Formatting.Indented, SettingsScope.Project);

            /// <summary>
            /// Should we fail a build if scene GUID to path map generation fails?<br/>
            /// Only relevant if 'Before Build' generation trigger is enabled.<br/>
            /// It is recommended to leave this option at 'true', as a failed map generation can result in broken scene references in runtime.
            /// </summary>
            /// <remarks><inheritdoc cref="SettingsManager"/></remarks>
            [field: UserSetting(CategoryName, "Fail Build If Generation Fails", "Should we fail a build if scene GUID to path map generation fails?\n\nOnly relevant if 'Before Build' generation trigger is enabled.\n\nIt is recommended to leave this option at 'true', as a failed map generation can result in broken scene references in runtime.")]
            public static UserSetting<bool> FailBuildIfGenerationFails { get; }
                = new(Settings, "SceneGuidToPathMap.FailBuildIfGenerationFails", true, SettingsScope.Project);

            /// <summary>
            /// Returns whether the given <paramref name="trigger"/> is enabled in the settings.
            /// </summary>
            /// <seealso cref="GenerationTriggers"/>
            public static bool IsGenerationTriggerEnabled(SceneGuidToPathMapGenerationTriggers trigger) =>
                GenerationTriggers.value.IncludesFlag(trigger);
        }
    }
}
