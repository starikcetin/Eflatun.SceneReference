using System.Collections.Generic;
using System.Linq;
using Eflatun.SceneReference.Exceptions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Scripting;
using Eflatun.SceneReference.Utility;
using Newtonsoft.Json;

namespace Eflatun.SceneReference
{
    /// <summary>
    /// Provides the scene GUID to address map. Can be used in both editor and runtime.
    /// </summary>
    /// <remarks>
    /// This map is only relevant if addressables support is enabled. It will be empty if addressables support is disabled.<p/>
    /// Unlike <see cref="SceneGuidToPathMapProvider"/>, this class can not provide an inverse map because address
    /// of an asset does not have to be unique. Instead, it provides <see cref="GetGuidFromAddress"/> and
    /// <see cref="TryGetGuidFromAddress"/> methods.
    /// </remarks>
    [PublicAPI]
    public static class SceneGuidToAddressMapProvider
    {
        private static Dictionary<string, string> _sceneGuidToAddressMap;

        /// <summary>
        /// The scene GUID to address map.
        /// </summary>
        public static IReadOnlyDictionary<string, string> SceneGuidToAddressMap
        {
            get
            {
                LoadIfNotAlready();
                return _sceneGuidToAddressMap;
            }
        }

        /// <summary>
        /// Gets the GUID of the scene with the given address.
        /// </summary>
        /// <param name="address">Address of the scene.</param>
        /// <returns>GUID of the scene.</returns>
        /// <exception cref="AddressNotFoundException">Thrown if no scene with the given address is found in the map.</exception>
        /// <exception cref="AddressNotUniqueException">Thrown if multiple scenes found with the given address in the map.</exception>
        /// <exception cref="AddressablesSupportDisabledException">Thrown if addressables support is disabled.</exception>
        public static string GetGuidFromAddress(string address)
        {
#if ESR_ADDRESSABLES
            LoadIfNotAlready();

            var matchingEntries = _sceneGuidToAddressMap.Where(x => x.Value == address).ToArray();

            if (matchingEntries.Length < 1)
            {
                throw new AddressNotFoundException(address);
            }

            if (matchingEntries.Length > 1)
            {
                throw new AddressNotUniqueException(address);
            }

            return matchingEntries.First().Key;
#else // ESR_ADDRESSABLES
            throw new AddressablesSupportDisabledException();
#endif // ESR_ADDRESSABLES
        }

        /// <summary>
        /// Gets the GUID of the scene with the given address.
        /// </summary>
        /// <param name="address">Address of the scene.</param>
        /// <param name="guid">GUID of the scene if the return is <c>true</c>; <c>null</c> otherwise.</param>
        /// <returns><c>true</c> if there is exactly one scene with the given address in the map; <c>false</c> otherwise.</returns>
        [ContractAnnotation("=> true, guid:notnull; => false, guid:null")]
        public static bool TryGetGuidFromAddress(string address, out string guid)
        {
            try
            {
                guid = GetGuidFromAddress(address);
                return true;
            }
            catch
            {
                guid = null;
                return false;
            }
        }

        /// <summary>
        /// IMPORTANT: This method does NOT check if addressables support is enabled or not! It will assign no matter what.
        /// </summary>
        internal static void DirectAssign(Dictionary<string, string> sceneGuidToAddressMap)
        {
            FillWith(sceneGuidToAddressMap);
        }

        [Preserve]
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void LoadIfNotAlready()
        {
            if (_sceneGuidToAddressMap == null)
            {
                Load();
            }
        }

        private static void Load()
        {
#if ESR_ADDRESSABLES
            var genFilePath = Paths.RelativeToResources.SceneGuidToAddressMapFile.UnixPath.WithoutExtension();
            var genFile = Resources.Load<TextAsset>(genFilePath);

            if (genFile == null)
            {
                Logger.Error("Scene GUID to address map file not found!");
                return;
            }

            var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(genFile.text);
            FillWith(deserialized);
#else // ESR_ADDRESSABLES
            FillWith(new Dictionary<string, string>());
#endif // ESR_ADDRESSABLES
        }

        private static void FillWith(Dictionary<string, string> sceneGuidToAddressMap)
        {
            _sceneGuidToAddressMap = sceneGuidToAddressMap;
        }
    }
}
