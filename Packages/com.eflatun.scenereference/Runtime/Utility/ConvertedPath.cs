using System.IO;
using JetBrains.Annotations;

namespace Eflatun.SceneReference.Utility
{
    /// <summary>
    /// Wrapper for a file/folder path. Provides platform-specific paths. 
    /// </summary>
    [PublicAPI]
    public class ConvertedPath
    {
        /// <summary>
        /// The path originally given to the constructor.
        /// </summary>
        public string GivenPath { get; }
        
        /// <summary>
        /// Path for the Window platform.
        /// </summary>
        public string WindowsPath { get; }
        
        /// <summary>
        /// Path for the Unix platform.
        /// </summary>
        public string UnixPath { get; }
        
        /// <summary>
        /// Path for the current platform.
        /// </summary>
        public string PlatformPath { get; }
        
        public ConvertedPath(string path)
        {
            GivenPath = path;
            var split = path.Split('\\', '/');
            WindowsPath = string.Join('\\', split);
            UnixPath = string.Join('/', split);
            PlatformPath = string.Join(Path.DirectorySeparatorChar, split);
        }
    }
}
