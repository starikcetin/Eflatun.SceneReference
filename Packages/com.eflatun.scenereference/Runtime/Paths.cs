using System.IO;
using Eflatun.SceneReference.Utility;
using UnityEngine;

namespace Eflatun.SceneReference
{
    internal static class Paths
    {
        private static readonly ConvertedPath AssetsFolder = new(Application.dataPath);
        private static readonly ConvertedPath ResourcesFolder = new(Path.Combine(AssetsFolder.GivenPath, "Resources"));
        
        internal static class RelativeToResources
        {
            internal static readonly ConvertedPath GenFolder = new(Path.Combine("Eflatun", "SceneReference"));
            internal static readonly ConvertedPath GenFile = new(Path.Combine(GenFolder.GivenPath, "map.generated.json"));
        }
        
        internal static class RelativeToAssets
        {
            internal static readonly ConvertedPath GenFolder = new(Path.Combine(ResourcesFolder.GivenPath, RelativeToResources.GenFolder.GivenPath));
            internal static readonly ConvertedPath GenFile = new(Path.Combine(ResourcesFolder.GivenPath, RelativeToResources.GenFile.GivenPath));
        }
    }
}
