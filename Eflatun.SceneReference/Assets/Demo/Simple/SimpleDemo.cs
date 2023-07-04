using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Eflatun.SceneReference.Demo
{
    public class SimpleDemo : MonoBehaviour
    {
        [SerializeField] private SceneReference regularA;
        [SerializeField] private SceneReference regularB;
        [SerializeField] private SceneReference addressableA;
        [SerializeField] private SceneReference addressableB;

        public void LoadRegularA()
        {
            SceneManager.LoadScene(regularA.BuildIndex);
        }

        public void LoadRegularB()
        {
            SceneManager.LoadScene(regularB.BuildIndex);
        }

        public void LoadAddressableA()
        {
            Addressables.LoadSceneAsync(addressableA.Address).WaitForCompletion();
        }

        public void LoadAddressableB()
        {
            Addressables.LoadSceneAsync(addressableB.Address).WaitForCompletion();
        }
    }
}
