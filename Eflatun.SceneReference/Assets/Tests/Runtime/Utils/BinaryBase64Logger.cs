using UnityEngine;

namespace Eflatun.SceneReference.Tests.Runtime.Utils
{
    public class BinaryBase64Logger : MonoBehaviour
    {
        [SerializeField] private SceneReference enabledScene;
        [SerializeField] private SceneReference disabledScene;
        [SerializeField] private SceneReference notInBuildScene;
        [SerializeField] private SceneReference empty;
        [SerializeField] private SceneReference deletedScene;
        [SerializeField] private SceneReference notExisting;
        [SerializeField] private SceneReference notSceneAsset;

        private void Start()
        {
            LogBinaryBase64(nameof(enabledScene), enabledScene);
            LogBinaryBase64(nameof(disabledScene), disabledScene);
            LogBinaryBase64(nameof(notInBuildScene), notInBuildScene);
            LogBinaryBase64(nameof(empty), empty);
            LogBinaryBase64(nameof(deletedScene), deletedScene);
            LogBinaryBase64(nameof(notExisting), notExisting);
            LogBinaryBase64(nameof(notSceneAsset), notSceneAsset);
        }

        private void LogBinaryBase64(string name, SceneReference sr)
        {
            Debug.Log($"{name} binary base64: {TestUtils.SerializeToBinaryBase64(sr)}");
        }
    }
}
