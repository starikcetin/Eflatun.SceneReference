using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Eflatun.SceneReference.Editor.Map
{
    public static class Generator
    {
        [MenuItem("Eflatun/SceneReference/Generate Scene Map")]
        public static void Run()
        {
            EditorUtils.EditorUpdateOneShot -= Run;
            Debug.Log("Regenerating scene map.");
            GenerateAndWrite();
        }
        
        public static void RunNextEditorFrame()
        {
            EditorUtils.EditorUpdateOneShot += Run;
        }

        private static void GenerateAndWrite()
        {
            try
            {
                EditorUtility.DisplayProgressBar("Generating scene map", "Generating scene map", 0);

                var allSceneGuids = AssetDatabase.FindAssets("t:Scene");
                var dictionary = allSceneGuids.ToDictionary(
                    x => x, // key generator: take guids
                    AssetDatabase.GUIDToAssetPath // value generator: take paths
                );
                
                var jsonRaw = JsonConvert.SerializeObject(dictionary, SettingsManager.JsonFormatting.value);
                
                Directory.CreateDirectory(Paths.RelativeToAssets.GenFolder.PlatformPath);
                File.WriteAllText(Paths.RelativeToAssets.GenFile.PlatformPath, jsonRaw);
            }
            finally
            {
                AssetDatabase.Refresh();
                EditorUtility.ClearProgressBar();
            }
        }
    }
}
