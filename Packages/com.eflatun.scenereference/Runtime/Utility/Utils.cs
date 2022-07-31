using System;

namespace Eflatun.SceneReference.Utility
{
    public static class Utils
    {
        public static string WithoutExtension(this string path) => path.Substring(0, path.LastIndexOf(".", StringComparison.Ordinal));

        public static bool IncludesFlag<T>(this T composite, T flag) where T : Enum => composite.HasFlag(flag);
    }
}
