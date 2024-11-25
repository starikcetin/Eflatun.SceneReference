using UnityEngine;

namespace Eflatun.SceneReference.Demo.UtilityLineAndColoringOverride
{
    public class DemoCombinedOverride : MonoBehaviour
    {
        [SerializeField] private SceneReference noOptions;

        [Space]
        [SceneReferenceOptions(SceneInBuildColoring = ColoringBehaviour.DoNotOverride, Toolbox = ToolboxBehaviour.DoNotOverride)]
        [SerializeField] private SceneReference doNotOverride;

        [Space]
        [SceneReferenceOptions(SceneInBuildColoring = ColoringBehaviour.Disabled, Toolbox = ToolboxBehaviour.Disabled)]
        [SerializeField] private SceneReference bothDisabled;

        [Space]
        [SceneReferenceOptions(SceneInBuildColoring = ColoringBehaviour.Enabled, Toolbox = ToolboxBehaviour.Enabled)]
        [SerializeField] private SceneReference bothEnabled;

        [Space]
        [SceneReferenceOptions(SceneInBuildColoring = ColoringBehaviour.Enabled, Toolbox = ToolboxBehaviour.Disabled)]
        [SerializeField] private SceneReference onlyColor;

        [Space]
        [SceneReferenceOptions(SceneInBuildColoring = ColoringBehaviour.Disabled, Toolbox = ToolboxBehaviour.Enabled)]
        [SerializeField] private SceneReference onlyUtility;
    }
}
