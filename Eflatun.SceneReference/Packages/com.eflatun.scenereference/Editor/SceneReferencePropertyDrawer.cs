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

        private enum SceneBundlingState
        {
            NoScene = 0,
            Nowhere = 1,
            InBuildDisabled = 2,
            InBuildEnabled = 3,
            Addressable = 4,
        }

        private SerializedProperty _assetSerializedProperty;
        private SerializedProperty _guidSerializedProperty;

        private UnityEngine.Object _asset;
        private string _guid;
        private string _path;
        private EditorBuildSettingsScene _buildEntry;
        private SceneBundlingState _bundlingState;
        private SceneReferenceOptionsAttribute _optionsAttribute;

#if ESR_ADDRESSABLES
        private AddressableAssetEntry _addressableEntry;
#endif // ESR_ADDRESSABLES

        private bool IsColoringIgnored => SettingsManager.UtilityIgnores.IsIgnoredForColoring(_path, _guid);
        private bool IsToolboxIgnored => SettingsManager.UtilityIgnores.IsIgnoredForToolbox(_path, _guid);

        private bool ShouldColorSceneInBuild => !IsColoringIgnored && _optionsAttribute.SceneInBuildColoring switch
        {
            ColoringBehaviour.Enabled => true,
            ColoringBehaviour.Disabled => false,
            ColoringBehaviour.DoNotOverride => SettingsManager.PropertyDrawer.ColorBasedOnSceneInBuildState.value,
            _ => throw new ArgumentOutOfRangeException(nameof(_optionsAttribute.SceneInBuildColoring), _optionsAttribute.SceneInBuildColoring, null)
        };

        private bool ShouldColorAddressable => !IsColoringIgnored && _optionsAttribute.AddressableColoring switch
        {
            ColoringBehaviour.Enabled => true,
            ColoringBehaviour.Disabled => false,
            ColoringBehaviour.DoNotOverride => SettingsManager.AddressablesSupport.ColorAddressableScenes.value,
            _ => throw new ArgumentOutOfRangeException(nameof(_optionsAttribute.AddressableColoring), _optionsAttribute.AddressableColoring, null)
        };

        private bool ShouldShowToolbox => !IsToolboxIgnored && _optionsAttribute.Toolbox switch
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
            _buildEntry = EditorBuildSettings.scenes.FirstOrDefault(x => x.guid.ToString() == _guid);

#if ESR_ADDRESSABLES
            if (AddressableAssetSettingsDefaultObject.SettingsExists)
            {
                _addressableEntry = AddressableAssetSettingsDefaultObject.Settings.FindAssetEntry(_guid);
            }
            else
            {
                _addressableEntry = null;
            }
#endif // ESR_ADDRESSABLES

            _optionsAttribute = fieldInfo.GetCustomAttribute<SceneReferenceOptionsAttribute>(false) ?? DefaultOptionsAttribute;

            if (_asset == null)
            {
                _bundlingState = SceneBundlingState.NoScene;
            }
#if ESR_ADDRESSABLES
            else if (_addressableEntry != null)
            {
                _bundlingState = SceneBundlingState.Addressable;
            }
#endif // ESR_ADDRESSABLES
            else if (_buildEntry == null)
            {
                _bundlingState = SceneBundlingState.Nowhere;
            }
            else if (!_buildEntry.enabled)
            {
                _bundlingState = SceneBundlingState.InBuildDisabled;
            }
            else
            {
                _bundlingState = SceneBundlingState.InBuildEnabled;
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
            GUI.color = _bundlingState switch
            {
                SceneBundlingState.Nowhere when ShouldColorSceneInBuild => Color.red,
                SceneBundlingState.InBuildDisabled when ShouldColorSceneInBuild => Color.yellow,
                SceneBundlingState.Addressable when ShouldColorAddressable => Color.cyan,
                _ => colorToRestore
            };

            // draw scene asset selector
            var toolboxButtonWidth = EditorGUIUtility.singleLineHeight;
            var selectorFieldRect = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            selectorFieldRect.height = EditorGUIUtility.singleLineHeight;

            if (ShouldShowToolbox)
            {
                selectorFieldRect.width -= toolboxButtonWidth + 2;
            }

            var newAsset = EditorGUI.ObjectField(selectorFieldRect, _asset, typeof(SceneAsset), false);
            SetWith(newAsset);

            if (ShouldShowToolbox)
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

// Backwards compatibility (https://github.com/starikcetin/Eflatun.SceneReference/issues/74)
#if UNITY_2022_1_OR_NEWER
                var toolboxButtonStyle = EditorStyles.iconButton;
#else
                var toolboxButtonStyle = EditorStyles.miniButton;
                toolboxButtonStyle.padding = new RectOffset(1, 1, 1, 1);
#endif

                var toolboxButton = GUI.Button(toolboxButtonRect, settingsIcon, toolboxButtonStyle);
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

            if (_bundlingState == SceneBundlingState.Nowhere)
            {
                tools.Add(new AddToBuildTool(_path, _asset));
            }

            if (_bundlingState == SceneBundlingState.InBuildDisabled)
            {
                tools.Add(new EnableInBuildTool(_path, _guid, _asset));
            }

#if ESR_ADDRESSABLES
            if (_bundlingState == SceneBundlingState.Nowhere || _bundlingState == SceneBundlingState.InBuildDisabled)
            {
                tools.Add(new MakeAddressableTool(_path, _guid, _asset));
            }
#endif // ESR_ADDRESSABLES

            return new ToolboxPopupWindow(tools);
        }
    }
}
