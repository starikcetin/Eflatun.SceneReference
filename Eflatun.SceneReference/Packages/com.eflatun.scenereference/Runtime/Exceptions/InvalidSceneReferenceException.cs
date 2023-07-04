using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Eflatun.SceneReference.Exceptions
{
    /// <summary>
    /// Thrown if a <see cref="SceneReference"/> is invalid. This can happen for these reasons:
    /// <list type="number">
    /// <item>The <see cref="SceneReference"/> is assigned an invalid scene, or the assigned asset is not a scene. To fix this, make sure the <see cref="SceneReference"/> is assigned a valid scene asset.</item>
    /// <item>The scene GUID to path map is outdated. To fix this, you can either manually run the map generator, or enable all generation triggers. It is highly recommended to keep all the generation triggers enabled.</item>
    /// </list>
    /// </summary>
    [PublicAPI]
    [Serializable]
    public class InvalidSceneReferenceException : SceneReferenceException
    {
        private const string ExceptionMessage =
            "The SceneReference is invalid. This can happen for these reasons:"
            + "\n1. The SceneReference is assigned an invalid scene, or the assigned asset is not a scene. To fix this, make sure the 'SceneReference' is assigned a valid scene asset."
            + "\n2. The scene GUID to path map is outdated. To fix this, you can either manually run the map generator, or enable all generation triggers. It is highly recommended to keep all the generation triggers enabled.";

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
