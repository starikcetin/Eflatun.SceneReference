using System.Collections.Generic;
using Eflatun.SceneReference;
using UnityEngine;

public class Demo : MonoBehaviour
{
    [SerializeField] private SceneReference scene;
    [SerializeField] private SceneReference anotherScene;
    [SerializeField] private SceneReference yetAnotherScene;

    [SerializeField] private SceneReference empty;

    [SerializeField] private List<SceneReference> sceneReferenceList;
}
