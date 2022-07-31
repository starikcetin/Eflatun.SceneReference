using System.IO;
using UnityEngine;

namespace Eflatun.SceneReference
{
    public static class Paths
    {
        private static readonly PathConverter AssetsFolder = new(Application.dataPath);
        private static readonly PathConverter ResourcesFolder = new(Path.Combine(AssetsFolder.GivenPath, "Resources"));
        
        public static class RelativeToResources
        {
            public static readonly PathConverter GenFolder = new(Path.Combine("Eflatun", "SceneReference"));
            public static readonly PathConverter GenFile = new(Path.Combine(GenFolder.GivenPath, "map.generated.json"));
        }
        
        public static class RelativeToAssets
        {
            public static readonly PathConverter GenFolder = new(Path.Combine(ResourcesFolder.GivenPath, RelativeToResources.GenFolder.GivenPath));
            public static readonly PathConverter GenFile = new(Path.Combine(ResourcesFolder.GivenPath, RelativeToResources.GenFile.GivenPath));
        }
    }
}
