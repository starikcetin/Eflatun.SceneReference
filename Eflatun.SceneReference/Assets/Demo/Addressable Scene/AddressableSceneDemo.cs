using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

#if EFLATUN_SCENEREFERENCE_ADDRESSABLES_PACKAGE_PRESENT
using UnityEngine.AddressableAssets;
#endif // EFLATUN_SCENEREFERENCE_ADDRESSABLES_PACKAGE_PRESENT

public class AddressableSceneDemo : MonoBehaviour
{
    [SerializeField] private SceneReference addressableSceneRef;

    private void Start()
    {
        Debug.Log($"Is addressable: {addressableSceneRef.IsAddressable}");
        Debug.Log($"IsSafeToUse: {addressableSceneRef.IsSafeToUse}");

        if (addressableSceneRef.IsAddressable)
        {
            Debug.Log($"Address: {addressableSceneRef.Address}");
#if EFLATUN_SCENEREFERENCE_ADDRESSABLES_PACKAGE_PRESENT
            Addressables.LoadSceneAsync(addressableSceneRef.Address, LoadSceneMode.Additive);
#endif // EFLATUN_SCENEREFERENCE_ADDRESSABLES_PACKAGE_PRESENT
        }
        else
        {
            SceneManager.LoadSceneAsync(addressableSceneRef.Path, LoadSceneMode.Additive);
        }
    }
}
