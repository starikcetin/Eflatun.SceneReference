using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Eflatun.SceneReference.Exceptions;
using Eflatun.SceneReference.Utility;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR

namespace Eflatun.SceneReference
{
    /// <summary>
    /// Provides a strongly-typed reference to a scene. Can be used in both editor and runtime.
    /// </summary>
    [PublicAPI]
    [Serializable]
    [XmlRoot(XmlRootElementName)]
    public class SceneReference : ISerializationCallbackReceiver, ISerializable, IXmlSerializable
    {
        internal const string XmlRootElementName = "Eflatun.SceneReference.SceneReference";
        internal const string CustomSerializationGuidKey = "sceneAssetGuidHex";

        [FormerlySerializedAs("sceneAsset")]
        [SerializeField] internal UnityEngine.Object asset;

        [FormerlySerializedAs("sceneAssetGuidHex")]
        [SerializeField] internal string guid;

        /// <summary>
        /// Creates a new empty <see cref="SceneReference"/>.
        /// </summary>
        /// <remarks>This constructor never throws.</remarks>
        public SceneReference()
        {
            // This parameterless constructor is required for the custom XML serialization support.
            // See: https://learn.microsoft.com/en-us/dotnet/api/system.xml.serialization.ixmlserializable?view=net-7.0#remarks

            guid = Utils.AllZeroGuid;
            asset = null;
        }

        /// <summary>
        /// Creates a new <see cref="SceneReference"/> which references the scene that has the given GUID.
        /// </summary>
        /// <param name="guid">GUID of the scene to reference.</param>
        /// <exception cref="SceneReferenceCreationException">Throws if the given GUID is null or empty.</exception>
        /// <exception cref="SceneReferenceCreationException">Throws if the given GUID is not found in the Scene GUID to Path map.</exception>
        /// <exception cref="SceneReferenceCreationException">(Editor-only) Throws if the asset is not found at the path that the GUID corresponds to.</exception>
        public SceneReference(string guid)
        {
            if (string.IsNullOrWhiteSpace(guid))
            {
                throw new SceneReferenceCreationException(
                    $"Given GUID is null or whitespace. GUID: '{guid}'." +
                    "\nTo fix this, make sure you provide the GUID of a valid scene.");
            }

            if (!SceneGuidToPathMapProvider.SceneGuidToPathMap.TryGetValue(guid, out var pathFromMap))
            {
                throw new SceneReferenceCreationException(
                    $"Given GUID is not found in the scene GUID to path map. GUID: '{guid}'"
                    + "\nThis can happen for these reasons:"
                    + "\n1. The asset with the given GUID either doesn't exist or is not a scene. To fix this, make sure you provide the GUID of a valid scene."
                    + "\n2. The scene GUID to path map is outdated. To fix this, you can either manually run the generator, or enable generation triggers. It is highly recommended to keep all the generation triggers enabled.");
            }

            this.guid = guid;

#if UNITY_EDITOR
            var foundAsset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(pathFromMap);

            if (!foundAsset)
            {
                throw new SceneReferenceCreationException(
                    $"The given GUID was found in the map, but the scene asset at the corresponding path could not be loaded. Path: '{pathFromMap}'."
                    + "\nThis can happen due to an outdated scene GUID to path map retaining scene assets that no longer exist. To fix this, you can either manually run the generator, or enable generation triggers. It is highly recommended to keep all the generation triggers enabled.");
            }

            asset = foundAsset;
#endif // UNITY_EDITOR
        }

#if UNITY_EDITOR
        /// <summary>
        /// Creates a new <see cref="SceneReference"/> which references the given scene asset.
        /// </summary>
        /// <param name="asset">The asset of the scene to reference.</param>
        /// <exception cref="SceneReferenceCreationException">Throws if the given asset is null.</exception>
        /// <exception cref="SceneReferenceCreationException">Throws if the GUID of the given asset cannot be retrieved.</exception>
        /// <exception cref="SceneReferenceCreationException">Throws if the Scene GUID to Path map does not contain the GUID of the given asset.</exception>
        /// <remarks>This constructor is for editor-use only. Do NOT use it in runtime code.</remarks>
        public SceneReference(UnityEngine.Object asset)
        {
            if (!asset)
            {
                throw new SceneReferenceCreationException("Given scene asset is null. To fix this, make sure you provide a valid scene asset.");
            }

            if (!AssetDatabase.TryGetGUIDAndLocalFileIdentifier(asset, out var guidFromAsset, out long _))
            {
                throw new SceneReferenceCreationException("Could not retrieve the GUID of the given scene asset. This usually indicates an invalid asset. To fix this, make sure you provide a valid scene asset.");
            }

            if (!SceneGuidToPathMapProvider.SceneGuidToPathMap.ContainsKey(guidFromAsset))
            {
                throw new SceneReferenceCreationException(
                    $"The GUID of the given scene asset is not found in the scene GUID to path map. GUID: '{guidFromAsset}'"
                    + "\nThis can happen for these reasons:"
                    + "\n1. Given asset either doesn't exist or is not a scene. To fix this, make sure you provide a valid scene asset."
                    + "\n2. The scene GUID to path map is outdated. To fix this, you can either manually run the generator, or enable generation triggers. It is highly recommended to keep all the generation triggers enabled.");
            }

            guid = guidFromAsset;
            this.asset = asset;
        }
#endif // UNITY_EDITOR

        /// <inheritdoc cref="GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)"/>
        protected SceneReference(SerializationInfo info, StreamingContext context)
        {
            var deserializedGuid = info.GetString(CustomSerializationGuidKey);
            FillWithDeserializedGuid(deserializedGuid);
        }

        /// <summary>
        /// Creates a new <see cref="SceneReference"/> which references the scene at the given path.
        /// </summary>
        /// <param name="path">Path of the scene to reference.</param>
        /// <returns>A new <see cref="SceneReference"/>.</returns>
        /// <exception cref="SceneReferenceCreationException">Throws if the given path is null or whitespace.</exception>
        /// <exception cref="SceneReferenceCreationException">Throws if the given path is not found in the Scene Path to GUID map.</exception>
        public static SceneReference FromScenePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new SceneReferenceCreationException(
                    $"Given path is null or whitespace. Path: '{path}'" +
                    "\nTo fix this, make sure you provide the path of a valid scene.");
            }

            if (!SceneGuidToPathMapProvider.ScenePathToGuidMap.TryGetValue(path, out var guidFromMap))
            {
                throw new SceneReferenceCreationException(
                    $"Given path is not found in the scene GUID to path map. Path: '{path}'"
                    + "\nThis can happen for these reasons:"
                    + "\n1. The asset at the given path either doesn't exist or is not a scene. To fix this, make sure you provide the path of a valid scene."
                    + "\n2. The scene GUID to path map is outdated. To fix this, you can either manually run the generator, or enable generation triggers. It is highly recommended to keep all the generation triggers enabled.");
            }

            return new SceneReference(guidFromMap);
        }

        /// <summary>
        /// Creates a new <see cref="SceneReference"/> which references the scene with the given address.
        /// </summary>
        /// <param name="address">Address of the scene to reference.</param>
        /// <returns>A new <see cref="SceneReference"/>.</returns>
        /// <exception cref="SceneReferenceCreationException">Throws if the given address is null or whitespace.</exception>
        /// <exception cref="SceneReferenceCreationException">Throws if the given address is not found in the scene GUID to address map.</exception>
        /// <exception cref="SceneReferenceCreationException">Throws if the given address matches multiple entries from the scene GUID to address map.</exception>
        /// <exception cref="AddressablesSupportDisabledException">Throws if addressables support is disabled.</exception>
        public static SceneReference FromAddress(string address)
        {
#if ESR_ADDRESSABLES
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new SceneReferenceCreationException(
                    $"Given address is null or whitespace. Path: '{address}'" +
                    "\nTo fix this, make sure you provide the address of a valid addressable scene.");
            }

            try
            {
                var guidFromMap = SceneGuidToAddressMapProvider.GetGuidFromAddress(address);
                return new SceneReference(guidFromMap);
            }
            catch (AddressNotFoundException e)
            {
                throw new SceneReferenceCreationException(
                    $"Given address is not found in the Scene GUID to Address Map. Address: {address}." +
                    "\nThis can happen for these reasons:" +
                    "\n1. The asset with the given address either doesn't exist or is not a scene. To fix this, make sure you provide the address of a valid addressable scene." +
                    "\n2. The Scene GUID to Address Map is outdated. To fix this, you can either manually run the generator, or enable generation triggers. It is highly recommended to keep all the generation triggers enabled."
                    , e
                );
            }
            catch (AddressNotUniqueException e)
            {
                throw new SceneReferenceCreationException(
                    $"Given address matches multiple scenes in the Scene GUID to Address Map. Address: {address}." +
                    "\nThrown if a given address matches multiple entries in the Scene GUID to Address Map. This can happen for these reasons:" +
                    "\n1. There are multiple addressable scenes with the same given address. To fix this, make sure there is only one addressable scene with the given address." +
                    "\n2. The Scene GUID to Address Map is outdated. To fix this, you can either manually run the generator, or enable generation triggers. It is highly recommended to keep all the generation triggers enabled."
                    , e
                );
            }
            catch (AddressablesSupportDisabledException e)
            {
                // internal exceptions should not be documented as part of the public API
                throw SceneReferenceInternalException.ExceptionImpossible("48302749", e);
            }
#else // ESR_ADDRESSABLES
            throw new AddressablesSupportDisabledException();
#endif // ESR_ADDRESSABLES
        }

        /// <summary>
        /// Is this <see cref="SceneReference"/> assigned something?
        /// </summary>
        private bool HasValue
        {
            get
            {
                if (!Guid.IsValidGuid())
                {
                    // internal exceptions should not be documented as part of the public API
                    throw SceneReferenceInternalException.InvalidGuid("54783205", Guid);
                }

                return Guid != Utils.AllZeroGuid;
            }
        }

        /// <summary>
        /// GUID of the scene asset.
        /// </summary>
        public string Guid => guid.GuardGuidAgainstNullOrWhitespace();

        /// <summary>
        /// Path to the scene asset.
        /// </summary>
        /// <exception cref="EmptySceneReferenceException">Throws if nothing is assigned to this SceneReference.</exception>
        /// <exception cref="InvalidSceneReferenceException">Throws if the scene is not in the scene GUID to path map.</exception>
        public string Path
        {
            get
            {
                if (!HasValue)
                {
                    throw new EmptySceneReferenceException();
                }

                if (!SceneGuidToPathMapProvider.SceneGuidToPathMap.TryGetValue(Guid, out var pathFromMap))
                {
                    throw new InvalidSceneReferenceException();
                }

                return pathFromMap;
            }
        }

        /// <summary>
        /// Build index of the scene.
        /// </summary>
        /// <exception cref="EmptySceneReferenceException">Throws if nothing is assigned to this SceneReference.</exception>
        /// <exception cref="InvalidSceneReferenceException">Throws if the scene is not in the scene GUID to path map.</exception>
        /// <remarks>
        /// This property will return <c>-1</c> if the scene is not added and enabled in the build settings.
        /// </remarks>
        public int BuildIndex => SceneUtility.GetBuildIndexByScenePath(Path);

        /// <summary>
        /// Name of the scene asset. Without '.unity' extension.
        /// </summary>
        /// <exception cref="EmptySceneReferenceException">Throws if nothing is assigned to this SceneReference.</exception>
        /// <exception cref="InvalidSceneReferenceException">Throws if the scene is not in the scene GUID to path map.</exception>
        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);

        /// <summary>
        /// The <see cref="Scene"/> struct for this scene. Only valid if the scene is currently loaded.
        /// </summary>
        /// <exception cref="EmptySceneReferenceException">Throws if nothing is assigned to this SceneReference.</exception>
        /// <exception cref="InvalidSceneReferenceException">Throws if the scene is not in the scene GUID to path map.</exception>
        /// <remarks>
        /// You can check <see cref="Scene.IsValid"/> on the return value to see if it is valid.
        /// </remarks>
        public Scene LoadedScene => SceneManager.GetSceneByPath(Path);

        /// <summary>
        /// Address of the scene.
        /// </summary>
        /// <exception cref="EmptySceneReferenceException">Throws if nothing is assigned to this SceneReference.</exception>
        /// <exception cref="InvalidSceneReferenceException">Throws if the scene is not in the scene GUID to path map.</exception>
        /// <exception cref="SceneNotAddressableException">Throws if the scene is not in the scene GUID to address map.</exception>
        /// <exception cref="AddressablesSupportDisabledException">Throws if addressables support is disabled.</exception>
        public string Address
        {
            get
            {
#if ESR_ADDRESSABLES
                if (!HasValue)
                {
                    throw new EmptySceneReferenceException();
                }

                if (!SceneGuidToPathMapProvider.SceneGuidToPathMap.ContainsKey(Guid))
                {
                    throw new InvalidSceneReferenceException();
                }

                if (!SceneGuidToAddressMapProvider.SceneGuidToAddressMap.TryGetValue(Guid, out var addressFromMap))
                {
                    throw new SceneNotAddressableException();
                }

                return addressFromMap;
#else // ESR_ADDRESSABLES
                throw new AddressablesSupportDisabledException();
#endif // ESR_ADDRESSABLES
            }
        }

        /// <inheritdoc cref="SceneReferenceState"/>
        /// <seealso cref="UnsafeReason"/>
        public SceneReferenceState State
        {
            get
            {
                if (HasValue)
                {
                    if (SceneGuidToPathMapProvider.SceneGuidToPathMap.TryGetValue(Guid, out var path) && SceneUtility.GetBuildIndexByScenePath(path) != -1)
                    {
                        return SceneReferenceState.Regular;
                    }

#if ESR_ADDRESSABLES
                    if (SceneGuidToAddressMapProvider.SceneGuidToAddressMap.ContainsKey(Guid))
                    {
                        return SceneReferenceState.Addressable;
                    }
#endif // ESR_ADDRESSABLES
                }

                return SceneReferenceState.Unsafe;
            }
        }

        /// <inheritdoc cref="SceneReferenceUnsafeReason"/>
        /// <seealso cref="State"/>
        public SceneReferenceUnsafeReason UnsafeReason
        {
            get
            {
                if (!HasValue)
                {
                    return SceneReferenceUnsafeReason.Empty;
                }

#if ESR_ADDRESSABLES
                if (SceneGuidToAddressMapProvider.SceneGuidToAddressMap.TryGetValue(Guid, out var address))
                {
                    return SceneReferenceUnsafeReason.None;
                }
#endif

                if (!SceneGuidToPathMapProvider.SceneGuidToPathMap.TryGetValue(Guid, out var path))
                {
                    return SceneReferenceUnsafeReason.NotInMaps;
                }

                if (SceneUtility.GetBuildIndexByScenePath(path) == -1)
                {
                    return SceneReferenceUnsafeReason.NotInBuild;
                }

                return SceneReferenceUnsafeReason.None;
            }
        }

        /// <inheritdoc cref="GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)"/>
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            GetObjectData(info, context);
        }

        /// <summary>
        /// Used by <see cref="ISerializable"/> for custom JSON and Binary serialization support.
        /// </summary>
        /// <remarks>
        /// https://www.newtonsoft.com/json/help/html/serializationguide.htm#ISerializable
        /// </remarks>
        protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            var guidToSerialize = GetGuidToSerialize();
            info.AddValue(CustomSerializationGuidKey, guidToSerialize);
        }

        /// <inheritdoc cref="OnBeforeSerialize()"/>
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            OnBeforeSerialize();
        }

        /// <summary>
        /// Used by <see cref="ISerializationCallbackReceiver"/>.
        /// </summary>
        protected virtual void OnBeforeSerialize()
        {
            // Intentionally using guid field directly instead of the Guid property.
            guid = guid.GuardGuidAgainstNullOrWhitespace();
        }

        /// <inheritdoc cref="OnAfterDeserialize()"/>
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            OnAfterDeserialize();
        }

        /// <summary>
        /// Used by <see cref="ISerializationCallbackReceiver"/>.
        /// </summary>
        protected virtual void OnAfterDeserialize()
        {
            // Intentionally using guid field directly instead of the Guid property.
            guid = guid.GuardGuidAgainstNullOrWhitespace();
        }

        /// <inheritdoc cref="GetSchema()"/>
        XmlSchema IXmlSerializable.GetSchema()
        {
            return GetSchema();
        }

        /// <summary>
        /// Used by <see cref="IXmlSerializable"/> for custom XML serialization support.
        /// </summary>
        protected virtual XmlSchema GetSchema()
        {
            return null;
        }

        /// <inheritdoc cref="ReadXml(System.Xml.XmlReader)"/>
        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            ReadXml(reader);
        }

        /// <summary>
        /// Used by <see cref="IXmlSerializable"/> for custom XML serialization support.
        /// </summary>
        protected virtual void ReadXml(XmlReader reader)
        {
            var deserializedGuid = reader.ReadString();
            FillWithDeserializedGuid(deserializedGuid);
        }

        /// <inheritdoc cref="WriteXml(System.Xml.XmlWriter)"/>
        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            WriteXml(writer);
        }

        /// <summary>
        /// Used by <see cref="IXmlSerializable"/> for custom XML serialization support.
        /// </summary>
        protected virtual void WriteXml(XmlWriter writer)
        {
            var guidToSerialize = GetGuidToSerialize();
            writer.WriteString(guidToSerialize);
        }

        private string GetGuidToSerialize() =>
            guid.GuardGuidAgainstNullOrWhitespace();

        private void FillWithDeserializedGuid(string deserializedGuid)
        {
            deserializedGuid = deserializedGuid.GuardGuidAgainstNullOrWhitespace();

            // Intentionally using guid field directly instead of the Guid property.
            guid = deserializedGuid;

#if UNITY_EDITOR
            asset = SceneGuidToPathMapProvider.SceneGuidToPathMap.TryGetValue(deserializedGuid, out var pathFromMap)
                ? AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(pathFromMap)
                : null;
#endif // UNITY_EDITOR
        }
    }
}
