using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

// Make internals visible to the Editor assembly
[assembly: InternalsVisibleTo("Eflatun.SceneReference.Editor")]

// Make internals visible to the runtime tests assembly
[assembly: InternalsVisibleTo("Eflatun.SceneReference.Tests.Runtime")]

// Always link to make sure RuntimeInitializeOnLoadMethod methods don't get stripped away
[assembly: AlwaysLinkAssembly]
