using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Eflatun.SceneReference
{
    /// <summary>
    /// Thrown if an address is not found in the addressable scene guid to address map.
    /// </summary>
    [PublicAPI]
    [Serializable]
    public class AddressNotFoundException : SceneReferenceException
    {
        private static string MakeExceptionMessage(string address) =>
            $"Address is not found in the addressable scene guid to address map. Address: {address}";

        internal AddressNotFoundException(string address) : base(MakeExceptionMessage(address))
        {
        }

        internal AddressNotFoundException(string address, Exception inner) : base(MakeExceptionMessage(address), inner)
        {
        }

        private protected AddressNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
