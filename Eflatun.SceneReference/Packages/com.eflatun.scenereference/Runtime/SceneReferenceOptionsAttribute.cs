using System;
using JetBrains.Annotations;

namespace Eflatun.SceneReference
{
    /// <summary>
    /// Provides the ability to set per-field settings for <see cref="SceneReference"/> fields. 
    /// </summary>
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class SceneReferenceOptionsAttribute : Attribute
    {
        /// <summary>
        /// <inheritdoc cref="ColoringBehaviour"/>
        /// Defaults to <see cref="ColoringBehaviour.DoNotOverride"/>.
        /// </summary>
        public ColoringBehaviour Coloring = ColoringBehaviour.DoNotOverride;

        /// <summary>
        /// <inheritdoc cref="ToolboxBehaviour"/>
        /// Defaults to <see cref="ToolboxBehaviour.DoNotOverride"/>.
        /// </summary>
        public ToolboxBehaviour Toolbox = ToolboxBehaviour.DoNotOverride;
    }
}
