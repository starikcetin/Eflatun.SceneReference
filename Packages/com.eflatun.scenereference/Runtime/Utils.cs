using System;

namespace Eflatun.SceneReference
{
    internal static class Utils
    {
        internal static string GetBackingFieldName(this string propertyName) => $"<{propertyName}>k__BackingField";
        internal static string WithoutExtension(this string path) => path.Substring(0, path.LastIndexOf(".", StringComparison.Ordinal));
    }
}
