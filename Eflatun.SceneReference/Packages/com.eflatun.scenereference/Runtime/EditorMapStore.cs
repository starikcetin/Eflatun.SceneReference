#if UNITY_EDITOR
using JetBrains.Annotations;
using UnityEditor;

namespace Eflatun.SceneReference
{
    internal static class EditorMapStore
    {
        private static readonly string KeyPrefix = $"Eflatun_SceneReference_";

        public static readonly string SceneGuidToPathMapJson_Key = $"{KeyPrefix}SceneGuidToPathMapJson";

        [CanBeNull]
        public static string SceneGuidToPathMapJson
        {
            get => EditorUserSettings.GetConfigValue(SceneGuidToPathMapJson_Key);
            set => EditorUserSettings.SetConfigValue(SceneGuidToPathMapJson_Key, value);
        }

        public static readonly string SceneGuidToAddressMapJson_Key = $"{KeyPrefix}SceneGuidToAddressMapJson";

        [CanBeNull]
        public static string SceneGuidToAddressMapJson
        {
            get => EditorUserSettings.GetConfigValue(SceneGuidToAddressMapJson_Key);
            set => EditorUserSettings.SetConfigValue(SceneGuidToAddressMapJson_Key, value);
        }
    }
}
#endif // UNITY_EDITOR
