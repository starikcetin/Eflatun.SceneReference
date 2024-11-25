using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

#if ESR_ADDRESSABLES
using UnityEngine.AddressableAssets;
#endif // ESR_ADDRESSABLES

namespace Eflatun.SceneReference.Demo.AddressableScene
{
    public class AddressableSceneDemo : MonoBehaviour
    {
        [SerializeField] private SceneReference addressableSceneRef;

        [SceneReferenceOptions(AddressableColoring = ColoringBehaviour.Disabled)]
        [SerializeField] private SceneReference noAddressableColoring;

        private void Start()
        {
            Debug.Log($"State: {addressableSceneRef.State}");

            if (addressableSceneRef.State == SceneReferenceState.Addressable)
            {
                Debug.Log($"Address: {addressableSceneRef.Address}");
#if ESR_ADDRESSABLES
                Addressables.LoadSceneAsync(addressableSceneRef.Address, LoadSceneMode.Additive);
#endif // ESR_ADDRESSABLES
            }
        }
    }
}
