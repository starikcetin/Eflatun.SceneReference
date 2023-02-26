using System.Collections.Generic;
using UnityEngine;

namespace Eflatun.SceneReference.Demo
{
    public class ListOverrideDemo : MonoBehaviour
    {
        [SceneReferenceOptions(UtilityLine = UtilityLineBehaviour.Disabled, Coloring = ColoringBehaviour.Disabled)] 
        [SerializeField] private List<SceneReference> sceneReferences;
    }
}
