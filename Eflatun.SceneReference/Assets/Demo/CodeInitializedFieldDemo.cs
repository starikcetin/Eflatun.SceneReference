using System.Reflection;
using Eflatun.SceneReference;
using UnityEngine;

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
        sceneA = new SceneReference("45ac7250dffb26f4e84eda01a52b8b19");
        sceneB = SceneReference.FromScenePath("Assets/Scenes/SceneB.unity");

#if UNITY_EDITOR
        sceneC = new SceneReference(GetSceneAsset(sceneCSource));
#endif // UNITY_EDITOR
    }

    private static UnityEngine.Object GetSceneAsset(SceneReference x)
    {
        const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        return (UnityEngine.Object) typeof(SceneReference).GetField("sceneAsset", bindingFlags)?.GetValue(x);
    }
}
