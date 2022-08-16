using System;
using Eflatun.SceneReference;
using UnityEngine;

public class NestedOverrideDemo : MonoBehaviour
{
    [field: SerializeField] public OuterContainer OuterContainerProp { get; private set; }
    [SerializeField] private OuterContainer outerContainerField;
}

[Serializable]
public class OuterContainer
{
    [field: SerializeField] public InnerContainer InnerContainerProp { get; private set; }
    [SerializeField] private InnerContainer innerContainerField;
}

[Serializable]
public class InnerContainer
{
    [field: SceneReferenceOptions(UtilityLine = UtilityLineBehaviour.Disabled, Coloring = ColoringBehaviour.Enabled)]
    [field: SerializeField] public SceneReference SceneReferenceProp { get; private set; }

    [SceneReferenceOptions(UtilityLine = UtilityLineBehaviour.Disabled, Coloring = ColoringBehaviour.Enabled)] 
    [SerializeField] private SceneReference sceneReferenceField;
}
