using System.Collections.Generic;
using Eflatun.SceneReference;
using UnityEngine;

public class ListOverrideDemo : MonoBehaviour
{
    [SceneReferenceOptions(UtilityLine = UtilityLineBehaviour.Disabled, Coloring = ColoringBehaviour.Disabled)] 
    [SerializeField] private List<SceneReference> sceneReferences;
}
