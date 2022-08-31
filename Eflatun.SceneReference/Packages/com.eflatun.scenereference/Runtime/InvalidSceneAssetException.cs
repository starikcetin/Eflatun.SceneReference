using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Eflatun.SceneReference
{
    /// <summary>
    /// Thrown if a SceneReference is assigned a null or invalid scene asset.<p/>
    /// To fix it, make sure the SceneReference is assigned a non-null and valid scene asset.<p/>
    /// You can check <see cref="SceneReference.IsValidSceneAsset"/> to see if the scene asset is non-null and valid.
    /// </summary>
    [PublicAPI]
    [Serializable]
    public class InvalidSceneAssetException : SceneReferenceException
    {
        internal InvalidSceneAssetException()
        {
        }

        internal InvalidSceneAssetException(string message) : base(message)
        {
        }

        internal InvalidSceneAssetException(string message, Exception inner) : base(message, inner)
        {
        }

        private protected InvalidSceneAssetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
