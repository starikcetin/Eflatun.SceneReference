using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Eflatun.SceneReference.Exceptions
{
    /// <summary>
    /// Thrown if an address matches multiple assets.
    /// </summary>
    [PublicAPI]
    [Serializable]
    public class AddressNotUniqueException : SceneReferenceException
    {
        private static string MakeExceptionMessage(string address) =>
            $"Address matches multiple scenes. Address: {address}";

        internal AddressNotUniqueException(string address) : base(MakeExceptionMessage(address))
        {
        }

        internal AddressNotUniqueException(string address, Exception inner) : base(MakeExceptionMessage(address), inner)
        {
        }

        private protected AddressNotUniqueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
