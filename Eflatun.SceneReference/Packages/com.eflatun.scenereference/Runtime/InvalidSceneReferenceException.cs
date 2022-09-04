using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Eflatun.SceneReference
{
    /// <summary>
    /// Thrown if a SceneReference is invalid. This can happen for these reasons:
    /// <list type="number">
    /// <item>The SceneReference is not assigned anything, or is assigned an invalid or a null scene. To fix this, make sure the SceneReference is assigned a valid scene asset.</item>
    /// <item>The scene GUID to path map is outdated. To fix this, you can either manually run the map generator, or enable all generation triggers. It is highly recommended to keep all the generation triggers enabled.</item>
    /// </list>
    /// </summary>
    [PublicAPI]
    [Serializable]
    public class InvalidSceneReferenceException : SceneReferenceException
    {
        private const string ExceptionMessage =
            "A SceneReference is invalid. This can happen for these reasons:"
            + "\n1. The SceneReference is not assigned anything, or is assigned an invalid or a null scene. To fix this, make sure to assign a valid scene to the SceneReference."
            + "\n2. The scene GUID to path map is outdated. To fix this, you can either manually run the generator, or enable generation triggers. It is highly recommended to keep all the generation triggers enabled.";

        internal InvalidSceneReferenceException() : base(ExceptionMessage)
        {
        }

        internal InvalidSceneReferenceException(Exception inner) : base(ExceptionMessage, inner)
        {
        }

        private protected InvalidSceneReferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
