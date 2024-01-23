using Eflatun.SceneReference.Editor.Utility;
using Eflatun.SceneReference.Utility;
using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Reflection;
using UnityEditor;
using UnityEditor.SettingsManagement;
using UnityEngine;

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
        /// <inheritdoc cref="UserSetting{T}"/>
        public class ProjectSetting<T> : UserSetting<T>
        {
            /// <inheritdoc cref="UserSetting{T}(Settings,string,T,SettingsScope)"/>
            internal ProjectSetting(string key, T value) : base(Settings, key, value, SettingsScope.Project)
            {
            }
        }

        private const string SettingsMenuPath = "Project/" + Constants.MenuPrefixBase;
        internal const string SettingsMenuPathForDisplay = "Project Settings/" + Constants.MenuPrefixBase;

        private static readonly Assembly ContainingAssembly = typeof(SettingsManager).Assembly;
        private static readonly Settings Settings = new Settings(Constants.PackageNameReverseDomain);

        [SettingsProvider]
        private static SettingsProvider CreateUserSettingsProvider() => new UserSettingsProvider(SettingsMenuPath, Settings, new[] { ContainingAssembly }, SettingsScope.Project);

        /// <summary>
        /// Settings regarding the scene data maps and <see cref="SceneDataMapsGenerator"/>.
        /// </summary>
        /// <remarks><inheritdoc cref="SettingsManager"/></remarks>
        [PublicAPI]
        public static class SceneDataMaps
        {
            private const string CategoryName = "Scene Data Maps";

            /// <summary>
            /// Controls when the scene data maps get regenerated.<br/>
            /// <list type="bullet">
            /// <item>After Scene Asset Change: Regenerate the maps every time a scene asset changes (delete, create, move, rename).</item>
            /// <item>Before Enter Play Mode: Regenerate the maps before entering play mode in the editor.</item>
            /// <item>Before Build: Regenerate the maps before a build.</item>
            /// <item>After Packages Resolve: Regenerate the maps after UPM packages are resolved.</item>
            /// <item>After Addressables Change: Regenerate the maps after addressable group entries change. Only relevant if you have addressables support enabled.</item>
            /// </list>
            /// It is recommended that you leave this option at <c>All</c> unless you are debugging something. Failure to generate the maps when needed can result in broken scene references in runtime.
            /// </summary>
            /// <remarks>
            /// <c>All</c> and <c>Everything</c> are the same thing. They both represent all triggers.<p/>
            /// <inheritdoc cref="SettingsManager"/>
            /// </remarks>
            /// <seealso cref="IsGenerationTriggerEnabled"/>
            [field: UserSetting]
            public static ProjectSetting<SceneDataMapsGeneratorTriggers> GenerationTriggers { get; }
                = new ProjectSetting<SceneDataMapsGeneratorTriggers>("SceneDataMaps.GenerationTriggers", SceneDataMapsGeneratorTriggers.All);

            /// <summary>
            /// Controls the Scene Data Maps Generator's JSON formatting.<br/>
            /// It is recommended to leave this option at <see cref="Formatting.None"/>, as it will make the generated files smaller in size.
            /// </summary>
            /// <remarks><inheritdoc cref="SettingsManager"/></remarks>
            [field: UserSetting(CategoryName, "JSON Formatting", "Controls the Scene Data Maps Generator's JSON formatting.\n\nIt is recommended to leave this option at 'None', as it will make the generated files smaller in size.")]
            public static ProjectSetting<Formatting> JsonFormatting { get; }
                = new ProjectSetting<Formatting>("SceneDataMaps.JsonFormatting", Formatting.None);

            /// <summary>
            /// Should we fail a build if scene data maps generation fails?<br/>
            /// Only relevant if <c>Before Build</c> generation trigger is enabled.<br/>
            /// It is recommended to leave this option at <c>true</c>, as a failed map generation can result in broken scene references in runtime.
            /// </summary>
            /// <remarks><inheritdoc cref="SettingsManager"/></remarks>
            [field: UserSetting(CategoryName, "Fail Build If Generation Fails", "Should we fail a build if scene data maps generation fails?\n\nOnly relevant if 'Before Build' generation trigger is enabled.\n\nIt is recommended to leave this option at 'true', as a failed map generation can result in broken scene references in runtime.")]
            public static ProjectSetting<bool> FailBuildIfGenerationFails { get; }
                = new ProjectSetting<bool>("SceneDataMaps.FailBuildIfGenerationFails", true);

            /// <summary>
            /// Returns whether the given <paramref name="trigger"/> is enabled in the settings.
            /// </summary>
            /// <remarks>This is a utility method for code access, it is not part of the settings data.</remarks>
            /// <seealso cref="GenerationTriggers"/>
            public static bool IsGenerationTriggerEnabled(SceneDataMapsGeneratorTriggers trigger) =>
                GenerationTriggers.value.IncludesFlag(trigger);

            [UserSettingBlock(CategoryName)]
            private static void Draw(string searchContext)
            {
                // Draw GenerationTriggers field.
                {
                    var oldPublic = GenerationTriggers.value;
                    var oldInternal = (InternalSceneDataMapsGeneratorTriggers)oldPublic;
                    var label = new GUIContent("Generation Triggers", "Controls when the scene data maps get regenerated.\n\n• After Scene Asset Change: Regenerate the maps every time a scene asset changes (delete, create, move, rename).\n\n• Before Enter Play Mode: Regenerate the maps before entering play mode in the editor.\n\n• Before Build: Regenerate the maps before a build.\n\n• After Packages Resolve: Regenerate the maps after UPM packages are resolved.\n\n• After Addressables Change: Regenerate the maps after addressable group entries change. Only relevant if you have addressables support enabled.\n\nIt is recommended that you leave this option at 'All' unless you are debugging something. Failure to generate the maps when needed can result in broken scene references in runtime.\n\nNote: 'All' and 'Everything' are the same thing. They both represent all triggers.");
                    var newInternal = EditorGUILayout.EnumFlagsField(label, oldInternal);
                    SettingsGUILayout.DoResetContextMenuForLastRect(GenerationTriggers);
                    var newPublic = (SceneDataMapsGeneratorTriggers)newInternal;
                    if (newPublic != oldPublic)
                    {
                        GenerationTriggers.value = newPublic;
                    }
                }
            }
        }

        /// <summary>
        /// Settings regarding <see cref="SceneReferencePropertyDrawer"/>.
        /// </summary>
        /// <remarks><inheritdoc cref="SettingsManager"/></remarks>
        [PublicAPI]
        public static class PropertyDrawer
        {
            private const string CategoryName = "Property Drawer";

            /// <summary>
            /// Should we show the inline gear (⚙️) button that opens a toolbox?<br/>
            /// Unity only bundles scenes that are added and enabled in build settings, and addressables only pack scenes that are in an Addressable Group. Therefore, you would want to make sure the scene you assign to a SceneReference is either added and enabled in build settings, or is in an addressable group. The toolbox provides tools for you to quickly take action in these cases.<br/>
            /// It is recommended to leave this option enabled, as the toolbox saves you a lot of time.
            /// </summary>
            /// <remarks><inheritdoc cref="SettingsManager"/></remarks>
            [field: UserSetting(CategoryName, "Show Inline Toolbox", "Should we show the inline gear button that opens a toolbox?\n\nUnity only bundles scenes that are added and enabled in build settings, and addressables only pack scenes that are in an Addressable Group. Therefore, you would want to make sure the scene you assign to a SceneReference is either added and enabled in build settings, or is in an addressable group. The toolbox provides tools for you to quickly take action in these cases.\n\nIt is recommended to leave this option enabled, as the toolbox saves you a lot of time.")]
            public static ProjectSetting<bool> ShowInlineToolbox { get; }
                = new ProjectSetting<bool>("PropertyDrawer.ShowInlineToolbox", true);

            /// <summary>
            /// Should we color the property to draw attention for scenes that are either not in build or disabled in build?<br/>
            /// Unity only bundles scenes that are added and enabled in build settings. Therefore, you would want to validate whether the scene you assign to a SceneReference is added and enabled in build settings.<br/>
            /// It is recommended to leave this option at 'true', as it will help you identify many potential runtime errors.
            /// </summary>
            /// <remarks>
            /// This setting does not apply to addressable scenes. They have their own coloring mechanism. It is controlled by the <see cref="AddressablesSupport.ColorAddressableScenes"/> setting under the <see cref="AddressablesSupport"/> category.<br/>
            /// <inheritdoc cref="SettingsManager"/>
            /// </remarks>
            [field: UserSetting(CategoryName, "Color Based On Scene-In-Build State", "Should we color the property to draw attention for scenes that are either not in build or disabled in build?\n\nUnity only bundles scenes that are added and enabled in build settings. Therefore, you would want to validate whether the scene you assign to a SceneReference is added and enabled in build settings.\n\nIt is recommended to leave this option at 'true', as it will help you identify many potential runtime errors.\n\nNote: This setting does not apply to addressable scenes. They have their own coloring mechanism. It is controlled by the 'Color Addressable Scenes' setting under the 'Addressables Support' category.")]
            public static ProjectSetting<bool> ColorBasedOnSceneInBuildState { get; }
                = new ProjectSetting<bool>("PropertyDrawer.ColorBasedOnSceneInBuildState", true);
        }

        /// <summary>
        /// Settings regarding addressables support.
        /// </summary>
        /// <remarks>
        /// Settings under this category are only relevant if you have addressables support enabled.<br/>
        /// <inheritdoc cref="SettingsManager"/>
        /// </remarks>
        [PublicAPI]
        public static class AddressablesSupport
        {
            private const string CategoryName = "Addressables Support";

            /// <summary>
            /// Should we color the property to draw attention for addressable scenes?<br/>
            /// Addressable scenes should be handled differently than regular scenes in runtime, through the addressables API. Therefore, you would want quickly identify if a <see cref="SceneReference"/> references an addressable scene or not.<br/>
            /// It is recommended to leave this option at <c>true</c>, as it will help you easily distinguish addressable scenes.
            /// </summary>
            /// <remarks>
            /// This setting does not apply to regular scenes. They have their own coloring mechanism. It is controlled by the <see cref="PropertyDrawer.ColorBasedOnSceneInBuildState"/> setting under the <see cref="PropertyDrawer"/> category.<br/>
            /// <inheritdoc cref="SettingsManager"/>
            /// </remarks>
            [field: UserSetting(CategoryName, "Color Addressable Scenes", "Should we color the property to draw attention for addressable scenes?\n\nAddressable scenes should be handled differently than regular scenes in runtime, through the addressables API. Therefore, you would want quickly identify if a SceneReference references an addressable scene or not.\n\nIt is recommended to leave this option at 'true', as it will help you easily distinguish addressable scenes.\n\nNote: This setting does not apply to regular scenes. They have their own coloring mechanism. It is controlled by the 'Color Based On Scene-In-Build State' setting under the 'Property Drawer' category.")]
            public static ProjectSetting<bool> ColorAddressableScenes { get; }
                = new ProjectSetting<bool>("AddressablesSupport.ColorAddressableScenes", true);

            [UserSettingBlock(CategoryName)]
            private static void Draw(string searchContext)
            {
                if (!EditorUtils.IsAddressablesPackagePresent)
                {
                    EditorGUILayout.HelpBox("Addressables support is disabled because the addressables package is not installed in the project. Addressables support will be enabled automatically when the addressables package is installed in the project.", MessageType.Warning);
                }
                else
                {
                    EditorGUILayout.HelpBox("Addressables support is enabled.", MessageType.Info);
                }
            }
        }

        /// <summary>
        /// Settings regarding logging.
        /// </summary>
        /// <remarks>
        /// Exceptions will always be logged.<p/>
        /// <inheritdoc cref="SettingsManager"/>
        /// </remarks>
        [PublicAPI]
        public static class Logging
        {
            private const string CategoryName = "Logging";

            /// <summary>
            /// Log level for the editor logger. It is recommended to leave this at <see cref="LogLevel.Warning"/>.
            /// </summary>
            /// <remarks>
            /// Exceptions will always be logged.<p/>
            /// <inheritdoc cref="SettingsManager"/>
            /// </remarks>
            [field: UserSetting(CategoryName, "Editor Log Level", "Log level for the editor logger. It is recommended to leave this at 'Warning'.")]
            public static ProjectSetting<LogLevel> EditorLogLevel { get; }
                = new ProjectSetting<LogLevel>("Logging.EditorLogLevel", LogLevel.Warning);
        }
    }
}
