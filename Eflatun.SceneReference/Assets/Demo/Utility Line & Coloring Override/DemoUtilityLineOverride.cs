using UnityEngine;

namespace Eflatun.SceneReference.Demo.UtilityLineAndColoringOverride
{
    public class DemoUtilityLineOverride : MonoBehaviour
    {
        [SerializeField] private SceneReference noOptions;

        [Space]
        [SceneReferenceOptions(Toolbox = ToolboxBehaviour.DoNotOverride)]
        [SerializeField] private SceneReference doNotOverride;

        [Space]
        [SceneReferenceOptions(Toolbox = ToolboxBehaviour.Disabled)]
        [SerializeField] private SceneReference disabled;

        [Space]
        [SceneReferenceOptions(Toolbox = ToolboxBehaviour.Enabled)]
        [SerializeField] private new SceneReference enabled;
    }
}
