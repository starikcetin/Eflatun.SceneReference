using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Eflatun.SceneReference.Exceptions
{
    /// <summary>
    /// Thrown if a <see cref="SceneReference"/> is empty (not assigned anything).
    /// To fix this, make sure the <see cref="SceneReference"/> is assigned a valid scene asset.
    /// </summary>
    [PublicAPI]
    [Serializable]
    public class EmptySceneReferenceException : SceneReferenceException
    {
        private const string ExceptionMessage =
            "The SceneReference is empty (not assigned anything). To fix this, make sure the 'SceneReference' is assigned a valid scene asset.";

        internal EmptySceneReferenceException() : base(ExceptionMessage)
        {
        }

        internal EmptySceneReferenceException(Exception inner) : base(ExceptionMessage, inner)
        {
        }

        private protected EmptySceneReferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
