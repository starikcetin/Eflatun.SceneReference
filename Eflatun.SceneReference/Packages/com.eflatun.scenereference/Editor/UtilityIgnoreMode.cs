using JetBrains.Annotations;

namespace Eflatun.SceneReference.Editor
{
    /// <summary>
    /// The mode of operation for preventing certain scenes from having inline utilities.
    /// </summary>
    [PublicAPI]
    public enum UtilityIgnoreMode
    {
        /// <summary>
        /// Nothing will be ignored.
        /// </summary>
        Disabled = 0,

        /// <summary>
        /// A list of scenes will be ignored.
        /// </summary>
        List = 1,

        /// <summary>
        /// Patterns will be used to ignore scenes.
        /// </summary>
        /// <remarks>
        /// This library is used for matching patterns: <see href="https://github.com/goelhardik/ignore"/>
        /// </remarks>
        Patterns = 2,
    }
}
