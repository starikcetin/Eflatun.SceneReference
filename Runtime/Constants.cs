namespace Eflatun.SceneReference
{
    internal static class Constants
    {
        internal const string MenuPrefixBase = "Eflatun/Scene Reference";
        internal const string PackageNameReverseDomain = "com.eflatun.scenereference";

        private const string LogColor = "#bf63fd";
        private const string PlaintextLogPrefixBase = "[Eflatun.SceneReference]";
        internal const string LogPrefixBase =
#if UNITY_EDITOR
            "<b><color=" + LogColor + ">" + PlaintextLogPrefixBase + "</color></b>";
#else // UNITY_EDITOR
            PlaintextLogPrefixBase;
#endif // UNITY_EDITOR
    }
}
