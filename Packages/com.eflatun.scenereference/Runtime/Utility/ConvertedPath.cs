using System.IO;

namespace Eflatun.SceneReference.Utility
{
    public class ConvertedPath
    {
        public string GivenPath { get; }
        public string WindowsPath { get; }
        public string UnixPath { get; }
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
