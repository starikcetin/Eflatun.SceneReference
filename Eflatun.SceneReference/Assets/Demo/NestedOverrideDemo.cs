using System;
using Eflatun.SceneReference;
using UnityEngine;

public class NestedOverrideDemo : MonoBehaviour
{
    [Serializable]
    public class OuterContainer
    {
        [field: SerializeField] public InnerContainer InnerContainerProp { get; private set; }
        [SerializeField] private InnerContainer innerContainerField;
    }

    [Serializable]
    public class InnerContainer
    {
        [field: SceneReferenceOptions(UtilityLine = UtilityLineBehaviour.Disabled, Coloring = ColoringBehaviour.Disabled)]
        [field: SerializeField] public SceneReference SceneReferenceProp { get; private set; }

        [SceneReferenceOptions(UtilityLine = UtilityLineBehaviour.Disabled, Coloring = ColoringBehaviour.Disabled)] 
        [SerializeField] private SceneReference sceneReferenceField;
    }
    
    [field: SerializeField] public OuterContainer OuterContainerProp { get; private set; }
    [SerializeField] private OuterContainer outerContainerField;
}
