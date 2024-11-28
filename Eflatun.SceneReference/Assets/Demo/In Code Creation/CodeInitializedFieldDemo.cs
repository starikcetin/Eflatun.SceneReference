using System.Reflection;
using UnityEngine;

namespace Eflatun.SceneReference.Demo.InCodeCreation
{
    public class CodeInitializedFieldDemo : MonoBehaviour
    {
        [SerializeField] private SceneReference sceneCSource;

        // Assets/Scenes/SceneA.unity
        [Space]
        [Header("Press start and watch sceneA, sceneB, and sceneC get filled in.")]
        [SerializeField] private SceneReference sceneA;

        // Assets/Scenes/SceneB.unity
        [SerializeField] private SceneReference sceneB;

        // Assets/Scenes Folder 2/SceneC.unity
        [SerializeField] private SceneReference sceneC;

        private void Start()
        {
            sceneA = new SceneReference("1d6d932bbdf133c41aa34e00b02bdd1d");
            sceneB = SceneReference.FromScenePath("Assets/Demo/Demo Utils/Subject Scenes/Enabled In Build.unity");

#if UNITY_EDITOR
            sceneC = new SceneReference(GetAsset(sceneCSource));
#endif // UNITY_EDITOR
        }

        private static UnityEngine.Object GetAsset(SceneReference x)
        {
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            return (UnityEngine.Object) typeof(SceneReference).GetField("asset", bindingFlags)?.GetValue(x);
        }
    }
}
