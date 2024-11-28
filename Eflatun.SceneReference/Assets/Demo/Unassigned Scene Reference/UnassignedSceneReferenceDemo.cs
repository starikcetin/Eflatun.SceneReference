using UnityEngine;

// https://github.com/starikcetin/Eflatun.SceneReference/issues/29

namespace Eflatun.SceneReference.Demo.UnassignedSceneReference
{
    public class UnassignedSceneReferenceDemo : MonoBehaviour
    {
        public SceneReference scene;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log($"State: {scene.State}");
        }
    }
}
