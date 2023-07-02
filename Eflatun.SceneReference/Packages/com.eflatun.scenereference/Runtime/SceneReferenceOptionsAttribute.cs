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
        /// For scene-in-build state.<br/>
        /// <inheritdoc cref="ColoringBehaviour"/><br/>
        /// Defaults to <see cref="ColoringBehaviour.DoNotOverride"/>.
        /// </summary>
        public ColoringBehaviour SceneInBuildColoring = ColoringBehaviour.DoNotOverride;

        /// <summary>
        /// <inheritdoc cref="ToolboxBehaviour"/><br/>
        /// Defaults to <see cref="ToolboxBehaviour.DoNotOverride"/>.
        /// </summary>
        public ToolboxBehaviour Toolbox = ToolboxBehaviour.DoNotOverride;

        /// <summary>
        /// For addressable scenes.<br/>
        /// <inheritdoc cref="ColoringBehaviour"/><br/>
        /// Defaults to <see cref="ColoringBehaviour.DoNotOverride"/>.
        /// </summary>
        public ColoringBehaviour AddressableColoring = ColoringBehaviour.DoNotOverride;
    }
}
