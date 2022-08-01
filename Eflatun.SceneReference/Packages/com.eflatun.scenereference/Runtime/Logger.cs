namespace Eflatun.SceneReference
{
    internal static class Logger
    {
        internal static void Debug(string msg) => UnityEngine.Debug.Log($"{Constants.LogPrefixBase} {msg}");
        internal static void Warn(string msg) => UnityEngine.Debug.LogWarning($"{Constants.LogPrefixBase} {msg}");
        internal static void Error(string msg) => UnityEngine.Debug.LogError($"{Constants.LogPrefixBase} {msg}");
    }
}
