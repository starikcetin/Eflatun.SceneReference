using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Scripting;
using Eflatun.SceneReference.Utility;
using Newtonsoft.Json;

namespace Eflatun.SceneReference
{
    /// <summary>
    /// Provides the scene GUID to path map. Can be used in both editor and runtime.
    /// </summary>
    /// <remarks>
    /// Unlike <see cref="SceneGuidToPathMapProvider"/>, this class can not provide an inverse map because address
    /// of an asset does not have to be unique. Instead, it provides <see cref="GetGuidFromAddress"/> and
    /// <see cref="TryGetGuidFromAddress"/> methods.
    /// </remarks>
    [PublicAPI]
    public static class AddressableSceneGuidToAddressMapProvider
    {
        private static Dictionary<string, string> _addressableSceneGuidToAddressMap;

        /// <summary>
        /// The scene GUID to address map for addressable scenes.
        /// </summary>
        public static IReadOnlyDictionary<string, string> AddressableSceneGuidToAddressMap
        {
            get
            {
                LoadIfNotAlready();
                return _addressableSceneGuidToAddressMap;
            }
        }

        /// <summary>
        /// TODO: docs
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static string GetGuidFromAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                // TODO: throw better exception
                throw new Exception();
            }

            LoadIfNotAlready();

            var matchingEntries = _addressableSceneGuidToAddressMap.Where(x => x.Value == address).ToArray();

            if (matchingEntries.Length < 1)
            {
                // TODO: throw better exception
                throw new Exception();
            }

            if (matchingEntries.Length > 1)
            {
                // TODO: throw better exception
                throw new Exception();
            }

            return matchingEntries.First().Key;
        }

        /// <summary>
        /// TODO: docs
        /// </summary>
        /// <param name="address"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
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

        internal static void DirectAssign(Dictionary<string, string> addressableSceneGuidToAddress)
        {
            FillWith(addressableSceneGuidToAddress);
        }

        [Preserve]
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void LoadIfNotAlready()
        {
            if (_addressableSceneGuidToAddressMap == null)
            {
                Load();
            }
        }

        private static void Load()
        {
#if EFLATUN_SCENEREFERENCE_ADDRESSABLES_PACKAGE_PRESENT
            var genFilePath = Paths.RelativeToResources.AddressableSceneGuidToAddressMapFile.UnixPath.WithoutExtension();
            var genFile = Resources.Load<TextAsset>(genFilePath);

            if (genFile == null)
            {
                Logger.Error("Addressable scene GUID to address map file not found!");
                return;
            }

            var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(genFile.text);
            FillWith(deserialized);
#else // EFLATUN_SCENEREFERENCE_ADDRESSABLES_PACKAGE_PRESENT
            FillWith(new Dictionary<string, string>());
#endif // EFLATUN_SCENEREFERENCE_ADDRESSABLES_PACKAGE_PRESENT
        }

        private static void FillWith(Dictionary<string, string> addressableSceneGuidToAddress)
        {
            _addressableSceneGuidToAddressMap = addressableSceneGuidToAddress;
        }
    }
}
