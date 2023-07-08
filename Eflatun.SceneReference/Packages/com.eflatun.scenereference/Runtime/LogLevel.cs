// NOTE: This file is placed in the runtime assembly to make sure we don't need to introduce a breaking change in case
// we implement log levels for the runtime logger in the future.

using JetBrains.Annotations;

namespace Eflatun.SceneReference
{
    /// <summary>
    /// Determines which types of logs are printed to the console.
    /// </summary>
    [PublicAPI]
    public enum LogLevel
    {
        /// <summary>
        /// All logs.
        /// </summary>
        Debug = 10,

        /// <summary>
        /// Warnings and errors.
        /// </summary>
        Warning = 20,

        /// <summary>
        /// Only errors.
        /// </summary>
        Error = 30,

        /// <summary>
        /// None.
        /// </summary>
        Silent = 40,
    }
}
