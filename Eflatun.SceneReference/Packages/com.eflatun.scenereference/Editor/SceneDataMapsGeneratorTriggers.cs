using System;
using JetBrains.Annotations;

namespace Eflatun.SceneReference.Editor
{
    // IMPORTANT: Keep SceneDataMapsGeneratorTriggers and InternalSceneDataMapsGeneratorTriggers in sync at all times.
    
    /// <summary>
    /// Identifiers for places that trigger <see cref="SceneDataMapsGenerator"/>.
    /// </summary>
    [PublicAPI]
    [Flags]
    public enum SceneDataMapsGeneratorTriggers
    {
        None = 0,
        All = ~0,

        AfterSceneAssetChange = 1 << 0,
        BeforeEnterPlayMode = 1 << 1,
        BeforeBuild = 1 << 2,
        AfterPackagesResolve = 1 << 3,
        AfterAddressablesChange = 1 << 4,
    }
    
    /// <summary>
    /// Same as <see cref="SceneDataMapsGeneratorTriggers"/>, but without the <c>All</c> and <c>None</c> values.
    /// </summary>
    [Flags]
    internal enum InternalSceneDataMapsGeneratorTriggers
    {
        AfterSceneAssetChange = 1 << 0,
        BeforeEnterPlayMode = 1 << 1,
        BeforeBuild = 1 << 2,
        AfterPackagesResolve = 1 << 3,
        AfterAddressablesChange = 1 << 4,
    }
}
