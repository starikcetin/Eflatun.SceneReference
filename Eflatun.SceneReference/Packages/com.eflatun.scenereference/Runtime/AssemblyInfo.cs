using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

[assembly: InternalsVisibleTo("Eflatun.SceneReference.Editor")]
[assembly: InternalsVisibleTo("Eflatun.SceneReference.Tests.Runtime")]
[assembly: InternalsVisibleTo("Eflatun.SceneReference.DevelopmentUtils.Editor")]

// Always link to make sure RuntimeInitializeOnLoadMethod methods don't get stripped away
[assembly: AlwaysLinkAssembly]
