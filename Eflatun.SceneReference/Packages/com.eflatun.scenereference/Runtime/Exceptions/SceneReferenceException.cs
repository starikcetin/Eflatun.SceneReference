using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Eflatun.SceneReference.Exceptions
{
    /// <summary>
    /// Root custom exception for the <c>Eflatun.SceneReference</c> package.
    /// </summary>
    [PublicAPI]
    [Serializable]
    public class SceneReferenceException : Exception
    {
        internal SceneReferenceException()
        {
        }

        internal SceneReferenceException(string message) : base($"{Constants.LogPrefixBase} {message}")
        {
        }

        internal SceneReferenceException(string message, Exception inner) : base($"{Constants.LogPrefixBase} {message}", inner)
        {
        }

        private protected SceneReferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
