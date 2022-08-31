using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Eflatun.SceneReference
{
    /// <summary>
    /// Thrown if the scene assigned to a SceneReference is non-null and valid, but it cannot be found in the scene
    /// GUID to path map. This happens if the map is outdated.<p/>
    /// To fix it, you can run the generator manually, or enable the automatic generation triggers. It is highly
    /// recommended to keep all the automatic generation triggers enabled to avoid this exception.
    /// </summary>
    [PublicAPI]
    [Serializable]
    public class OutdatedSceneGuidToPathMapException : SceneReferenceException
    {
        internal OutdatedSceneGuidToPathMapException()
        {
        }

        internal OutdatedSceneGuidToPathMapException(string message) : base(message)
        {
        }

        internal OutdatedSceneGuidToPathMapException(string message, Exception inner) : base(message, inner)
        {
        }

        private protected OutdatedSceneGuidToPathMapException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
