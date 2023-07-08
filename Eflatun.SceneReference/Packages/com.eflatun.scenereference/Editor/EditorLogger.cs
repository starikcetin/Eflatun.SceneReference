namespace Eflatun.SceneReference.Editor
{
    /// <summary>
    /// Logger for editor code.
    /// </summary>
    internal static class EditorLogger
    {
        public static void Debug(string msg)
        {
            if (SettingsManager.Logging.EditorLogLevel.value <= LogLevel.Debug)
            {
                UnityEngine.Debug.Log($"{Constants.LogPrefixBase} {msg}");
            }
        }

        public static void Warn(string msg)
        {
            if (SettingsManager.Logging.EditorLogLevel.value <= LogLevel.Warning)
            {
                UnityEngine.Debug.LogWarning($"{Constants.LogPrefixBase} {msg}");
            }
        }

        public static void Error(string msg)
        {
            if (SettingsManager.Logging.EditorLogLevel.value <= LogLevel.Error)
            {
                UnityEngine.Debug.LogError($"{Constants.LogPrefixBase} {msg}");
            }
        }
    }
}
