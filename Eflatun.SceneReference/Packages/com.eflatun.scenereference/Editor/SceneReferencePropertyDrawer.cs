using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Eflatun.SceneReference.Editor.Toolbox;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

#if ESR_ADDRESSABLES
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
#endif // ESR_ADDRESSABLES

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
            Enabled,
#if ESR_ADDRESSABLES
            Addressable,
#endif // ESR_ADDRESSABLES
        }

        private SerializedProperty _assetSerializedProperty;
        private SerializedProperty _guidSerializedProperty;

        private UnityEngine.Object _asset;
        private string _guid;
        private string _path;
        private EditorBuildSettingsScene _sceneInBuildSettings;
        private SceneBuildSettingsState _sceneBuildSettingsState;
        private SceneReferenceOptionsAttribute _optionsAttribute;

#if ESR_ADDRESSABLES
        private AddressableAssetEntry _addressableEntry;
#endif // ESR_ADDRESSABLES

        private bool IsColoringEnabled => _optionsAttribute.Coloring switch
        {
            ColoringBehaviour.Enabled => true,
            ColoringBehaviour.Disabled => false,
            ColoringBehaviour.DoNotOverride => SettingsManager.PropertyDrawer.ColorBasedOnSceneInBuildState.value,
            _ => throw new ArgumentOutOfRangeException(nameof(_optionsAttribute.Coloring), _optionsAttribute.Coloring, null)
        };

        private bool IsToolboxEnabled => _optionsAttribute.Toolbox switch
        {
            ToolboxBehaviour.Enabled => true,
            ToolboxBehaviour.Disabled => false,
            ToolboxBehaviour.DoNotOverride => SettingsManager.PropertyDrawer.ShowInlineToolbox.value,
            _ => throw new ArgumentOutOfRangeException(nameof(_optionsAttribute.Toolbox), _optionsAttribute.Toolbox, null)
        };

        private void Init(SerializedProperty property)
        {
            _assetSerializedProperty = property.FindPropertyRelative(nameof(SceneReference.asset));
            _guidSerializedProperty = property.FindPropertyRelative(nameof(SceneReference.guid));

            _asset = _assetSerializedProperty.objectReferenceValue;
            _guid = _guidSerializedProperty.stringValue;
            _path = AssetDatabase.GetAssetPath(_asset);
            _sceneInBuildSettings = EditorBuildSettings.scenes.FirstOrDefault(x => x.guid.ToString() == _guid);

#if ESR_ADDRESSABLES
            _addressableEntry = AddressableAssetSettingsDefaultObject.Settings.FindAssetEntry(_guid);
#endif // ESR_ADDRESSABLES

            _optionsAttribute = fieldInfo.GetCustomAttribute<SceneReferenceOptionsAttribute>(false) ?? DefaultOptionsAttribute;

            if (_asset == null)
            {
                _sceneBuildSettingsState = SceneBuildSettingsState.None;
            }
#if ESR_ADDRESSABLES
            else if (_addressableEntry != null)
            {
                _sceneBuildSettingsState = SceneBuildSettingsState.Addressable;
            }
#endif // ESR_ADDRESSABLES
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
            var toolboxButtonWidth = EditorGUIUtility.singleLineHeight;
            var selectorFieldRect = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            selectorFieldRect.height = EditorGUIUtility.singleLineHeight;

            if (IsToolboxEnabled)
            {
                selectorFieldRect.width -= toolboxButtonWidth + 2;
            }

            var newAsset = EditorGUI.ObjectField(selectorFieldRect, _asset, typeof(SceneAsset), false);
            SetWith(newAsset);

            if (IsToolboxEnabled)
            {
                var toolboxButtonRect = new Rect
                {
                    x = selectorFieldRect.x + selectorFieldRect.width + 2,
                    width = toolboxButtonWidth,
                    y = selectorFieldRect.y + 1,
                    height = selectorFieldRect.height - 1
                };

                // TODO: we should ship our own icon to prevent this breaking in the future
                var settingsIcon = EditorGUIUtility.IconContent("SettingsIcon");
                var toolboxButton = GUI.Button(toolboxButtonRect, settingsIcon, EditorStyles.iconButton);
                if (toolboxButton)
                {
                    var toolboxPopupWindow = CreateToolboxPopupWindow();
                    PopupWindow.Show(toolboxButtonRect, toolboxPopupWindow);
                }
            }

            GUI.color = colorToRestore;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        private ToolboxPopupWindow CreateToolboxPopupWindow()
        {
            var tools = new List<ITool>();

            if (_sceneBuildSettingsState == SceneBuildSettingsState.NotIncluded)
            {
                tools.Add(new AddToBuildTool(_path, _asset));
            }

            if (_sceneBuildSettingsState == SceneBuildSettingsState.Disabled)
            {
                tools.Add(new EnableInBuildTool(_path, _guid, _asset));
            }

#if ESR_ADDRESSABLES
            if(_sceneBuildSettingsState == SceneBuildSettingsState.NotIncluded || _sceneBuildSettingsState == SceneBuildSettingsState.Disabled)
            {
                tools.Add(new MakeAddressableTool(_path, _guid, _asset));
            }
#endif // ESR_ADDRESSABLES

            return new ToolboxPopupWindow(tools);
        }
    }
}
