using UnityEngine;

// https://github.com/starikcetin/Eflatun.SceneReference/issues/29

namespace Eflatun.SceneReference.Demo
{
    public class UnassignedSceneReferenceDemo : MonoBehaviour
    {
        public SceneReference scene;

        // Start is called before the first frame update
        void Start()
        {
            if (scene.HasValue)
            {
                Debug.Log("not null");
            }
            else
            {
                Debug.Log("null");
            }
        }
    }
}
