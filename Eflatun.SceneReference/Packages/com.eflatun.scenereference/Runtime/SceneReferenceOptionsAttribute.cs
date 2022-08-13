using System;

namespace Eflatun.SceneReference
{
    /// <summary>
    /// Provides the ability to set per-field settings for <see cref="SceneReference"/> fields. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class SceneReferenceOptionsAttribute : Attribute
    {
        /// <summary>
        /// <inheritdoc cref="ColoringBehaviour"/>
        /// Defaults to <see cref="ColoringBehaviour.DoNotOverride"/>.
        /// </summary>
        public ColoringBehaviour Coloring = ColoringBehaviour.DoNotOverride;
        
        /// <summary>
        /// <inheritdoc cref="UtilityLineBehaviour"/>
        /// Defaults to <see cref="UtilityLineBehaviour.DoNotOverride"/>.
        /// </summary>
        public UtilityLineBehaviour UtilityLine = UtilityLineBehaviour.DoNotOverride;
    }
}
