using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Eflatun.SceneReference.Exceptions
{
    /// <summary>
    /// Thrown when something goes wrong during the creation of a <see cref="SceneReference"/>.
    /// </summary>
    [PublicAPI]
    [Serializable]
    public class SceneReferenceCreationException : SceneReferenceException
    {
        private static string PrefixMessage(string message) => $"An exception occured during the creation of a {nameof(SceneReference)}: {message}";

        internal SceneReferenceCreationException(string message) : base(PrefixMessage(message))
        {
        }

        internal SceneReferenceCreationException(string message, Exception inner) : base(PrefixMessage(message), inner)
        {
        }

        private protected SceneReferenceCreationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
