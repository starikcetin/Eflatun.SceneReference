using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReferenceTest : MonoBehaviour
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
