using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

// Make internals visible to the Editor assembly
[assembly: InternalsVisibleTo("Eflatun.SceneReference.Editor")]

// Always link to make sure RuntimeInitializeOnLoadMethod methods don't get stripped away
[assembly: AlwaysLinkAssembly]
