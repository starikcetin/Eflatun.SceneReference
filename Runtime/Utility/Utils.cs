using System;
using System.Collections.Generic;
using System.Linq;

namespace Eflatun.SceneReference.Utility
{
    /// <summary>
    /// Utilities used by Eflatun.SceneReference.
    /// </summary>
    internal static class Utils
    {
        /// <summary>
        /// GUID of all zeros. Invalid assets have this GUID.
        /// </summary>
        public const string AllZeroGuid = "00000000000000000000000000000000";

        /// <summary>
        /// Returns whether the addressables package is installed in the project.
        /// </summary>
        public static bool IsAddressablesPackagePresent =>
#if ESR_ADDRESSABLES
            true
#else // ESR_ADDRESSABLES
            false
#endif // ESR_ADDRESSABLES
        ;

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
        /// Returns if the given <paramref name="guid"/> is valid. A valid GUID is 32 chars of hexadecimals.
        /// </summary>
        public static bool IsValidGuid(this string guid) =>
            guid.Length == 32 && guid.ToUpper().All("0123456789ABCDEF".Contains);

        /// <summary>
        /// If the given GUID is null or whitespace returns <see cref="AllZeroGuid"/>. Otherwise returns as-is.
        /// </summary>
        public static string GuardGuidAgainstNullOrWhitespace(this string guid) =>
            string.IsNullOrWhiteSpace(guid) ? AllZeroGuid : guid;

// Backwards compatibility: KVP deconstruction is not built-in for Unity versions before 2021.3.
#if !UNITY_2021_3_OR_NEWER
        /// <summary>
        /// Allows deconstructing <see cref="KeyValuePair{TKey, TValue}"/> (for <see cref="Dictionary{TKey, TValue}"/>).
        /// </summary>
        public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> kvp, out TKey key, out TValue value)
        {
            key = kvp.Key;
            value = kvp.Value;
        }
#endif

        /// <summary>
        /// Convert <see cref="IReadOnlyDictionary{TKey, TValue}"/> to <see cref="Dictionary{TKey, TValue}"/>.
        /// </summary>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> readOnly)
        {
// Backwards compatibility: Dictionary did not have a constructor that took in an IReadOnlyDictionary for Unity versions before 2021.3.
#if UNITY_2021_3_OR_NEWER
            return new Dictionary<TKey, TValue>(readOnly);
#else
            return readOnly.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
#endif
        }
    }
}
