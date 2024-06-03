﻿using System;

namespace Eflatun.SceneReference.Editor
{
    internal static class IgnoreCheckers
    {
        public static IgnoreChecker Coloring = new IgnoreChecker(SettingsManager.UtilityIgnores.ColoringIgnoresPatterns.value);
        public static IgnoreChecker Toolbox = new IgnoreChecker(SettingsManager.UtilityIgnores.ToolboxIgnoresPatterns.value);

        internal class IgnoreChecker
        {
            private Ignore.Ignore Checker;

            public IgnoreChecker(string patterns)
            {
                SetPatterns(patterns);
            }

            public void SetPatterns(string patterns)
            {
                Checker = new Ignore.Ignore();
                var split = patterns.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                Checker.Add(split);
            }

            public bool IsIgnored(string path)
            {
                return Checker.IsIgnored(path);
            }
        }
    }
}
