# Eflatun.SceneReference Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/), and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).



## [Unreleased]

### Breaking Changes

### Added

### Changed

### Removed

### Fixed



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
