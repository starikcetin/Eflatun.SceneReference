using Eflatun.SceneReference;
using UnityEngine;

public class DemoColoringOverride : MonoBehaviour
{
    [SerializeField] private SceneReference noOptions;

    [Space]
    [SceneReferenceOptions(Coloring = ColoringBehaviour.DoNotOverride)]
    [SerializeField] private SceneReference doNotOverride;

    [Space]
    [SceneReferenceOptions(Coloring = ColoringBehaviour.Disabled)]
    [SerializeField] private SceneReference disabled;

    [Space]
    [SceneReferenceOptions(Coloring = ColoringBehaviour.Enabled)]
    [SerializeField] private SceneReference enabled;

    // You can use properties too:
    // [field: Space]
    // [field: SceneReferenceOptions(Coloring = ColoringBehaviour.Disabled)]
    // [field: SerializeField]
    // public SceneReference DisabledProperty { get; private set; }
}
