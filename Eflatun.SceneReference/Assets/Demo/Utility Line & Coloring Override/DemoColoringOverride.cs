using UnityEngine;

namespace Eflatun.SceneReference.Demo.UtilityLineAndColoringOverride
{
    public class DemoColoringOverride : MonoBehaviour
    {
        [SerializeField] private SceneReference noOptions;

        [Space]
        [SceneReferenceOptions(SceneInBuildColoring = ColoringBehaviour.DoNotOverride)]
        [SerializeField] private SceneReference doNotOverride;

        [Space]
        [SceneReferenceOptions(SceneInBuildColoring = ColoringBehaviour.Disabled)]
        [SerializeField] private SceneReference disabled;

        [Space]
        [SceneReferenceOptions(SceneInBuildColoring = ColoringBehaviour.Enabled)]
        [SerializeField] private new SceneReference enabled;

        // You can use properties too:
        // [field: Space]
        // [field: SceneReferenceOptions(SceneInBuildColoring = ColoringBehaviour.Disabled)]
        // [field: SerializeField]
        // public SceneReference DisabledProperty { get; private set; }
    }
}
