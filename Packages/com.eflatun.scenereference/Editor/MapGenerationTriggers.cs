using System;

namespace Eflatun.SceneReference.Editor
{
    /// <summary>
    /// Identifiers for places that perform map generation.
    /// </summary>
    [Flags]
    public enum MapGenerationTriggers
    {
        None = 0,
        All = ~None,
        
        AfterSceneAssetChange = 1 << 0,
        BeforeEnterPlayMode = 1 << 1,
        BeforeBuild = 1 << 2,
    }
}
