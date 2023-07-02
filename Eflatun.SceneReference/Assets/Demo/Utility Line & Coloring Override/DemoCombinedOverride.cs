using UnityEngine;

namespace Eflatun.SceneReference.Demo
{
    public class DemoCombinedOverride : MonoBehaviour
    {
        [SerializeField] private SceneReference noOptions;

        [Space]
        [SceneReferenceOptions(Coloring = ColoringBehaviour.DoNotOverride, Toolbox = ToolboxBehaviour.DoNotOverride)]
        [SerializeField] private SceneReference doNotOverride;

        [Space]
        [SceneReferenceOptions(Coloring = ColoringBehaviour.Disabled, Toolbox = ToolboxBehaviour.Disabled)]
        [SerializeField] private SceneReference bothDisabled;

        [Space]
        [SceneReferenceOptions(Coloring = ColoringBehaviour.Enabled, Toolbox = ToolboxBehaviour.Enabled)]
        [SerializeField] private SceneReference bothEnabled;

        [Space]
        [SceneReferenceOptions(Coloring = ColoringBehaviour.Enabled, Toolbox = ToolboxBehaviour.Disabled)]
        [SerializeField] private SceneReference onlyColor;

        [Space]
        [SceneReferenceOptions(Coloring = ColoringBehaviour.Disabled, Toolbox = ToolboxBehaviour.Enabled)]
        [SerializeField] private SceneReference onlyUtility;
    }
}
