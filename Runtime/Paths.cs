using Eflatun.SceneReference.Utility;
using System.IO;
using UnityEngine;

namespace Eflatun.SceneReference
{
    /// <summary>
    /// Paths used by Eflatun.SceneReference.
    /// </summary>
    internal static class Paths
    {
        private static readonly ConvertedPath AssetsFolder = new ConvertedPath(Application.dataPath);
        public static readonly ConvertedPath ResourcesFolder = new ConvertedPath(Path.Combine(AssetsFolder.GivenPath, "Resources"));
        public static readonly ConvertedPath ResourcesMetaFile = new ConvertedPath(Path.Combine(AssetsFolder.GivenPath, "Resources.meta"));

        /// <summary>
        /// Relative to the 'Assets/Resources' folder.
        /// </summary>
        public static class RelativeToResources
        {
            private static readonly string MapPrefix = "Eflatun_SceneReference_";
            private static readonly string MapExt = ".generated.json";

            public static readonly string SceneGuidToPathMapFile = $"{MapPrefix}SceneGuidToPathMap{MapExt}";
            public static readonly string SceneGuidToPathMapMetaFile = $"{MapPrefix}SceneGuidToPathMap{MapExt}.meta";

            public static readonly string SceneGuidToAddressMapFile = $"{MapPrefix}SceneGuidToAddressMap{MapExt}";
            public static readonly string SceneGuidToAddressMapMetaFile = $"{MapPrefix}SceneGuidToAddressMap{MapExt}.meta";
        }

        /// <summary>
        /// Absolute paths.
        /// </summary>
        public static class Absolute
        {
            public static readonly ConvertedPath SceneGuidToPathMapFile = new ConvertedPath(Path.Combine(ResourcesFolder.GivenPath, RelativeToResources.SceneGuidToPathMapFile));
            public static readonly ConvertedPath SceneGuidToPathMapMetaFile = new ConvertedPath(Path.Combine(ResourcesFolder.GivenPath, RelativeToResources.SceneGuidToPathMapMetaFile));

            public static readonly ConvertedPath SceneGuidToAddressMapFile = new ConvertedPath(Path.Combine(ResourcesFolder.GivenPath, RelativeToResources.SceneGuidToAddressMapFile));
            public static readonly ConvertedPath SceneGuidToAddressMapMetaFile = new ConvertedPath(Path.Combine(ResourcesFolder.GivenPath, RelativeToResources.SceneGuidToAddressMapMetaFile));
        }
    }
}
