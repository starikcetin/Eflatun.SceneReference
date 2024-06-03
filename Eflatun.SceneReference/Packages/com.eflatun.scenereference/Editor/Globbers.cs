using DotNet.Globbing;

namespace Eflatun.SceneReference.Editor
{
    internal static class Globbers
    {
        public static Globber Coloring = new Globber(SettingsManager.UtilityIgnores.ColoringIgnoresGlobs.value);
        public static Globber Toolbox = new Globber(SettingsManager.UtilityIgnores.ToolboxIgnoresGlobs.value);

        internal class Globber
        {
            private Glob Glob;

            public Globber(string pattern)
            {
                SetPattern(pattern);
            }

            public void SetPattern(string pattern)
            {
                Glob = string.IsNullOrWhiteSpace(pattern) ? null : Glob.Parse(pattern);
            }

            public bool IsIgnored(string path)
            {
                return Glob != null && Glob.IsMatch(path);
            }
        }
    }
}
