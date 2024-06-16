# Eflatun.SceneReference Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/), and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).



## [Unreleased]

### Breaking Changes

### Added

### Changed

### Removed

### Fixed



## [4.1.1] - 2024-06-17

### Fixed
- Fixed mistakes in README.



## [4.1.0] - 2024-06-16

### Added
- `SceneReferenceUnityEventAdapter` class: A utility `MonoBehaviour` that allows using statically provided `SceneReference`s as parameters to a `UnityEvent`.
- Utility Ignores: A set of settings that allow ignoring certain scenes from having the inline utilities.



## [4.0.0] - 2024-01-23
There are fundamental changes to the editor-time behaviour in this release. Please examine carefully before upgrading.

### Breaking Changes
- From now on, map files are only generated during a build, and removed right after. In editor-time, maps are instead stored in and fetched from User Settings. You can remove the generated files and folders from your project, and remove the corresponding lines from your source control's ignore settings.
- `SceneDataMapsGenerator.Run` now takes a booelan argument that controls whether to ouput files or not.
- Minimum compatible Unity version is set to `2020.3.48f1`.

### Changed
- Default value of `EditorLogLevel` setting is now `Warning`, it was previously `Debug`.
- Default value of `JsonFormatting` setting is now `None`, it was previously `Indented`.

### Fixed
- Prevent null ref exceptions during runtime caused by uninitialized map providers when the maps are missing.



## [3.2.1] - 2023-12-27

### Fixed
- Prevent `Scene GUID to path map file not found!` errors and null reference exceptions in `SceneAssetPostprocessor` if the scene GUID to path map file is missing.
- Prevent null reference exceptions at project launch due to addressables settings not being loaded yet.
- Ensure map files are always generated at editor startup, even if they are empty.



## [3.2.0] - 2023-11-20

### Added
- `SceneReference.UnsafeReason` property: Provides the reasoning behind why a `SceneReference` was deemed unsafe.
- `SceneReferenceUnsafeReason` enum: Possible reasons for a `SceneReference` to be deemed unsafe.

### Fixed
- `SceneDataMapsGenerator` no longer runs on scene saves.



## [3.1.3] - 2023-11-12

### Fixed
- Prevent null reference errors when addressables package is installed, but addressables settings are not initialized.



## [3.1.2] - 2023-11-12

### Fixed
- Backwards compatibility: Use `EditorStyles.miniButton` instead of non-existent `EditorStyles.iconButton` for Unity versions earlier than 2022.1.



## [3.1.1] - 2023-07-22

### Fixed
- Implemented workaround for a Unity bug that caused our settings page to throw exceptions and not display in Unity `2022.3.4f1`.



## [3.1.0] - 2023-07-08

### Added
- Log level of the editor logger can now be controlled with the new setting `Logging/Editor Log Level`.

### Removed
- Removed the suggestion to output generated files as indented for source control reasons. This is because we are suggesting to ignore the generated files.



## [3.0.0] - 2023-07-07

This release introduces support for addressable scenes.

There are breaking changes to settings. Please visit the settings page and re-apply all your settings as soon as you update.

### Breaking Changes
- `SceneGuidToPathMapGenerator` is renamed to `SceneDataMapsGenerator`.
- `SceneGuidToPathMapGenerationTriggers` is renamed to `SceneDataMapsGeneratorTriggers`.
- Settings changes:
  - Scene GUID to Path Map (new name Scene Data Maps) category:
    - The category is renamed from `SceneGuidToPathMap` to `SceneDataMaps`.
    - Key of the `GenerationTriggers` setting is changed from `SceneGuidToPathMap.GenerationTriggers` to `SceneDataMaps.GenerationTriggers`.
    - Key of the `JsonFormatting` setting is changed from `SceneGuidToPathMap.JsonFormatting` to `SceneDataMaps.JsonFormatting`.
    - Key of the `FailBuildIfGenerationFails` setting is changed from `SceneGuidToPathMap.FailBuildIfGenerationFails` to `SceneDataMaps.FailBuildIfGenerationFails`.
  - Property Drawer category:
    - `ShowInlineSceneInBuildUtility` setting is renamed to `ShowInlineToolbox`. The key is also changed from `PropertyDrawer.ShowInlineSceneInBuildUtility` to `PropertyDrawer.ShowInlineToolbox`.
- All exceptions are moved from the `Eflatun.SceneReference` namespace to the new `Eflatun.SceneReference.Exceptions` namespace.
- Following valiation properties are removed from `SceneReference`. They are instead replaced with a more informative `State` property. See the _Added_ section.
  - `IsInSceneGuidToPathMap`: removed.
  - `IsInBuildAndEnabled`: removed.
  - `IsSafeToUse`: removed.
  - `HasValue`: no longer public.
- `SceneReferenceOptionsAttribute` changes:
  - `Coloring` field is renamed to `SceneInBuildColoring`. It still controls the same cases of coloring, but they are no longer all the coloring options. See the _Changes_ section.
  - `UtilityLine` field is removed. Its replacement is the `Toolbox` field. See the _Added_ section.

### Added
- `SceneDataMapsGenerator` now also generates a scene GUID to address map. The map will be empty if addressables support is disabled.
- New map generation triggers:
  - `AfterPackagesResolve`: Triggers after packages are resolved.
  - `AfterAddressablesChange`: Triggers after addressable groups change.
- Property drawer can optionally color addressables scenes differently to draw attention to them.
- New inline utilties:
  - `Make Addressable`: Makes the scene addressable.
- New settings:
  - Addressables Support (`AddressablesSupport`) category:
    - `ColorAddressableScenes` setting: If enabled, scene references that have an addressable scene will be colored differently.
    - An info box that displays the current addressables support status.
- New exceptions:
  - `AddressNotFoundException`
  - `AddressNotUniqueException`
  - `AddressablesSupportDisabledException`
  - `SceneNotAddressableException`
- `SceneGuidToAddressMapProvider` class: Provides a map of scene GUIDs to their address. Very similar to `SceneGuidToPathMapProvider`, with the exception that it cannot provide an inverse map. This is because the address of an asset is not guaranteed to be unique. Instead, it provides `GetGuidFromAddress` and `TryGetGuidFromAddress` methods.
- `SceneReference.FromAddress` factory method: Creates a `SceneReference` from the given address.
- `SceneReference.Address` property.
- `SceneReference.State` property: This property replaces all previously exposed validation methods. It returns a `SceneReferenceState` enum, which describes the state of the `SceneReference` in terms of usage.
- `SceneReferenceState` enum: Describes the state of the `SceneReference` in terms of usage.
  - `Unsafe`: The `SceneReference` is not safe to use.
  - `Regular`: The `SceneReference` is safe to use, and it references a regular scene.
  - `Addressable`: The `SceneReference` is safe to use, and it references an addressable scene. This state is only possible if addressables support is enabled.
- `SceneReferenceOptionsAttribute` new fields:
  - `Toolbox`: Controls the visibility of the toolbox button. It replaces the now deleted `UtilityLine` field.
  - `AddressableColoring`: Controls the coloring behaviour of the addressable scenes.
- `ToolboxBehaviour` enum. Replaces the now deleted `UtilityLineBehaviour` enum with the same semantics.

### Changed
- The concept of Utility Line is replaced with the concept of Toolbox. In summary, instead of drawing buttons as a second line below the field, we instead draw a small button to the end of the field on the same line. When clicked, a toolbox popup appears with all the utilities.
- `SceneInBuildColoring` argument (previously named `Coloring`) of `SceneReferenceOptionsAttribute` still controls the same types of coloring cases, but since they used to be all the cases, the field was implicitly controlling the entire coloring behaviour. While its semantics are not changed, since there are now other coloring cases, it is no longer the only field that controls the entire coloring behaviour.



## [2.1.0] - 2023-03-01

### Added
- `Open Build Settings` button to the scene-in-build utility popups. This button will ping the scene in the project view and open build settings, allowing you to fix your build settings manually.

### Changed
- `Add to Build as Enabled` button is renamed to `Add to Build` in scene-in-build utility popups of scenes that are not in build.

### Removed
- `Add to Build as Disabled` button in the scene-in-build utility popups of scenes that are not in build. This button was not making much sense because adding to build as disabled doesn't actually fix the situation, since the scene will still be disabled afterwards.



## [2.0.0] - 2023-02-26
We renamed some of our internal serialized fields. Since we utilize `FormerlySerializedAs`, you will not lose any data. However, due to these changes, Unity will re-serialize your `SceneReference`s as you save your scenes and prefabs. Please commit these re-serialization changes as you see fit, otherwise they will keep appearing until you do so.

### Breaking Changes
- Constructors and factory methods of `SceneReference` now validate their arguments and throw exceptions of type `SceneReferenceCreationException` if they are invalid. Note that the default constructor always creates an empty instance, but it never throws.
- Changed the argument name of the constructor `SceneReference(string sceneAssetGuidHex)` to `SceneReference(string guid)`.
- Changed the name of the property `SceneReference.AssetGuidHex` to `SceneReference.Guid`.
- Changed the argument name of the factory method `SceneReference.FromScenePath(string scenePath)` to `SceneReference.FromScenePath(string path)`.

### Added
- `SceneReference` now supports custom XML serialization via `System.Xml`.
- `SceneGuidToPathMapProvider` now also provides a path to GUID map, which is inversely equivalent to the already existing GUID to path map.
- `SceneReferenceCreationException`: Thrown when something goes wrong during the creation of a `SceneReference`.

### Changed
- `SceneReference` now implements serialization interfaces explicitly. This means serialization implementations are no longer exposed as `public`.
- `SceneReference` serialization implementations are now `virtual`. This means child classes can override custom serialization behaviours.
- Internal serialized field name changes:
	- `SceneReference.sceneAsset` to `SceneReference.asset`
	- `SceneReference.sceneAssetGuidHex` to `SceneReference.guid`
- Menu item `Tools/Eflatun/Scene Reference/Run Scene GUID to Path Map Generator` is renamed to `Tools/Eflatun/Scene Reference/Generate Scene Data Maps`.

### Fixed
- Prevent empty scene GUID hex in Unity serialized `SceneReference` instances.
- `SceneReference` default constructor now initailizes with an all-zero GUID as intended.
- `SceneReference` custom serialization implementations now guard against null or whitespace GUIDs.
- Prevent Unity from throwing `InvalidOperationException: Stack empty.` after inline scene-in-build utility pop-up.
- Internal bug fixes.



## [1.5.0] - 2023-02-23

### Added
- `SceneReference` now exposes public constructors and a factory method for allowing instance creation in code:
	- Empty: `new SceneReference()` constructor
	- From GUID: `new SceneReference(string sceneAssetGuidHex)` constructor
	- (Editor-only) From asset: `new SceneReference(UnityEngine.Object sceneAsset)` constructor
	- From path: `SceneReference.FromScenePath(string scenePath)` factory method

### Fixed
- Add `UNITY_EDITOR` condition to `UnityEditor` namespace import in `SceneReference`.



## [1.4.1] - 2023-02-20

### Fixed
- Added safeguard against situations where the scene GUID hex might be uninitialized.



## [1.4.0] - 2023-02-07

### Added
- Support for custom serialization: `SceneReference` now implements the `ISerializable` interface and the corresponding deserialization constructor.



## [1.3.0] - 2022-09-05

### Added
- Descriptive custom exceptions:
  - `SceneReferenceException`: The root class of all custom exceptions of this package.
  - `EmptySceneReferenceException`
  - `InvalidSceneReferenceException`
- Validation properties in `SceneReference`:
  - `IsSafeToUse`: Checks everything. Recommended for most use cases.
  - `HasValue`
  - `IsInSceneGuidToPathMap`
  - `IsInBuildAndEnabled`

### Changed
- Now throwing more descriptive custom exceptions (`EmptySceneReferenceException` and `InvalidSceneReferenceException`) for edge cases.

### Fixed
- Use plaintext log prefixes (without styling) if outside Unity Editor.



## [1.2.2] - 2022-08-16

### Changed
- Improve performance of `[SceneReferenceOptions]` attribute lookup in the property drawer.

### Fixed
- `[SceneReferenceOptions]` attribute not being recognized when the field is nested in other serializables.



## [1.2.1] - 2022-08-15
It is now recommended to ignore the generated map file in version control. See the new [Ignore Auto-Generated Map File in Version Control section in README.md](README.md#ignore-auto-generated-map-file-in-version-control).

### Fixed
- Generator also generates a `.keep` file in the the auto-generated map folder to make sure the folder is always tracked in version control.



## [1.2.0] - 2022-08-13

### Added
- `[SceneReferenceOptions]` attribute: Provides the ability to override scene-in-build validation settings on a per-field basis.

### Changed
- Moved the whole `Eflatun/Scene Reference` menu to under `Tools`. The new resulting menu path is `Tools/Eflatun/Scene Reference`.



## [1.1.0] - 2022-08-07

### Added
- Validation & inline fix utility for scenes that are either not in build or disabled in build.
- New settings entries for scene in build validation & inline fix utilities.



## [1.0.0] - 2022-08-03

Initial release.
