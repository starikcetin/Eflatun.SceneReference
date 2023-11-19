using JetBrains.Annotations;

namespace Eflatun.SceneReference
{
    [PublicAPI]
    public enum SceneReferenceUnsafeReason
    {
        None,
        Empty,
        NotInMaps,
        NotInBuild,
    }
}
