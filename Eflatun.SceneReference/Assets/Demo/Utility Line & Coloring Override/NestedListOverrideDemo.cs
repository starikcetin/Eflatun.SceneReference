using System;
using System.Collections.Generic;
using UnityEngine;

namespace Eflatun.SceneReference.Demo.UtilityLineAndColoringOverride
{
    public class NestedListOverrideDemo : MonoBehaviour
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
            [field: SerializeField] public List<SceneReference> SceneReferencesProp { get; private set; }

            [SceneReferenceOptions(Toolbox = ToolboxBehaviour.Disabled, SceneInBuildColoring = ColoringBehaviour.Disabled)] 
            [SerializeField] private List<SceneReference> sceneReferencesField;
        }
    
        [field: SerializeField] public OuterContainer OuterContainerProp { get; private set; }
        [SerializeField] private OuterContainer outerContainerField;
    }
}
