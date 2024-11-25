using UnityEngine;

namespace Eflatun.SceneReference.Demo.CustomEditorUsage
{
    public class CustomEditorUsageDemo : MonoBehaviour
    {
        public SceneReference PublicScene;
        [SerializeField] private SceneReference privateScene;
    }
}
