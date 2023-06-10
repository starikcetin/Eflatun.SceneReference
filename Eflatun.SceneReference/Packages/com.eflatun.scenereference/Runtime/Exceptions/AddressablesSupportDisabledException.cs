using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Eflatun.SceneReference.Exceptions
{
    /// <summary>
    /// Thrown if the addressables support is disabled.
    /// </summary>
    [PublicAPI]
    [Serializable]
    public class AddressablesSupportDisabledException : SceneReferenceException
    {
        private const string ExceptionMessage = "Addressables support is disabled. To fix this, make sure you have addressables support enabled.";

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
