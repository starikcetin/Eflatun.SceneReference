using JetBrains.Annotations;

namespace Eflatun.SceneReference
{
    /// <summary>
    /// State of a <see cref="SceneReference"/>.
    /// <list type="number">
    /// <item><see cref="Unsafe"/>: <inheritdoc cref="Unsafe"/></item>
    /// <item><see cref="Regular"/>: <inheritdoc cref="Regular"/></item>
    /// <item><see cref="Addressable"/>: <inheritdoc cref="Addressable"/></item>
    /// </list>
    /// </summary>
    [PublicAPI]
    public enum SceneReferenceState
    {
        /// <summary>
        /// The <see cref="SceneReference"/> is not safe to use. Something is wrong.
        /// </summary>
        Unsafe = 0,

        /// <summary>
        /// The <see cref="SceneReference"/> is safe to use, and it references a regular scene.
        /// </summary>
        Regular = 1,

        /// <summary>
        /// The <see cref="SceneReference"/> is safe to use, and it references an addressable scene.
        /// This state is only possible if the addressables support is enabled.
        /// </summary>
        Addressable = 2,
    }
}
