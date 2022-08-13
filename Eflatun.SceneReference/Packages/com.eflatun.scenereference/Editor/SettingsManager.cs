﻿using System.Reflection;
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
        /// <inheritdoc cref="UserSetting{T}"/> 
        public class ProjectSetting<T> : UserSetting<T>
        {
            /// <inheritdoc cref="UserSetting{T}(Settings,string,T,SettingsScope)"/>
            internal ProjectSetting(string key, T value) : base(Settings, key, value, SettingsScope.Project)
            {
            }
        }

        private const string SettingsMenuPath = "Project/" + Constants.MenuPrefixBase;
        internal const string SettingsMenuPathForDisplay = "Project Settings/" + SettingsMenuPath;

        private static readonly Assembly ContainingAssembly = typeof(SettingsManager).Assembly;
        private static readonly Settings Settings = new Settings(Constants.PackageNameReverseDomain);

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
            public static ProjectSetting<SceneGuidToPathMapGenerationTriggers> GenerationTriggers { get; }
                = new ProjectSetting<SceneGuidToPathMapGenerationTriggers>("SceneGuidToPathMap.GenerationTriggers", SceneGuidToPathMapGenerationTriggers.All);

            /// <summary>
            /// Controls the scene GUID to path map generator's JSON formatting.<br/>
            /// It is recommended to leave this option at 'Indented', as it will help with version control and make the generated file human-readable.
            /// </summary>
            /// <remarks><inheritdoc cref="SettingsManager"/></remarks>
            [field: UserSetting(CategoryName, "JSON Formatting", "Controls the scene GUID to path map generator's JSON formatting.\n\nIt is recommended to leave this option at 'Indented', as it will help with version control and make the generated file human-readable.")]
            public static ProjectSetting<Formatting> JsonFormatting { get; }
                = new ProjectSetting<Formatting>("SceneGuidToPathMap.JsonFormatting", Formatting.Indented);

            /// <summary>
            /// Should we fail a build if scene GUID to path map generation fails?<br/>
            /// Only relevant if 'Before Build' generation trigger is enabled.<br/>
            /// It is recommended to leave this option at 'true', as a failed map generation can result in broken scene references in runtime.
            /// </summary>
            /// <remarks><inheritdoc cref="SettingsManager"/></remarks>
            [field: UserSetting(CategoryName, "Fail Build If Generation Fails", "Should we fail a build if scene GUID to path map generation fails?\n\nOnly relevant if 'Before Build' generation trigger is enabled.\n\nIt is recommended to leave this option at 'true', as a failed map generation can result in broken scene references in runtime.")]
            public static ProjectSetting<bool> FailBuildIfGenerationFails { get; }
                = new ProjectSetting<bool>("SceneGuidToPathMap.FailBuildIfGenerationFails", true);

            /// <summary>
            /// Returns whether the given <paramref name="trigger"/> is enabled in the settings.
            /// </summary>
            /// <seealso cref="GenerationTriggers"/>
            public static bool IsGenerationTriggerEnabled(SceneGuidToPathMapGenerationTriggers trigger) =>
                GenerationTriggers.value.IncludesFlag(trigger);
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
            /// Should we show the inline utility that allows you to quickly fix scenes that are either not in build or disabled in build?<br/>
            /// Unity only bundles scenes that are added and enabled in build settings. Therefore, you would want to make sure the scene you assign to a SceneReference is added and enabled in build settings.<br/>
            /// It is recommended to leave this option at 'true', as the inline utility saves you a lot of time.
            /// </summary>
            /// <remarks><inheritdoc cref="SettingsManager"/></remarks>
            [field: UserSetting(CategoryName, "Show Inline Scene-In-Build Utility", "SShould we show the inline utility that allows you to quickly fix scenes that are either not in build or disabled in build?\n\nUnity only bundles scenes that are added and enabled in build settings. Therefore, you would want to make sure the scene you assign to a SceneReference is added and enabled in build settings.\n\nIt is recommended to leave this option at 'true', as the inline utility saves you a lot of time.")]
            public static ProjectSetting<bool> ShowInlineSceneInBuildUtility { get; }
                = new ProjectSetting<bool>("PropertyDrawer.ShowInlineSceneInBuildUtility", true);

            /// <summary>
            /// Should we color the property to draw attention for scenes that are either not in build or disabled in build?<br/>
            /// Unity only bundles scenes that are added and enabled in build settings. Therefore, you would want to validate whether the scene you assign to a SceneReference is added and enabled in build settings.<br/>
            /// It is recommended to leave this option at 'true', as it will help you identify many potential runtime errors.
            /// </summary>
            /// <remarks><inheritdoc cref="SettingsManager"/></remarks>
            [field: UserSetting(CategoryName, "Color Based On Scene-In-Build State", "Should we color the property to draw attention for scenes that are either not in build or disabled in build?\n\nUnity only bundles scenes that are added and enabled in build settings. Therefore, you would want to validate whether the scene you assign to a SceneReference is added and enabled in build settings.\n\nIt is recommended to leave this option at 'true', as it will help you identify many potential runtime errors.")]
            public static ProjectSetting<bool> ColorBasedOnSceneInBuildState { get; }
                = new ProjectSetting<bool>("PropertyDrawer.ColorBasedOnSceneInBuildState", true);
        }
    }
}
