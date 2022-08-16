using System;
using System.Linq;
using System.Reflection;
using Eflatun.SceneReference.Editor.Utility;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Eflatun.SceneReference.Editor
{
    /// <summary>
    /// Custom property drawer for <see cref="SceneReference"/>.
    /// </summary>
    [PublicAPI]
    [CustomPropertyDrawer(typeof(SceneReference))]
    public class SceneReferencePropertyDrawer : PropertyDrawer
    {
        private static readonly SceneReferenceOptionsAttribute DefaultOptionsAttribute = new SceneReferenceOptionsAttribute();

        private enum SceneBuildSettingsState
        {
            None,
            NotIncluded,
            Disabled,
            Enabled
        }

        private SerializedProperty _sceneAssetProperty;
        private SerializedProperty _sceneAssetGuidHexProperty;

        private UnityEngine.Object _sceneAsset;
        private string _sceneAssetGuidHex;
        private string _scenePath;
        private EditorBuildSettingsScene _sceneInBuildSettings;
        private SceneBuildSettingsState _sceneBuildSettingsState;
        private SceneReferenceOptionsAttribute _optionsAttribute;

        private bool NeedsBuildSettingsFix => _sceneBuildSettingsState == SceneBuildSettingsState.Disabled
                                              || _sceneBuildSettingsState == SceneBuildSettingsState.NotIncluded;

        private bool IsColoringEnabled => _optionsAttribute.Coloring switch
        {
            ColoringBehaviour.Enabled => true,
            ColoringBehaviour.Disabled => false,
            ColoringBehaviour.DoNotOverride => SettingsManager.PropertyDrawer.ColorBasedOnSceneInBuildState.value,
            _ => throw new ArgumentOutOfRangeException(nameof(_optionsAttribute.Coloring), _optionsAttribute.Coloring, null)
        };

        private bool IsUtilityLineEnabled => _optionsAttribute.UtilityLine switch
        {
            UtilityLineBehaviour.Enabled => true,
            UtilityLineBehaviour.Disabled => false,
            UtilityLineBehaviour.DoNotOverride => SettingsManager.PropertyDrawer.ShowInlineSceneInBuildUtility.value,
            _ => throw new ArgumentOutOfRangeException(nameof(_optionsAttribute.UtilityLine), _optionsAttribute.UtilityLine, null)
        };

        private void Init(SerializedProperty property)
        {
            _sceneAssetProperty = property.FindPropertyRelative(nameof(SceneReference.sceneAsset));
            _sceneAssetGuidHexProperty = property.FindPropertyRelative(nameof(SceneReference.sceneAssetGuidHex));

            _sceneAsset = _sceneAssetProperty.objectReferenceValue;
            _sceneAssetGuidHex = _sceneAssetGuidHexProperty.stringValue;
            _scenePath = AssetDatabase.GetAssetPath(_sceneAsset);
            _sceneInBuildSettings = EditorBuildSettings.scenes.FirstOrDefault(x => x.guid.ToString() == _sceneAssetGuidHex);

            _optionsAttribute = fieldInfo.GetCustomAttribute<SceneReferenceOptionsAttribute>(false) ?? DefaultOptionsAttribute;

            if (_sceneAsset == null)
            {
                _sceneBuildSettingsState = SceneBuildSettingsState.None;
            }
            else if (_sceneInBuildSettings == null)
            {
                _sceneBuildSettingsState = SceneBuildSettingsState.NotIncluded;
            }
            else if (!_sceneInBuildSettings.enabled)
            {
                _sceneBuildSettingsState = SceneBuildSettingsState.Disabled;
            }
            else
            {
                _sceneBuildSettingsState = SceneBuildSettingsState.Enabled;
            }
        }

        private void SetWith(UnityEngine.Object newSceneAsset)
        {
            if (_sceneAsset == newSceneAsset)
            {
                return;
            }

            _sceneAssetProperty.objectReferenceValue = newSceneAsset;

            var sceneAssetPath = AssetDatabase.GetAssetPath(newSceneAsset);
            var sceneAssetGuid = AssetDatabase.GUIDFromAssetPath(sceneAssetPath);
            _sceneAssetGuidHexProperty.stringValue = sceneAssetGuid.ToString();
        }

        private void FixInBuildSettings()
        {
            var changed = false;
            var tempScenes = EditorBuildSettings.scenes.ToList();

            if (_sceneBuildSettingsState == SceneBuildSettingsState.NotIncluded)
            {
                var title = "Add Scene to Build Settings?";
                var body = $"Would you like to add the following scene to build settings?\n\n{_scenePath}";

                switch (EditorUtility.DisplayDialogComplex(title, body, "Add to Build as Enabled", "Add to Build as Disabled", "Cancel"))
                {
                    case 0:
                    {
                        tempScenes.Add(new EditorBuildSettingsScene(_scenePath, true));
                        changed = true;
                        break;
                    }
                    case 1:
                    {
                        tempScenes.Add(new EditorBuildSettingsScene(_scenePath, false));
                        changed = true;
                        break;
                    }
                }
            }
            else if (_sceneBuildSettingsState == SceneBuildSettingsState.Disabled)
            {
                var title = "Enable Scene in Build Settings?";
                var body = $"Would you like to enable the following scene in build settings?\n\n{_scenePath}";

                if (EditorUtility.DisplayDialog(title, body, "Enable in Build", "Cancel"))
                {
                    tempScenes.Single(x => x.guid.ToString() == _sceneAssetGuidHex).enabled = true;
                    changed = true;
                }
            }

            if (changed)
            {
                EditorBuildSettings.scenes = tempScenes.ToArray();
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Init(property);

            EditorGUI.BeginProperty(position, GUIContent.none, property);

            var colorToRestore = GUI.color;

            if (IsColoringEnabled)
            {
                GUI.color = _sceneBuildSettingsState switch
                {
                    SceneBuildSettingsState.NotIncluded => Color.red,
                    SceneBuildSettingsState.Disabled => Color.yellow,
                    _ => colorToRestore
                };
            }

            // draw scene asset selector
            var selectorFieldRect = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            selectorFieldRect.height = EditorGUIUtility.singleLineHeight;
            var newSceneAsset = EditorGUI.ObjectField(selectorFieldRect, _sceneAsset, typeof(SceneAsset), false);
            SetWith(newSceneAsset);

            // draw utility line if needed
            if (IsUtilityLineEnabled && NeedsBuildSettingsFix)
            {
                var buttonRect = new Rect(position)
                {
                    // x = selectorFieldRect.x,                    
                    y = position.y + EditorGUIUtility.singleLineHeight,
                    // width = selectorFieldRect.width,
                    height = EditorGUIUtility.singleLineHeight
                };

                var buttonText = _sceneBuildSettingsState switch
                {
                    SceneBuildSettingsState.NotIncluded => "Add to Build...",
                    SceneBuildSettingsState.Disabled => "Enable in Build...",
                    _ => throw new ArgumentOutOfRangeException(nameof(_sceneBuildSettingsState), _sceneBuildSettingsState, null)
                };

                if (GUI.Button(buttonRect, buttonText))
                {
                    FixInBuildSettings();
                }
            }

            GUI.color = colorToRestore;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            Init(property);

            return _sceneAsset && IsUtilityLineEnabled && NeedsBuildSettingsFix
                ? EditorGUIUtility.singleLineHeight * 2
                : EditorGUIUtility.singleLineHeight;
        }
    }
}
