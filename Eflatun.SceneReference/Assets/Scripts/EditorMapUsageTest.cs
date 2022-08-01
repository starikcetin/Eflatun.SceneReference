using Eflatun.SceneReference;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    [InitializeOnLoad]
    public static class EditorMapUsageTest
    {
        static EditorMapUsageTest()
        {
            // EditorApplication.update += EditorApplication_OnUpdate;
        }

        private static void EditorApplication_OnUpdate()
        {
            Debug.Log(SceneGuidToPathMapProvider.SceneGuidToPathMap["50ccd0f8666b55544ac0f21398ad9535"] == "Assets/Scenes Folder 2/SceneC.unity");
        }
    }
}
