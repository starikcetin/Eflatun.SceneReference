using JetBrains.Annotations;

namespace Eflatun.SceneReference
{
    /// <summary>
    /// The inspector coloring override behaviour for <see cref="SceneReference"/> fields.
    /// </summary>
    [PublicAPI]
    public enum ColoringBehaviour
    {
        /// <summary>
        /// Use the project settings.
        /// </summary>
        DoNotOverride = 0,

        /// <summary>
        /// Force enable coloring, disregard project settings.
        /// </summary>
        Enabled = 1,

        /// <summary>
        /// Force disable coloring, disregard project settings.
        /// </summary>
        Disabled = 2
    }
}
