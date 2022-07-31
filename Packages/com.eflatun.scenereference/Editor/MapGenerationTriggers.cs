using System;

namespace Eflatun.SceneReference.Editor
{
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
