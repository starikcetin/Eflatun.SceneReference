using UnityEngine;

namespace Eflatun.SceneReference.Tests.Runtime.Utils
{
    public class BinaryBase64Logger : MonoBehaviour
    {
        [SerializeField]
        private SceneReference scene;

        private void Start()
        {
            LogBinaryBase64(nameof(scene), scene);
        }

        private void LogBinaryBase64(string name, SceneReference sr)
        {
            Debug.Log($"{name} binary base64: {TestUtils.SerializeToBinaryBase64(sr)}");
        }
    }
}
