using System;
using UnityEditor;

namespace Eflatun.SceneReference.Editor
{
    [InitializeOnLoad]
    public static class EditorUtils
    {
        public static event Action EditorUpdateOneShot;

        static EditorUtils()
        {
            EditorApplication.update += EditorApplication_OnUpdate;
        }

        private static void EditorApplication_OnUpdate()
        {
            if (EditorUpdateOneShot != null)
            {
                var temp = EditorUpdateOneShot;
                EditorUpdateOneShot = null;
                temp.Invoke();
            }
        }

        public static void DelayFor(int editorFrames, Action action)
        {
            for (var i = 0; i < editorFrames; i++)
            {
                var actionNow = action;
                action = () => EditorUpdateOneShot += actionNow;
            }

            action();
        }
    }
}
