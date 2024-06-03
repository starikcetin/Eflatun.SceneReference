using DotNet.Globbing;
using System;
using System.Linq;

namespace Eflatun.SceneReference.Editor
{
    internal static class Globbers
    {
        public static Globber Coloring = new Globber(SettingsManager.UtilityIgnores.ColoringIgnoresGlobs.value);
        public static Globber Toolbox = new Globber(SettingsManager.UtilityIgnores.ToolboxIgnoresGlobs.value);

        internal class Globber
        {
            private Glob[] Globs;

            public Globber(string patterns)
            {
                SetPattern(patterns);
            }

            public void SetPattern(string patterns)
            {
                Globs = patterns
                    .Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => string.IsNullOrWhiteSpace(x) ? null : Glob.Parse(x))
                    .ToArray();
            }

            public bool IsIgnored(string path)
            {
                return Globs.Any(x => x.IsMatch(path));
            }
        }
    }
}
