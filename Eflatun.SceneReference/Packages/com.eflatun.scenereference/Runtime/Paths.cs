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
            /// Path to the folder containing the generated map files. Relative to the 'Assets/Resources' folder.
            /// </summary>
            public static readonly ConvertedPath SceneDataMapsFolder = new ConvertedPath(Path.Combine("Eflatun", "SceneReference"));

            /// <summary>
            /// Path to the generated scene GUID to path map file. Relative to the 'Assets/Resources' folder.
            /// </summary>
            public static readonly ConvertedPath SceneGuidToPathMapFile = new ConvertedPath(Path.Combine(SceneDataMapsFolder.GivenPath, "SceneGuidToPathMap.generated.json"));

            /// <summary>
            /// Path to the `.keep` file in the folder of the generated map files. Relative to the 'Assets/Resources' folder.
            /// </summary>
            public static readonly ConvertedPath SceneDataMapsDotKeepFile = new ConvertedPath(Path.Combine(SceneDataMapsFolder.GivenPath, ".keep"));

            /// <summary>
            /// Path to the generated addressable scene GUID to address map file. Relative to the 'Assets/Resources' folder.
            /// </summary>
            public static readonly ConvertedPath AddressableSceneGuidToAddressMapFile = new ConvertedPath(Path.Combine(SceneDataMapsFolder.GivenPath, "AddressableSceneGuidToAddressMap.generated.json"));
        }

        /// <summary>
        /// Absolute paths.
        /// </summary>
        public static class Absolute
        {
            /// <summary>
            /// Path to the folder containing the generated map files. Absolute.
            /// </summary>
            public static readonly ConvertedPath SceneDataMapsFolder = new ConvertedPath(Path.Combine(ResourcesFolder.GivenPath, RelativeToResources.SceneDataMapsFolder.GivenPath));

            /// <summary>
            /// Path to the generated scene GUID to path map file. Absolute.
            /// </summary>
            public static readonly ConvertedPath SceneGuidToPathMapFile = new ConvertedPath(Path.Combine(ResourcesFolder.GivenPath, RelativeToResources.SceneGuidToPathMapFile.GivenPath));

            /// <summary>
            /// Path to the `.keep` file in the folder of the generated map files. Absolute.
            /// </summary>
            public static readonly ConvertedPath SceneDataMapsDotKeepFile = new ConvertedPath(Path.Combine(ResourcesFolder.GivenPath, RelativeToResources.SceneDataMapsDotKeepFile.GivenPath));

            /// <summary>
            /// Path to the generated addressable scene GUID to address map file. Absolute.
            /// </summary>
            public static readonly ConvertedPath AddressableSceneGuidToAddressMapFile = new ConvertedPath(Path.Combine(ResourcesFolder.GivenPath, RelativeToResources.AddressableSceneGuidToAddressMapFile.GivenPath));
        }
    }
}
