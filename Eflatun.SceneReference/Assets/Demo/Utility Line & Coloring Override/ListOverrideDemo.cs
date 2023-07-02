﻿using System.Collections.Generic;
using UnityEngine;

namespace Eflatun.SceneReference.Demo
{
    public class ListOverrideDemo : MonoBehaviour
    {
        [SceneReferenceOptions(Toolbox = ToolboxBehaviour.Disabled, Coloring = ColoringBehaviour.Disabled)] 
        [SerializeField] private List<SceneReference> sceneReferences;
    }
}
