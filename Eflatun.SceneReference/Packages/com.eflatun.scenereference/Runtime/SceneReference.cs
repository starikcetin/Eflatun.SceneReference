using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Eflatun.SceneReference.Utility;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [XmlRoot("Eflatun.SceneReference.SceneReference")]
    public class SceneReference : ISerializationCallbackReceiver, ISerializable, IXmlSerializable
    {
        /// <summary>
        /// Creates a new <see cref="SceneReference"/> which references the scene at the given path.
        /// </summary>
        /// <param name="scenePath">Path of the scene to reference.</param>
        /// <returns>A new <see cref="SceneReference"/>.</returns>
        /// <exception cref="SceneReferenceCreationException">Throws if the given path is null or whitespace.</exception>
        /// <exception cref="SceneReferenceCreationException">Throws if the given path is not found in the Scene Path to GUID map.</exception>
        public static SceneReference FromScenePath(string scenePath)
        {
            if (string.IsNullOrWhiteSpace(scenePath))
            {
                throw new SceneReferenceCreationException(
                    $"Given path is null or whitespace. Path: '{scenePath}'" +
                    "\nTo fix this, make sure you provide the path of a valid scene.");
            }

            if (!SceneGuidToPathMapProvider.ScenePathToGuidMap.TryGetValue(scenePath, out var sceneGuid))
            {
                throw new SceneReferenceCreationException(
                    $"Given path is not found in the scene GUID to path map. Path: '{scenePath}'"
                    + "\nThis can happen for these reasons:"
                    + "\n1. The asset at the given path either doesn't exist or is not a scene. To fix this, make sure you provide the path of a valid scene."
                    + "\n2. The scene GUID to path map is outdated. To fix this, you can either manually run the generator, or enable generation triggers. It is highly recommended to keep all the generation triggers enabled.");
            }

            return new SceneReference(sceneGuid);
        }

        // GUID hex of an invalid asset contains all zeros. A GUID hex has 32 chars.
        private const string AllZeroGuidHex = "00000000000000000000000000000000";

        [SerializeField] internal UnityEngine.Object sceneAsset;
        [SerializeField] internal string sceneAssetGuidHex;

        /// <summary>
        /// Creates a new empty <see cref="SceneReference"/>.
        /// </summary>
        /// <remarks>This constructor never throws.</remarks>
        public SceneReference()
        {
            // This parameterless constructor is required for the XML serialization.
            // See: https://learn.microsoft.com/en-us/dotnet/api/system.xml.serialization.ixmlserializable?view=net-7.0#remarks

            sceneAssetGuidHex = AllZeroGuidHex;
            sceneAsset = null;
        }

        /// <summary>
        /// Creates a new <see cref="SceneReference"/> which references the scene that has the given GUID.
        /// </summary>
        /// <param name="sceneAssetGuidHex">GUID of the scene to reference.</param>
        /// <exception cref="SceneReferenceCreationException">Throws if the given GUID is null or empty.</exception>
        /// <exception cref="SceneReferenceCreationException">Throws if the given GUID is not found in the Scene GUID to Path map.</exception>
        /// <exception cref="SceneReferenceCreationException">(Editor-only) Throws if the asset is not found at the path that the GUID corresponds to.</exception>
        public SceneReference(string sceneAssetGuidHex)
        {
            if (string.IsNullOrWhiteSpace(sceneAssetGuidHex))
            {
                throw new SceneReferenceCreationException(
                    $"Given GUID is null or whitespace. GUID: '{sceneAssetGuidHex}'." +
                    "\nTo fix this, make sure you provide the GUID of a valid scene.");
            }

            if (!SceneGuidToPathMapProvider.SceneGuidToPathMap.TryGetValue(sceneAssetGuidHex, out var scenePath))
            {
                throw new SceneReferenceCreationException(
                    $"Given GUID is not found in the scene GUID to path map. GUID: '{sceneAssetGuidHex}'"
                    + "\nThis can happen for these reasons:"
                    + "\n1. The asset with the given GUID either doesn't exist or is not a scene. To fix this, make sure you provide the GUID of a valid scene."
                    + "\n2. The scene GUID to path map is outdated. To fix this, you can either manually run the generator, or enable generation triggers. It is highly recommended to keep all the generation triggers enabled.");
            }

            this.sceneAssetGuidHex = sceneAssetGuidHex;

#if UNITY_EDITOR
            var foundAsset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(scenePath);

            if (!foundAsset)
            {
                throw new SceneReferenceCreationException(
                    $"The given GUID was found in the map, but the scene asset at the corresponding path could not be loaded. Path: '{scenePath}'."
                    + "\nThis can happen due to an outdated scene GUID to path map retaining scene assets that no longer exist. To fix this, you can either manually run the generator, or enable generation triggers. It is highly recommended to keep all the generation triggers enabled.");
            }

            sceneAsset = foundAsset;
#endif // UNITY_EDITOR
        }

#if UNITY_EDITOR
        /// <summary>
        /// Creates a new <see cref="SceneReference"/> which references the given scene asset.
        /// </summary>
        /// <param name="sceneAsset">The asset of the scene to reference.</param>
        /// <exception cref="SceneReferenceCreationException">Throws if the given asset is null.</exception>
        /// <exception cref="SceneReferenceCreationException">Throws if the GUID of the given asset cannot be retrieved.</exception>
        /// <exception cref="SceneReferenceCreationException">Throws if the Scene GUID to Path map does not contain the GUID of the given asset.</exception>
        /// <remarks>This constructor is for editor-use only. Do NOT use it in runtime code.</remarks>
        public SceneReference(UnityEngine.Object sceneAsset)
        {
            if (!sceneAsset)
            {
                throw new SceneReferenceCreationException("Given scene asset is null. To fix this, make sure you provide a valid scene asset.");
            }

            if (!AssetDatabase.TryGetGUIDAndLocalFileIdentifier(sceneAsset, out var guid, out long _))
            {
                throw new SceneReferenceCreationException("Could not retrieve the GUID of the given scene asset. This usually indicates an invalid asset. To fix this, make sure you provide a valid scene asset.");
            }

            if (!SceneGuidToPathMapProvider.SceneGuidToPathMap.ContainsKey(guid))
            {
                throw new SceneReferenceCreationException(
                    $"The GUID of the given scene asset is not found in the scene GUID to path map. GUID: '{guid}'"
                    + "\nThis can happen for these reasons:"
                    + "\n1. Given asset either doesn't exist or is not a scene. To fix this, make sure you provide a valid scene asset."
                    + "\n2. The scene GUID to path map is outdated. To fix this, you can either manually run the generator, or enable generation triggers. It is highly recommended to keep all the generation triggers enabled.");
            }

            sceneAssetGuidHex = guid;
            this.sceneAsset = sceneAsset;
        }
#endif // UNITY_EDITOR

        /// <summary>
        /// Used by <see cref="ISerializable"/> for custom JSON and Binary serialization support.
        /// </summary>
        protected SceneReference(SerializationInfo info, StreamingContext context)
        {
            var deserializedGuid = info.GetString("sceneAssetGuidHex");
            sceneAssetGuidHex = deserializedGuid;

#if UNITY_EDITOR
            if (SceneGuidToPathMapProvider.SceneGuidToPathMap.TryGetValue(deserializedGuid, out var scenePath))
            {
                sceneAsset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(scenePath);
            }
#endif // UNITY_EDITOR
        }

        /// <summary>
        /// GUID of the scene asset in hex format.
        /// </summary>
        public string AssetGuidHex
        {
            get
            {
                // For some reason, the field initializer for sceneAssetGuidHex is working inconsistently. Therefore,
                // we sometimes get empty strings instead of all zero hex. This condition is here to safeguard against
                // that situation.
                // TODO: There can be a deeper problem causing the issue described above, needs further investigation.
                if (string.IsNullOrWhiteSpace(sceneAssetGuidHex))
                {
                    return AllZeroGuidHex;
                }

                return sceneAssetGuidHex;
            }
        }

        /// <summary>
        /// Path to the scene asset.
        /// </summary>
        /// <exception cref="EmptySceneReferenceException">Throws if <see cref="HasValue"/> is <c>false</c>.</exception>
        /// <exception cref="InvalidSceneReferenceException">Throws if <see cref="IsInSceneGuidToPathMap"/> is <c>false</c>.</exception>
        public string Path
        {
            get
            {
                if (!HasValue)
                {
                    throw new EmptySceneReferenceException();
                }

                if (!SceneGuidToPathMapProvider.SceneGuidToPathMap.TryGetValue(AssetGuidHex, out var scenePath))
                {
                    throw new InvalidSceneReferenceException();
                }

                return scenePath;
            }
        }

        /// <summary>
        /// Build index of the scene.
        /// </summary>
        /// <exception cref="EmptySceneReferenceException">Throws if <see cref="HasValue"/> is <c>false</c>.</exception>
        /// <exception cref="InvalidSceneReferenceException">Throws if <see cref="IsInSceneGuidToPathMap"/> is <c>false</c>.</exception>
        /// <remarks>
        /// This property will return <c>-1</c> if <see cref="IsInBuildAndEnabled"/> is <c>false</c>.
        /// </remarks>
        public int BuildIndex => SceneUtility.GetBuildIndexByScenePath(Path);

        /// <summary>
        /// Name of the scene asset. Without '.unity' extension.
        /// </summary>
        /// <exception cref="EmptySceneReferenceException">Throws if <see cref="HasValue"/> is <c>false</c>.</exception>
        /// <exception cref="InvalidSceneReferenceException">Throws if <see cref="IsInSceneGuidToPathMap"/> is <c>false</c>.</exception>
        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);

        /// <summary>
        /// The <see cref="Scene"/> struct for this scene. Only valid if the scene is currently loaded.
        /// </summary>
        /// <exception cref="EmptySceneReferenceException">Throws if <see cref="HasValue"/> is <c>false</c>.</exception>
        /// <exception cref="InvalidSceneReferenceException">Throws if <see cref="IsInSceneGuidToPathMap"/> is <c>false</c>.</exception>
        /// <remarks>
        /// You can check <see cref="Scene.IsValid"/> to see if the value of this property is valid.<p/>
        /// If <see cref="IsInBuildAndEnabled"/> is <c>false</c>, the scene can never be loaded, and therefore this property can never have a valid value.
        /// </remarks>
        public Scene LoadedScene => SceneManager.GetSceneByPath(Path);

        /// <summary>
        /// Is this <see cref="SceneReference"/> assigned something?
        /// </summary>
        /// <remarks>
        /// Only check this property if you need partial validations, as this property alone does not communicate whether this <see cref="SceneReference"/> is absolutely safe to use.<p/>
        /// If you only need to check if it is completely safe to use a <see cref="SceneReference"/> without knowing where exactly the problem is, then only check <see cref="IsSafeToUse"/> instead. Checking only <see cref="IsSafeToUse"/> is sufficient for the majority of the use cases.
        /// </remarks>
        /// <seealso cref="IsInSceneGuidToPathMap"/>
        /// <seealso cref="IsInBuildAndEnabled"/>
        /// <seealso cref="IsSafeToUse"/>
        public bool HasValue
        {
            get
            {
                if (!AssetGuidHex.IsValidGuidHex())
                {
                    // internal exceptions should not be documented as part of the public API
                    throw SceneReferenceInternalException.InvalidAssetGuidHex("54783205", AssetGuidHex);
                }

                return AssetGuidHex != AllZeroGuidHex;
            }
        }

        /// <summary>
        /// Does the scene GUID to path map contain the scene?
        /// </summary>
        /// <exception cref="EmptySceneReferenceException">Throws if <see cref="HasValue"/> is <c>false</c>.</exception>
        /// <remarks>
        /// Only check this property if you need partial validations, as this property alone does not communicate whether this <see cref="SceneReference"/> is absolutely safe to use.<p/>
        /// If you only need to check if it is completely safe to use a <see cref="SceneReference"/> without knowing where exactly the problem is, then only check <see cref="IsSafeToUse"/> instead. Checking only <see cref="IsSafeToUse"/> is sufficient for the majority of the use cases.
        /// </remarks>
        /// <seealso cref="HasValue"/>
        /// <seealso cref="IsInBuildAndEnabled"/>
        /// <seealso cref="IsSafeToUse"/>
        public bool IsInSceneGuidToPathMap
        {
            get
            {
                if (!HasValue)
                {
                    throw new EmptySceneReferenceException();
                }

                return SceneGuidToPathMapProvider.SceneGuidToPathMap.ContainsKey(AssetGuidHex);
            }
        }

        /// <summary>
        /// Is the scene added and enabled in Build Settings?
        /// </summary>
        /// <exception cref="EmptySceneReferenceException">Throws if <see cref="HasValue"/> is <c>false</c>.</exception>
        /// <exception cref="InvalidSceneReferenceException">Throws if <see cref="IsInSceneGuidToPathMap"/> is <c>false</c>.</exception>
        /// <remarks>
        /// Only check this property if you need partial validations, as this property alone does not communicate whether this <see cref="SceneReference"/> is absolutely safe to use.<p/>
        /// If you only need to check if it is completely safe to use a <see cref="SceneReference"/> without knowing where exactly the problem is, then only check <see cref="IsSafeToUse"/> instead. Checking only <see cref="IsSafeToUse"/> is sufficient for the majority of the use cases.
        /// </remarks>
        /// <seealso cref="HasValue"/>
        /// <seealso cref="IsInSceneGuidToPathMap"/>
        /// <seealso cref="IsSafeToUse"/>
        public bool IsInBuildAndEnabled => BuildIndex != -1;

        /// <summary>
        /// Is this <see cref="SceneReference"/> safe to use?
        /// </summary>
        /// <remarks>
        /// Checking this property alone is sufficient for the majority of the validation use cases, as this property absolutely communicates whether this <see cref="SceneReference"/> is safe to use.<p/>
        /// Checking this property is equivalent to checking all partial validation properties (namely: <see cref="HasValue"/>, <see cref="IsInSceneGuidToPathMap"/>, and <see cref="IsInBuildAndEnabled"/>) in the correct order, but it provides a slightly better performance. If you need those validations partially, you can check them instead of this property. Keep in mind that the use cases that require partial validation are rare and few.
        /// </remarks>
        /// <seealso cref="HasValue"/>
        /// <seealso cref="IsInSceneGuidToPathMap"/>
        /// <seealso cref="IsInBuildAndEnabled"/>
        public bool IsSafeToUse =>
            HasValue
            && SceneGuidToPathMapProvider.SceneGuidToPathMap.TryGetValue(AssetGuidHex, out var path)
            && SceneUtility.GetBuildIndexByScenePath(path) != -1;

        /// <inheritdoc cref="GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)"/>
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            GetObjectData(info, context);
        }

        /// <summary>
        /// Used by <see cref="ISerializable"/> for custom JSON and Binary serialization support.
        /// </summary>
        protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Intentionally using sceneAssetGuidHex field directly instead of the AssetGuidHex property.
            info.AddValue("sceneAssetGuidHex", sceneAssetGuidHex);
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
            if (string.IsNullOrEmpty(sceneAssetGuidHex))
            {
                sceneAssetGuidHex = AllZeroGuidHex;
            }
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
            if (string.IsNullOrEmpty(sceneAssetGuidHex))
            {
                sceneAssetGuidHex = AllZeroGuidHex;
            }
        }

        /// <inheritdoc cref="GetSchema()"/>
        XmlSchema IXmlSerializable.GetSchema()
        {
            return GetSchema();
        }

        /// <summary>
        /// Used by <see cref="IXmlSerializable"/> for custom XML serialization support..
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
            // Intentionally using sceneAssetGuidHex field directly instead of the AssetGuidHex property.
            sceneAssetGuidHex = reader.ReadString();

#if UNITY_EDITOR
            sceneAsset = SceneGuidToPathMapProvider.SceneGuidToPathMap.TryGetValue(sceneAssetGuidHex, out var scenePath)
                ? AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(scenePath)
                : null;
#endif // UNITY_EDITOR
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
            // Intentionally using sceneAssetGuidHex field directly instead of the AssetGuidHex property.
            writer.WriteString(sceneAssetGuidHex);
        }
    }
}
