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

        private SerializedProperty _assetSerializedProperty;
        private SerializedProperty _guidSerializedProperty;

        private UnityEngine.Object _asset;
        private string _guid;
        private string _path;
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
            _assetSerializedProperty = property.FindPropertyRelative(nameof(SceneReference.asset));
            _guidSerializedProperty = property.FindPropertyRelative(nameof(SceneReference.guid));

            _asset = _assetSerializedProperty.objectReferenceValue;
            _guid = _guidSerializedProperty.stringValue;
            _path = AssetDatabase.GetAssetPath(_asset);
            _sceneInBuildSettings = EditorBuildSettings.scenes.FirstOrDefault(x => x.guid.ToString() == _guid);

            _optionsAttribute = fieldInfo.GetCustomAttribute<SceneReferenceOptionsAttribute>(false) ?? DefaultOptionsAttribute;

            if (_asset == null)
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

        private void SetWith(UnityEngine.Object newAsset)
        {
            if (_asset == newAsset)
            {
                return;
            }

            _assetSerializedProperty.objectReferenceValue = newAsset;

            var newPath = AssetDatabase.GetAssetPath(newAsset);
            var newGuid = AssetDatabase.GUIDFromAssetPath(newPath);
            _guidSerializedProperty.stringValue = newGuid.ToString();
        }

        private void FixInBuildSettings()
        {
            var changed = false;
            var tempScenes = EditorBuildSettings.scenes.ToList();

            if (_sceneBuildSettingsState == SceneBuildSettingsState.NotIncluded)
            {
                var title = "Add Scene to Build Settings?";
                var body = $"Would you like to add the following scene to build settings?\n\n{_path}";

                switch (EditorUtility.DisplayDialogComplex(title, body, "Add to Build", "Cancel", "Open Build Settings"))
                {
                    case 0:
                    {
                        tempScenes.Add(new EditorBuildSettingsScene(_path, true));
                        changed = true;
                        break;
                    }
                    case 1:
                    {
                        // 1 is cancel
                        break;
                    }
                    case 2:
                    {
                        PingAssetAndOpenBuildSettings();
                        break;
                    }
                }
            }
            else if (_sceneBuildSettingsState == SceneBuildSettingsState.Disabled)
            {
                var title = "Enable Scene in Build Settings?";
                var body = $"Would you like to enable the following scene in build settings?\n\n{_path}";

                switch (EditorUtility.DisplayDialogComplex(title, body, "Enable in Build", "Cancel", "Open Build Settings"))
                {
                    case 0:
                    {
                        tempScenes.Single(x => x.guid.ToString() == _guid).enabled = true;
                        changed = true;
                        break;
                    }
                    case 1:
                    {
                        // 1 is cancel
                        break;
                    }
                    case 2:
                    {
                        PingAssetAndOpenBuildSettings();
                        break;
                    }
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
            var newAsset = EditorGUI.ObjectField(selectorFieldRect, _asset, typeof(SceneAsset), false);
            SetWith(newAsset);

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

                    // This prevents Unity from throwing 'InvalidOperationException: Stack empty.' at 'EditorGUI.EndProperty'.
                    GUIUtility.ExitGUI();
                }
            }

            GUI.color = colorToRestore;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            Init(property);

            return _asset && IsUtilityLineEnabled && NeedsBuildSettingsFix
                ? EditorGUIUtility.singleLineHeight * 2
                : EditorGUIUtility.singleLineHeight;
        }

        private void PingAssetAndOpenBuildSettings()
        {
            EditorGUIUtility.PingObject(_asset);
            EditorWindow.GetWindow<BuildPlayerWindow>();
        }
    }
}
