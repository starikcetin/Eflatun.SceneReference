using JetBrains.Annotations;

namespace Eflatun.SceneReference
{
    /// <summary>
    /// The inspector utility line override behaviour for scene-in-build validation of <see cref="SceneReference"/> fields.
    /// </summary>
    [PublicAPI]
    public enum UtilityLineBehaviour
    {
        /// <summary>
        /// Use the project settings.
        /// </summary>
        DoNotOverride = 0,

        /// <summary>
        /// Force enable the utility line, disregard project settings.
        /// </summary>
        Enabled = 1,

        /// <summary>
        /// Force disable the utility line, disregard project settings.
        /// </summary>
        Disabled = 2
    }
}
