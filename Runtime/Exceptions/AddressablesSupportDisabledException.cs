using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Eflatun.SceneReference.Exceptions
{
    /// <summary>
    /// Thrown if an operation that requires addressables support is attempted while addressables support is disabled.
    /// To fix it, make sure addressables support is enabled.
    /// </summary>
    [PublicAPI]
    [Serializable]
    public class AddressablesSupportDisabledException : SceneReferenceException
    {
        private const string ExceptionMessage = "An operation that requires addressables support is attempted while addressables support is disabled. To fix it, make sure addressables support is enabled.";

        internal AddressablesSupportDisabledException() : base(ExceptionMessage)
        {
        }

        internal AddressablesSupportDisabledException(Exception inner) : base(ExceptionMessage, inner)
        {
        }

        private protected AddressablesSupportDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
