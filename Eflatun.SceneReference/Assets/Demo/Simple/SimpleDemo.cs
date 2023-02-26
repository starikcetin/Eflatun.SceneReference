using UnityEngine;
using UnityEngine.SceneManagement;

namespace Eflatun.SceneReference.Demo
{
    public class SimpleDemo : MonoBehaviour
    {
        [SerializeField] private SceneReference sceneARef;
        [SerializeField] private SceneReference sceneBRef;
        [SerializeField] private SceneReference sceneCRef;

        public void LoadA()
        {
            SceneManager.LoadScene(sceneARef.BuildIndex);
        }

        public void LoadB()
        {
            SceneManager.LoadScene(sceneBRef.BuildIndex);
        }

        public void LoadC()
        {
            SceneManager.LoadScene(sceneCRef.BuildIndex);
        }
    }
}
