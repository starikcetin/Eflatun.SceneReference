using System.IO;
using Eflatun.SceneReference.Utility;
using JetBrains.Annotations;
using UnityEngine;

namespace Eflatun.SceneReference
{
    /// <summary>
    /// Paths used by Eflatun.SceneReference.
    /// </summary>
    [PublicAPI]
    public static class Paths
    {
        private static readonly ConvertedPath AssetsFolder = new(Application.dataPath);
        private static readonly ConvertedPath ResourcesFolder = new(Path.Combine(AssetsFolder.GivenPath, "Resources"));
        
        /// <summary>
        /// Relative to the 'Assets/Resources' folder.
        /// </summary>
        public static class RelativeToResources
        {
            /// <summary>
            /// Path to the folder containing the generated map file. Relative to the 'Assets/Resources' folder.
            /// </summary>
            public static readonly ConvertedPath MapFolder = new(Path.Combine("Eflatun", "SceneReference"));
            
            /// <summary>
            /// Path to the generated map file. Relative to the 'Assets/Resources' folder.
            /// </summary>
            public static readonly ConvertedPath MapFile = new(Path.Combine(MapFolder.GivenPath, "map.generated.json"));
        }
        
        /// <summary>
        /// Relative to the 'Assets' folder.
        /// </summary>
        public static class RelativeToAssets
        {
            /// <summary>
            /// Path to the folder containing the generated map file. Relative to the 'Assets' folder.
            /// </summary>
            public static readonly ConvertedPath MapFolder = new(Path.Combine(ResourcesFolder.GivenPath, RelativeToResources.MapFolder.GivenPath));
            
            /// <summary>
            /// Path to the generated map file. Relative to the 'Assets' folder.
            /// </summary>
            public static readonly ConvertedPath MapFile = new(Path.Combine(ResourcesFolder.GivenPath, RelativeToResources.MapFile.GivenPath));
        }
    }
}
