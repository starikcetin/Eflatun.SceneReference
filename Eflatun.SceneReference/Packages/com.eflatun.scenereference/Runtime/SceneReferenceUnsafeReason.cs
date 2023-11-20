using JetBrains.Annotations;

namespace Eflatun.SceneReference
{
    /// <summary>
    /// Describes the reason a <see cref="SceneReference"/> is unsafe.
    /// <list type="number">
    /// <item><see cref="None"/>: <inheritdoc cref="None"/></item>
    /// <item><see cref="Empty"/>: <inheritdoc cref="Empty"/></item>
    /// <item><see cref="NotInMaps"/>: <inheritdoc cref="NotInMaps"/></item>
    /// <item><see cref="NotInBuild"/>: <inheritdoc cref="NotInBuild"/></item>
    /// </list>
    /// </summary>
    [PublicAPI]
    public enum SceneReferenceUnsafeReason
    {
        /// <summary>
        /// The <see cref="SceneReference"/> is safe to use.
        /// </summary>
        None,

        /// <summary>
        /// The <see cref="SceneReference"/> is empty. It is not referencing anything.
        /// </summary>
        Empty,

        /// <summary>
        /// The scene referenced by this <see cref="SceneReference"/> is not found in any of the maps.
        /// </summary>
        NotInMaps,

        /// <summary>
        /// The scene referenced by this <see cref="SceneReference"/> is not added and enabled in build.
        /// </summary>
        NotInBuild,
    }
}
