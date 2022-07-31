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
            public static readonly ConvertedPath SceneGuidToPathMapFolder = new(Path.Combine("Eflatun", "SceneReference"));
            
            /// <summary>
            /// Path to the generated map file. Relative to the 'Assets/Resources' folder.
            /// </summary>
            public static readonly ConvertedPath SceneGuidToPathMapFile = new(Path.Combine(SceneGuidToPathMapFolder.GivenPath, "SceneGuidToPathMap.generated.json"));
        }
        
        /// <summary>
        /// Absolute paths.
        /// </summary>
        public static class Absolute
        {
            /// <summary>
            /// Path to the folder containing the generated map file. Absolute.
            /// </summary>
            public static readonly ConvertedPath SceneGuidToPathMapFolder = new(Path.Combine(ResourcesFolder.GivenPath, RelativeToResources.SceneGuidToPathMapFolder.GivenPath));
            
            /// <summary>
            /// Path to the generated map file. Absolute.
            /// </summary>
            public static readonly ConvertedPath SceneGuidToPathMapFile = new(Path.Combine(ResourcesFolder.GivenPath, RelativeToResources.SceneGuidToPathMapFile.GivenPath));
        }
    }
}
