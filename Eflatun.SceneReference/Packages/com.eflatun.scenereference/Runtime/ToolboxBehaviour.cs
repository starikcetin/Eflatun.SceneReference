using JetBrains.Annotations;

namespace Eflatun.SceneReference
{
    /// <summary>
    /// The inspector toolbox button override behaviour for <see cref="SceneReference"/> fields.
    /// </summary>
    [PublicAPI]
    public enum ToolboxBehaviour
    {
        /// <summary>
        /// Use the project settings.
        /// </summary>
        DoNotOverride = 0,

        /// <summary>
        /// Force enable the toolbox button, disregard project settings.
        /// </summary>
        Enabled = 1,

        /// <summary>
        /// Force disable the toolbox button, disregard project settings.
        /// </summary>
        Disabled = 2
    }
}
