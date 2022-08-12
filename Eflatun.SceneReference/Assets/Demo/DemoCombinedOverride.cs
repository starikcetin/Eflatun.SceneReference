using Eflatun.SceneReference;
using UnityEngine;

public class DemoCombinedOverride : MonoBehaviour
{
    [SerializeField] private SceneReference noOptions;

    [Space]
    [SceneReferenceOptions(Coloring = ColoringBehaviour.DoNotOverride, UtilityLine = UtilityLineBehaviour.DoNotOverride)]
    [SerializeField] private SceneReference doNotOverride;

    [Space]
    [SceneReferenceOptions(Coloring = ColoringBehaviour.Disabled, UtilityLine = UtilityLineBehaviour.Disabled)]
    [SerializeField] private SceneReference bothDisabled;

    [Space]
    [SceneReferenceOptions(Coloring = ColoringBehaviour.Enabled, UtilityLine = UtilityLineBehaviour.Enabled)]
    [SerializeField] private SceneReference bothEnabled;

    [Space]
    [SceneReferenceOptions(Coloring = ColoringBehaviour.Enabled, UtilityLine = UtilityLineBehaviour.Disabled)]
    [SerializeField] private SceneReference onlyColor;

    [Space]
    [SceneReferenceOptions(Coloring = ColoringBehaviour.Disabled, UtilityLine = UtilityLineBehaviour.Enabled)]
    [SerializeField] private SceneReference onlyUtility;
}
