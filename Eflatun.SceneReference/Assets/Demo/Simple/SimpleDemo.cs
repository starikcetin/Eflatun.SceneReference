using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if ESR_ADDRESSABLES
using UnityEngine.AddressableAssets;
#endif // ESR_ADDRESSABLES

namespace Eflatun.SceneReference.Demo.Simple
{
    public class SimpleDemo : MonoBehaviour
    {
        [Header("Scenes")]
        [SerializeField] private SceneReference regularA;
        [SerializeField] private SceneReference regularB;
        [SerializeField] private SceneReference addressableA;
        [SerializeField] private SceneReference addressableB;

        [Header("Demo Tools")]
        [SerializeField] private GameObject buttonsPanel;
        [SerializeField] private GameObject loadingPanel;
        [SerializeField] private Text loadingText;

        public void LoadRegularA()
        {
            StartCoroutine(LoadScene(regularA));
        }

        public void LoadRegularB()
        {
            StartCoroutine(LoadScene(regularB));
        }

        public void LoadAddressableA()
        {
            StartCoroutine(LoadScene(addressableA));
        }

        public void LoadAddressableB()
        {
            StartCoroutine(LoadScene(addressableB));
        }

        private IEnumerator LoadScene(SceneReference sceneReference)
        {
            if (sceneReference.State == SceneReferenceState.Unsafe)
            {
                throw new Exception("Unsafe scene reference, can't load.");
            }

            buttonsPanel.SetActive(false);
            loadingPanel.SetActive(true);
            loadingText.text = $"Loading {sceneReference.Name}...";

            if (sceneReference.State == SceneReferenceState.Regular)
            {
                yield return SceneManager.LoadSceneAsync(sceneReference.Path);
            }
#if ESR_ADDRESSABLES
            else if (sceneReference.State == SceneReferenceState.Addressable)
            {
                yield return Addressables.LoadSceneAsync(sceneReference.Address);
            }
#endif // ESR_ADDRESSABLES
        }
    }
}
