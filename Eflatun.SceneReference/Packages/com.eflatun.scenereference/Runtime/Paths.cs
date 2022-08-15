using System.IO;
using Eflatun.SceneReference.Utility;
using UnityEngine;

namespace Eflatun.SceneReference
{
    /// <summary>
    /// Paths used by Eflatun.SceneReference.
    /// </summary>
    internal static class Paths
    {
        private static readonly ConvertedPath AssetsFolder = new ConvertedPath(Application.dataPath);
        private static readonly ConvertedPath ResourcesFolder = new ConvertedPath(Path.Combine(AssetsFolder.GivenPath, "Resources"));
        
        /// <summary>
        /// Relative to the 'Assets/Resources' folder.
        /// </summary>
        public static class RelativeToResources
        {
            /// <summary>
            /// Path to the folder containing the generated map file. Relative to the 'Assets/Resources' folder.
            /// </summary>
            public static readonly ConvertedPath SceneGuidToPathMapFolder = new ConvertedPath(Path.Combine("Eflatun", "SceneReference"));
            
            /// <summary>
            /// Path to the generated map file. Relative to the 'Assets/Resources' folder.
            /// </summary>
            public static readonly ConvertedPath SceneGuidToPathMapFile = new ConvertedPath(Path.Combine(SceneGuidToPathMapFolder.GivenPath, "SceneGuidToPathMap.generated.json"));
            
            /// <summary>
            /// Path to the `.keep` file in the folder of the generated map file. Relative to the 'Assets/Resources' folder.
            /// </summary>
            public static readonly ConvertedPath SceneGuidToPathMapDotKeepFile = new ConvertedPath(Path.Combine(SceneGuidToPathMapFolder.GivenPath, ".keep"));
        }
        
        /// <summary>
        /// Absolute paths.
        /// </summary>
        public static class Absolute
        {
            /// <summary>
            /// Path to the folder containing the generated map file. Absolute.
            /// </summary>
            public static readonly ConvertedPath SceneGuidToPathMapFolder = new ConvertedPath(Path.Combine(ResourcesFolder.GivenPath, RelativeToResources.SceneGuidToPathMapFolder.GivenPath));
            
            /// <summary>
            /// Path to the generated map file. Absolute.
            /// </summary>
            public static readonly ConvertedPath SceneGuidToPathMapFile = new ConvertedPath(Path.Combine(ResourcesFolder.GivenPath, RelativeToResources.SceneGuidToPathMapFile.GivenPath));
            
            /// <summary>
            /// Path to the `.keep` file in the folder of the generated map file. Absolute.
            /// </summary>
            public static readonly ConvertedPath SceneGuidToPathMapDotKeepFile = new ConvertedPath(Path.Combine(ResourcesFolder.GivenPath, RelativeToResources.SceneGuidToPathMapDotKeepFile.GivenPath));
        }
    }
}
