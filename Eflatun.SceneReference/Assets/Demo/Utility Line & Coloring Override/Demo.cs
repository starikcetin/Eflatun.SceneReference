using System.Collections.Generic;
using UnityEngine;

namespace Eflatun.SceneReference.Demo
{
    public class Demo : MonoBehaviour
    {
        [SerializeField] private SceneReference scene;
        [SerializeField] private SceneReference anotherScene;
        [SerializeField] private SceneReference yetAnotherScene;

        [SerializeField] private SceneReference empty;

        [SerializeField] private List<SceneReference> sceneReferenceList;
    }
}
