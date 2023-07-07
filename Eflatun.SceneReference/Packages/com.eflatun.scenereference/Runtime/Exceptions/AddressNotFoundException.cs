using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Eflatun.SceneReference.Exceptions
{
    /// <summary>
    /// Thrown if a given address is not found in the Scene GUID to Address Map. This can happen for these reasons:
    /// <list type="number">
    /// <item>The asset with the given address either doesn't exist or is not a scene. To fix this, make sure you provide the address of a valid addressable scene.</item>
    /// <item>The Scene GUID to Address Map is outdated. To fix this, you can either manually run the generator, or enable generation triggers. It is highly recommended to keep all the generation triggers enabled.</item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// This exception will never be thrown if addressables support is disabled.
    /// </remarks>
    [PublicAPI]
    [Serializable]
    public class AddressNotFoundException : SceneReferenceException
    {
        private static string MakeExceptionMessage(string address) =>
            $"The address is not found in the Scene GUID to Address Map. Address: {address}." +
            "\nThis can happen for these reasons:" +
            "\n1. The asset with the given address either doesn't exist or is not a scene. To fix this, make sure you provide the address of a valid addressable scene." +
            "\n2. The Scene GUID to Address Map is outdated. To fix this, you can either manually run the generator, or enable generation triggers. It is highly recommended to keep all the generation triggers enabled.";

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
