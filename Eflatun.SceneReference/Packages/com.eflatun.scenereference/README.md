<h1 align="center">Eflatun.SceneReference</h1>
<br>

<p align="center">
    <img src=".assets/logo.png" width="250" height="250" alt="logo" />
</p>
<br>

<p align="center">
    Scene References for Runtime and Editor.
</p>

<p align="center">
    Strongly typed, robust, and reliable.
</p>

<p align="center">
    Provides Asset GUID, Scene Path, Build Index, and Scene Name.
</p>
<br>

<p align="center">
  <a href="https://github.com/starikcetin/Eflatun.SceneReference/"><img src="https://img.shields.io/static/v1?label=GitHub&logo=github&message=Eflatun.SceneReference&color=blueviolet" alt="GitHub badge"/></a>
  &nbsp;
  <a href="https://openupm.com/packages/com.eflatun.scenereference/"><img src="https://img.shields.io/npm/v/com.eflatun.scenereference?label=OpenUPM&logo=unity&registry_uri=https://package.openupm.com" alt="OpenUPM badge"/></a>
</p>
<br>

# Installation

## Get `Eflatun.SceneReference`

### With OpenUPM (recommended)

1. Install `openupm-cli` via `npm`. You can skip this step if already have `openupm-cli` installed.

```console
npm install -g openupm-cli
```

2. Install `com.eflatun.scenereference` in your project. Make sure to run this command at the root of your Unity project.

```console
openupm add com.eflatun.scenereference
```

### With Git URL

Add the following line to the `dependencies` section of your project's `manifest.json` file. Replace `1.3.0` with the version you want to install.

```json
"com.eflatun.scenereference": "git+https://github.com/starikcetin/Eflatun.SceneReference.git#1.3.0"
```

_Although it is highly discouraged, you can replace `1.3.0` with `upm` to get the latest version instead of a specific one._

## Ignore Auto-Generated Map File in Version Control

_You can skip this section if you are not using version control in your project._

It is generally a recommended practice to ignore auto-generated files in version control. `Eflatun.SceneReference` auto-generates a JSON file with the path `Assets/Resources/Eflatun/SceneReference/SceneGuidToPathMap.generated.json`. We recommend you ignore this file and its corresponding `.meta` file in your version control.

If you are using Git, you can do so by adding the following lines to your `.gitignore` file:

```gitignore
# Eflatun.SceneReference auto-generated map file
**/[Aa]ssets/Resources/Eflatun/SceneReference/SceneGuidToPathMap.generated.json
**/[Aa]ssets/Resources/Eflatun/SceneReference/SceneGuidToPathMap.generated.json.meta
```

# Usage

1. Define your `SceneReference` serialized field:

```cs
// Import Runtime namespace
using Eflatun.SceneReference;

// You can define it by itself
[SerializeField] private SceneReference mySceneReference;

// Or in a collection
[SerializeField] private List<SceneReference> mySceneReferences;
```

2. Assign your scene to your `SceneReference` field in the inspector:

![.assets/inspector.png](.assets/inspector.png)

3. Use it!

```cs
// Import Runtime namespace
using Eflatun.SceneReference;

// You can access these anytime, anywhere
var sceneGuid = mySceneReference.AssetGuidHex;
var scenePath = mySceneReference.ScenePath;
var sceneBuildIndex = mySceneReference.BuildIndex;
var sceneName = mySceneReference.Name;

// You can only access these when the scene is currently loaded
var loadedScene = mySceneReference.LoadedScene
```

## Validation

You can check `IsSafeToUse` property to make sure a `SceneReference` is completely valid before using it.

```cs
// Import Runtime namespace
using Eflatun.SceneReference;

// Validate
if(mySceneReference.IsSafeToUse)
{
    // Safe to use!
}
else 
{
    // Something is wrong.
}
```

## Inline Scene-In-Build Validation & Fix Utility

Unity only includes in a build the scenes that are added and enabled in build settings. `Eflatun.SceneReference` on the other hand, allows you to assign on to it any scene you wish. This behaviour may cause runtime bugs when loading scenes. To prevent these potential bugs, `Eflatun.SceneReference` provides inline validation and fix utilities.

In this example:

![.assets/validation_inspector.png](.assets/validation_inspector.png)

- `Another Scene` field is assigned a scene that is disabled in build settings.
- `Yet Another Scene` field is assigned a scene that is not included in build settings.
- Similarly for the `Scene Reference List` property.

Clicking on the `Enable in Build...` button gives us this prompt, which enables us to quickly fix the situation:

![.assets/validation_enable_prompt.png](.assets/validation_enable_prompt.png)

Similarly, `Add to Build...` button gives the following prompt:

![.assets/validation_add_prompt.png](.assets/validation_add_prompt.png)

Using these prompts, we can quickly alleviate the situation, and prevent potential runtime bugs when loading these scenes.

# Settings

`Eflatun.SceneReference` provides settings under the `Project Settings`.

Open the project settings via `Edit/Project Settings...` menu item.

Look for the `Eflatun` category in the left panel. Select the `Scene Reference` item.

![.assets/settings.png](.assets/settings.png)

## Property Drawer

### Show Inline Scene-In-Build Utility

Should we show the inline utility that allows you to quickly fix scenes that are either not in build or disabled in build?

Unity only bundles scenes that are added and enabled in build settings. Therefore, you would want to make sure the scene you assign to a
SceneReference is added and enabled in build settings.

It is recommended to leave this option at 'true', as the inline utility saves you a lot of time.

### Color Based On Scene-In-Build State

Should we color the property to draw attention for scenes that are either not in build or disabled in build?

Unity only bundles scenes that are added and enabled in build settings. Therefore, you would want to validate whether the scene you assign to a SceneReference is added and enabled in build settings.

It is recommended to leave this option at 'true', as it will help you identify many potential runtime errors.

## Scene GUID To Path Map

### Generation Triggers

Controls when the Scene GUID to Path Map gets regenerated.

- After Scene Asset Change: Regenerate the map every time a scene asset changes (delete, create, move, rename).

- Before Enter Play Mode: Regenerate the map before entering play mode in the editor.

- Before Build: Regenerate the map before a build.

It is recommended that you leave this option at _All_ unless you are debugging something. Failure to generate the map when needed can result in broken scene references in runtime.

### JSON Formatting

Controls the Scene GUID to Path Map Generator's JSON formatting.

It is recommended to leave this option at _Indented_, as it will help with version control and make the generated file human-readable.

### Fail Build If Generation Fails

Should we fail a build if scene GUID to path map generation fails?

Only relevant if _Before Build_ generation trigger is enabled.

It is recommended to leave this option at _true_, as a failed map generation can result in broken scene references in runtime.

# Advanced Usage

## Generated File

`Eflatun.SceneReference` uses a JSON generator in editor-time to produce a `Scene GUID -> Scene Path` map. You can find the file at this location: `Assets/Resources/Eflatun/SceneReference/SceneGuidToPathMap.generated.json`.

**This file is auto-generated, do not edit it. Any edits will be lost at the next generation.**

## Running the Generator Manually

The generator runs automatically according to the triggers selected in the settings. However, if for some reason you need to run the generator yourself, you can do so. 

Running the generator has no side-effects.

### Via Menu Item

You can trigger the generator via a menu item. Find it under `Tools/Eflatun/Scene Reference/Run Scene GUID to Path Map Generator`:

![.assets/generator_menu.png](.assets/generator_menu.png)

### In Editor Code

You can trigger the generator from your editor code:

```cs
// Import Editor namespace
using Eflatun.SceneReference.Editor;

// Run the generator. Only do this in Editor code!
SceneGuidToPathMapGenerator.Run();
```

## Accessing Settings in Editor Code

You can read and manipulate `Eflatun.SceneReference` settings from your editor code.

**Changing the settings from code may have unintended consequences. Make sure you now what you are doing.**

```cs
// Import the Editor namespace
using Eflatun.SceneReference.Editor;

// Access a setting. Only do this in Editor code!
var generationTriggers = SettingsManager.SceneGuidToPathMap.GenerationTriggers;

// Change a setting. Only do this in Editor code!
SettingsManager.SceneGuidToPathMap.GenerationTriggers = GenerationTriggers.All;
```

## Accessing the Scene Guid to Path Map Directly

The `SceneGuidToPathMapProvider` static class is responsible for providing the scene GUID to scene path mapping to the rest of the code. You have the option of accessing it directly both in runtime and editor code:

```cs
// Import the Runtime namespace
using Eflatun.SceneReference;

// Get the scene path from a scene GUID. You can do this both in runtime and in editor.
var scenePath = SceneGuidToPathMapProvider.SceneGuidToPathMap[sceneGuid];
```

There are no side-effects of accessing the map directly.

In runtime, there are no performance penalties. The generated file is parsed automatically either upon the first access to `SceneGuidToPathMapProvider.SceneGuidToPathMap` or during `RuntimeInitializeLoadType.BeforeSceneLoad`, whichever comes first. It is guaranteed that the map is parsed only once.

In editor, there are also no performance penalties except for one case. The generator assigns the map directly to the provider upon every generation. This prevents unnecessarily parsing the map file. However, if the provider loses the value assigned by the generator due to Unity [reloading the domain](https://docs.unity3d.com/Manual/DomainReloading.html), and some code tries to access the map before the generator runs again, then the provider has to parse the map file itself. This is what happens in that scenario:

1. Generator runs and directly assigns the map to the provider.
2. Something happens which triggers Unity to [reload the domain](https://docs.unity3d.com/Manual/DomainReloading.html).
3. You access `SceneGuidToPathMapProvider.SceneGuidToPathMap`.
4. Provider checks to see if it still has the map values, and realizes they are lost.
5. Provider parses the map file.

## Overriding Inline Scene-In-Build Validation Settings Per Field

You can override the behaviour of the scene-in-build validation project settings on a per-field basis using the `[SceneReferenceOptions]` attribute. For example, in order to disable both the coloring and the utility line, use the attribute as such:

```cs
[SceneReferenceOptions(Coloring = ColoringBehaviour.Disabled, UtilityLine = UtilityLineBehaviour.Disabled)]
[SerializeField] private SceneReference scene;
```

For both `Coloring` and `UtlityLine`, passing `Enabled` or `Disabled` will force that behaviour to be enabled or disabled respectively, disregarding the project settings. `DoNotOverride` makes the field respect the project settings. `DoNotOverride` is the default value. 

You don't have to supply both fields at once. Missing fields will have the default value, which is `DoNotOverride`. For example, the following code disables the utility line, but makes coloring respect project settings:

```cs
[SceneReferenceOptions(UtilityLine = UtilityLineBehaviour.Disabled)]
[SerializeField] private SceneReference scene;
```

## Partial Validation

If you need to perform validation partially (step-by-step), then you can use the partial validation properties. Keep in mind that the use cases that require partial validation are rare and few.

1. `HasValue`: Is this `SceneReference` assigned something?
2. `IsInSceneGuidToPathMap`: Does the Scene GUID to Path Map contain the scene?
3. `IsInBuildAndEnabled`: Is the scene added and enabled in Build Settings?

These properties can throw exceptions. So the order in which you check them is important. This is the recommended order to avoid exceptions:

```cs
// Import Runtime namespace
using Eflatun.SceneReference;

// Avoid EmptySceneReferenceException.
if(mySceneReference.HasValue)
{
    // Avoid InvalidSceneReferenceException.
    if(mySceneReference.IsInSceneGuidToPathMap)
    {
        // Avoid SceneManagement-related problems.
        if(mySceneReference.IsInBuildAndEnabled)
        {
            // Completely validated. Safe to use.
        }
        else
        {
            // The scene is not added or is disabled in Build Settings.
        }
    }
    else
    {
        // One of these things:
        // 1. The Scene GUID to Path Map is outdated.
        // 2. The scene is invalid.
        // 3. The assigned asset is not a scene.
    }
}
else
{
    // The SceneReference is empty (not assigned anything).
}
```

If you only need to check if it is completely safe to use a `SceneReference` without knowing where exactly the problem is, then only check `IsSafeToUse` instead. Checking only `IsSafeToUse` is sufficient for the majority of the use cases.

Checking `IsSafeToUse` is equivalent to checking all partial validation properties in the correct order, but it provides a slightly better performance.

# Exceptions

## `EmptySceneReferenceException`

Thrown if a `SceneReference` is empty (not assigned anything).

To fix it, make sure the `SceneReference` is assigned a valid scene asset.

You can avoid it by checking `IsSafeToUse` (recommended) or `HasValue`.

## `InvalidSceneReferenceException`

Thrown if a `SceneReference` is invalid. This can happen for these reasons:

1. The `SceneReference` is assigned an invalid scene, or the assigned asset is not a scene. To fix this, make sure the `SceneReference` is assigned a valid scene asset.

2. The Scene GUID to Path Map is outdated. To fix this, you can either manually run the map generator, or enable all generation triggers. It is highly recommended to keep all the generation triggers enabled.

You can avoid it by checking `IsSafeToUse` (recommended) or `IsInSceneGuidToPathMap`.

## `SceneReferenceInternalException`

This exception is not part of the public API. It indicates that something has gone wrong internally. It is not meant to be catched, fixed, or avoided by user code.

If you come across this exception, make sure to create a bug report by [opening an issue](https://github.com/starikcetin/Eflatun.SceneReference/issues) and including the relevant information in the exception message.

# Acknowledgements

* This project is inspired by [JohannesMP's SceneReference](https://github.com/JohannesMP/unity-scene-reference). For many years I have used his original implementation of a runtime Scene Reference. Many thanks to [@JohannesMP](https://github.com/JohannesMP) for saving me countless hours of debugging, and inspiring me to come up with a more robust way to tackle this problem that Unity refuses to solve.

* README header inspired by [Angular's README](https://github.com/angular/angular/blob/main/README.md).

# Similar Projects
If this project doesn't suit your needs, you can always let me know by [opening an issue](https://github.com/starikcetin/Eflatun.SceneReference/issues) or [creating a discussion](https://github.com/starikcetin/Eflatun.SceneReference/discussions) and I will see what we can do about it. If you think you absolutely need another approach, here are some similar projects to check out:

* https://github.com/JohannesMP/unity-scene-reference
* https://github.com/NibbleByte/UnitySceneReference

# License

MIT License. Refer to the [LICENSE.md](LICENSE.md) file.

Copyright (c) 2022 [S. Tarık Çetin](https://github.com/starikcetin)
