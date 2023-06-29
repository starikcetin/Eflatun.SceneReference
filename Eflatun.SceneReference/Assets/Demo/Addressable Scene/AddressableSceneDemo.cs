using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

#if ESR_ADDRESSABLES
using UnityEngine.AddressableAssets;
#endif // ESR_ADDRESSABLES

public class AddressableSceneDemo : MonoBehaviour
{
    [SerializeField] private SceneReference addressableSceneRef;

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
