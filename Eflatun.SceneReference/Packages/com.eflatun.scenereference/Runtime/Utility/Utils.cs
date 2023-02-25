using System;
using System.Linq;

namespace Eflatun.SceneReference.Utility
{
    /// <summary>
    /// Utilities used by Eflatun.SceneReference.
    /// </summary>
    internal static class Utils
    {
        // GUID hex of an invalid asset contains all zeros. A GUID hex has 32 chars.
        public const string AllZeroGuidHex = "00000000000000000000000000000000";

        /// <summary>
        /// Returns the given <paramref name="path"/> without file extension.
        /// </summary>
        public static string WithoutExtension(this string path) => path.BeforeLast('.');

        /// <summary>
        /// Returns whether flags enum <paramref name="composite"/> includes all flags in <paramref name="flag"/>.
        /// </summary>
        public static bool IncludesFlag<T>(this T composite, T flag) where T : Enum => composite.HasFlag(flag);

        /// <summary>
        /// Returns the portion of <paramref name="source"/> that comes before the last occurence of <paramref name="chr"/>.
        /// </summary>
        public static string BeforeLast(this string source, char chr)
        {
            var lastChrIndex = source.LastIndexOf(chr);
            return lastChrIndex < 0
                ? source
                : source.Substring(0, lastChrIndex);
        }

        /// <summary>
        /// Returns if the given <paramref name="guidHex"/> is valid. A valid GUID hex is 32 chars of hexadecimals.
        /// </summary>
        public static bool IsValidGuidHex(this string guidHex) =>
            guidHex.Length == 32 && guidHex.ToUpper().All("0123456789ABCDEF".Contains);

        /// <summary>
        /// If the given GUID is null or whitespace returns <see cref="AllZeroGuidHex"/>. Otherwise returns as-is.
        /// </summary>
        public static string GuardGuidAgainstNullOrWhitespace(this string guid) =>
            string.IsNullOrWhiteSpace(guid) ? AllZeroGuidHex : guid;
    }
}
