namespace Eflatun.SceneReference
{
    internal static class Utils
    {
        internal static string GetBackingFieldName(this string propertyName) => $"<{propertyName}>k__BackingField";
    }
}
