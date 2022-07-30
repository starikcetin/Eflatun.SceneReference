﻿using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Eflatun.SceneReference.Editor.Map
{
    public static class Generator
    {
        [MenuItem("Eflatun/SceneReference/Generate Scene Map")]
        public static void Run()
        {
            EditorUtils.EditorUpdateOneShot -= Run;
            
            if (!Importer.CheckExistsAll())
            {
                throw new Exception("You need to import.");
            }
            
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
                EditorApplication.LockReloadAssemblies();

                var sb = new StringBuilder();

                sb.AppendLine("/*");
                sb.AppendLine(" * GENERATED FILE. DO NOT EDIT.");
                sb.AppendLine(" * This file is generated by Eflatun.SceneReference package.");
                sb.AppendLine(" */");
                sb.AppendLine("");
                sb.AppendLine("#pragma warning disable");
                sb.AppendLine("");
                sb.AppendLine("using System.Collections.Generic;");
                sb.AppendLine("");
                sb.AppendLine("namespace Eflatun.SceneReference");
                sb.AppendLine("{");
                sb.AppendLine("    public static partial class Map");
                sb.AppendLine("    {");
                sb.AppendLine("        static Map()");
                sb.AppendLine("        {");
                sb.AppendLine("            SceneGuidToScenePath = new Dictionary<string, string>");
                sb.AppendLine("            {");

                var allSceneGuids = AssetDatabase.FindAssets("t:Scene");
                foreach (var sceneGuid in allSceneGuids)
                {
                    var scenePath = AssetDatabase.GUIDToAssetPath(sceneGuid);
                    sb.AppendLine($"                {{\"{sceneGuid}\", \"{scenePath}\"}},");
                }

                sb.AppendLine("            };");
                sb.AppendLine("        }");
                sb.AppendLine("    }");
                sb.AppendLine("}");

                var source = sb.ToString();
                File.WriteAllText(Paths.GeneratedWriteFilePath, source);

            }
            finally
            {
                AssetDatabase.Refresh();
                EditorApplication.UnlockReloadAssemblies();
                EditorUtility.ClearProgressBar();
            }
        }
    }
}