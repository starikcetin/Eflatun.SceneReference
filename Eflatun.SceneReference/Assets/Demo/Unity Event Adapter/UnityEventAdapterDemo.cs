using UnityEngine;

namespace Eflatun.SceneReference.Demo.UnityEventAdapter
{
    public class UnityEventAdapterDemo : MonoBehaviour
    {
        public void LogScene(SceneReference sceneReference)
        {
            if (sceneReference.State != SceneReferenceState.Unsafe)
            {
                Debug.Log($"Received this safe scene: {sceneReference.Path}");
            }
            else
            {
                Debug.Log($"The received scene is unsafe because: {sceneReference.UnsafeReason}");
            }
        }
    }
}
