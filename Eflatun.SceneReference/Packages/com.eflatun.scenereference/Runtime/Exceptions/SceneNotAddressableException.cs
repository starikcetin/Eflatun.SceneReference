using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Eflatun.SceneReference.Exceptions
{
    /// <summary>
    /// Thrown if addressables-specific operations are attempted on a <see cref="SceneReference"/> that is assigned a non-addressable scene.<p/>
    /// You can avoid this exception by making sure the <see cref="SceneReference.State"/> property is <see cref="SceneReferenceState.Addressable"/>.
    /// </summary>
    [PublicAPI]
    [Serializable]
    public class SceneNotAddressableException : SceneReferenceException
    {
        private const string ExceptionMessage =
            "An addressables-specific operation is attempted on a SceneReference that is assigned a non-addressable scene." +
            "\nYou can avoid this exception by making sure the State property is Addressable.";

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
