using System;
using JetBrains.Annotations;

namespace Eflatun.SceneReference.Editor
{
    /// <summary>
    /// Identifiers for places that perform scene GUID to path map generation generation.
    /// </summary>
    [PublicAPI]
    [Flags]
    public enum SceneGuidToPathMapGenerationTriggers
    {
        None = 0,
        All = ~0,
        
        AfterSceneAssetChange = 1 << 0,
        BeforeEnterPlayMode = 1 << 1,
        BeforeBuild = 1 << 2,
    }
}
