using System;
using UnityEngine;

namespace Eflatun.SceneReference.Demo.UtilityLineAndColoringOverride
{
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
            [field: SceneReferenceOptions(Toolbox = ToolboxBehaviour.Disabled, SceneInBuildColoring = ColoringBehaviour.Disabled)]
            [field: SerializeField] public SceneReference SceneReferenceProp { get; private set; }

            [SceneReferenceOptions(Toolbox = ToolboxBehaviour.Disabled, SceneInBuildColoring = ColoringBehaviour.Disabled)] 
            [SerializeField] private SceneReference sceneReferenceField;
        }
    
        [field: SerializeField] public OuterContainer OuterContainerProp { get; private set; }
        [SerializeField] private OuterContainer outerContainerField;
    }
}
