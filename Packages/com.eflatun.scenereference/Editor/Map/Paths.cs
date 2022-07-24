using System.IO;
using UnityEngine;

namespace Eflatun.SceneReference.Editor.Map
{
    public static class Paths
    {
        public const string MapPatchFolderName = "Eflatun.SceneReference Generated";

        public static readonly string AssetsFolderPath = Application.dataPath;
        public static readonly string ProjectRootPath = Path.GetDirectoryName(AssetsFolderPath);

        public static readonly string SourceFolderPath = Path.Combine(ProjectRootPath!, "Packages", "com.eflatun.scenereference", "Patches", MapPatchFolderName);
        public static readonly string DestinationFolderPath = Path.Combine(AssetsFolderPath, MapPatchFolderName);

        public static readonly string GeneratedWriteFilePath = Path.Combine(DestinationFolderPath, "Map.Generated.cs");
    }
}
