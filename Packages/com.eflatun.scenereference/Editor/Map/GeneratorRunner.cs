using System.IO;
using UnityEditor;
using UnityEngine;

namespace Eflatun.SceneReference.Editor.Map
{
    [InitializeOnLoad]
    public class GeneratorRunner
    {
        private static bool _changed = false;

        static GeneratorRunner()
        {
            EditorApplication.update += EditorApplication_OnUpdate;
        }

        private static void EditorApplication_OnUpdate()
        {
            if (_changed)
            {
                _changed = false;

                if (Importer.CheckExistsAll())
                {
                    Debug.Log("Regenerating scene map.");
                    Generator.GenerateAndWrite();
                }
                else
                {
                    Debug.LogError("You need to import");
                }
            }
        }

        [MenuItem("Eflatun/SceneReference/Generate Scene Map")]
        public static void SetChanged()
        {
            _changed = true;
        }
    }
}
