using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Eflatun.SceneReference.Exceptions
{
    /// <summary>
    /// Thrown if a <see cref="SceneReference"/> is not addressable.
    /// </summary>
    [PublicAPI]
    [Serializable]
    public class SceneNotAddressableException : SceneReferenceException
    {
        private const string ExceptionMessage =
            "The scene referenced by the SceneReference is not addressable. To fix this, make sure the scene is addressable.";

        internal SceneNotAddressableException() : base(ExceptionMessage)
        {
        }

        internal SceneNotAddressableException(Exception inner) : base(ExceptionMessage, inner)
        {
        }

        private protected SceneNotAddressableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
