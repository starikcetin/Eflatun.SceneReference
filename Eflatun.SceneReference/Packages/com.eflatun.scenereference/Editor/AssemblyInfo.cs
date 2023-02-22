using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

// Make internals visible to the Test assembly
[assembly: InternalsVisibleTo("Eflatun.SceneReference.Tests")]

// Always link to make sure InitializeOnLoad methods don't get stripped away
[assembly: AlwaysLinkAssembly]
