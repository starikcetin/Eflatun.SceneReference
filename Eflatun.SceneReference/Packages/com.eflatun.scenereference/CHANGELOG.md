# Eflatun.SceneReference Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/), and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).



## [Unreleased]

### Added

### Changed

### Removed

### Fixed



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
