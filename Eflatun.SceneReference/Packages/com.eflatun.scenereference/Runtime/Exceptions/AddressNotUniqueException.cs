using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Eflatun.SceneReference.Exceptions
{
    /// <summary>
    /// Thrown if a given address matches multiple entries in the Scene GUID to Address Map. This can happen for these reasons:
    /// <list type="number">
    /// <item>There are multiple addressable scenes with the same given address. To fix this, make sure there is only one addressable scene with the given address.</item>
    /// <item>The Scene GUID to Address Map is outdated. To fix this, you can either manually run the generator, or enable generation triggers. It is highly recommended to keep all the generation triggers enabled.</item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// This exception will never be thrown if addressables support is disabled.
    /// </remarks>
    [PublicAPI]
    [Serializable]
    public class AddressNotUniqueException : SceneReferenceException
    {
        private static string MakeExceptionMessage(string address) =>
            $"The address matches multiple scenes in the Scene GUID to Address Map. Address: {address}." +
            "\nThrown if a given address matches multiple entries in the Scene GUID to Address Map. This can happen for these reasons:" +
            "\n1. There are multiple addressable scenes with the same given address. To fix this, make sure there is only one addressable scene with the given address." +
            "\n2. The Scene GUID to Address Map is outdated. To fix this, you can either manually run the generator, or enable generation triggers. It is highly recommended to keep all the generation triggers enabled.";

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
