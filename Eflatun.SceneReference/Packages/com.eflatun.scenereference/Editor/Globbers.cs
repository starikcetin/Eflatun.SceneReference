using System;
using Matcher = Ignore.Ignore;

namespace Eflatun.SceneReference.Editor
{
    internal static class Globbers
    {
        public static Globber Coloring = new Globber(SettingsManager.UtilityIgnores.ColoringIgnoresGlobs.value);
        public static Globber Toolbox = new Globber(SettingsManager.UtilityIgnores.ToolboxIgnoresGlobs.value);

        internal class Globber
        {
            private Matcher Matcher;

            public Globber(string patterns)
            {
                SetPattern(patterns);
            }

            public void SetPattern(string patterns)
            {
                Matcher = new Matcher();
                var split = patterns.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                Matcher.Add(split);
            }

            public bool IsIgnored(string path)
            {
                return Matcher.IsIgnored(path);
            }
        }
    }
}
