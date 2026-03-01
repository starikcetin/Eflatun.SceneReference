using UnityEngine;

namespace Eflatun.SceneReference.Tests.Runtime.Utils
{
    public class BinaryFormatterBase64Logger : MonoBehaviour
    {
        [SerializeField]
        private SceneReference scene;

        private void Start()
        {
            LogBase64OutputOfBinaryFormatter(nameof(scene), scene);
        }

        private void LogBase64OutputOfBinaryFormatter(string name, SceneReference sceneReference)
        {
            Debug.Log($"{name} BinaryFormatter output as base64: {TestUtils.SerializeToBase64ViaBinaryFormatter(sceneReference)}");
        }
    }
}
