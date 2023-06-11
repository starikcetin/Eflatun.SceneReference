﻿using System;
using System.Runtime.Serialization;

namespace Eflatun.SceneReference.Exceptions
{
    [Serializable]
    internal class SceneReferenceInternalException : SceneReferenceException
    {
        public static SceneReferenceInternalException InvalidGuid(string location, string guid) =>
            new SceneReferenceInternalException(location, $"GUID is invalid. GUID: \"{guid}\"");

        public static SceneReferenceInternalException ImpossibleException<TException>(string location, TException exception)
            where TException : Exception =>
            new SceneReferenceInternalException(location, $"Impossible exception \"{typeof(TException).Name}\". Exception: \n{exception.ToString()}");

        private SceneReferenceInternalException(string location, string info)
            : base($"If you are seeing this, something has gone wrong internally. Please open an issue on Github (https://github.com/starikcetin/Eflatun.SceneReference/issues) and include the following information:\nLocation: {location}\nInfo: {info}")
        {
        }

        private protected SceneReferenceInternalException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
