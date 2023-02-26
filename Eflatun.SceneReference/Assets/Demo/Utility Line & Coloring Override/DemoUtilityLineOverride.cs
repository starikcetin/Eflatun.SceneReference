using UnityEngine;

namespace Eflatun.SceneReference.Demo
{
    public class DemoUtilityLineOverride : MonoBehaviour
    {
        [SerializeField] private SceneReference noOptions;

        [Space]
        [SceneReferenceOptions(UtilityLine = UtilityLineBehaviour.DoNotOverride)]
        [SerializeField] private SceneReference doNotOverride;

        [Space]
        [SceneReferenceOptions(UtilityLine = UtilityLineBehaviour.Disabled)]
        [SerializeField] private SceneReference disabled;

        [Space]
        [SceneReferenceOptions(UtilityLine = UtilityLineBehaviour.Enabled)]
        [SerializeField] private new SceneReference enabled;
    }
}
