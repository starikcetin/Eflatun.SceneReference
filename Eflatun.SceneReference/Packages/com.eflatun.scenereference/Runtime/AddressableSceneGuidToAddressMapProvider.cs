using System.Collections.Generic;
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
