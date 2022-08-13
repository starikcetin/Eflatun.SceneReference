﻿namespace Eflatun.SceneReference
{
    /// <summary>
    /// The inspector coloring override behaviour for scene-in-build validation of <see cref="SceneReference"/> fields.
    /// </summary>
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
