using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Eflatun.SceneReference.Editor.Toolbox
{
    internal class ToolboxPopupWindow : PopupWindowContent
    {
        private readonly IReadOnlyList<ITool> _tools;

        public ToolboxPopupWindow(IReadOnlyList<ITool> tools)
        {
            _tools = tools;
        }

        public override void OnGUI(Rect rect)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Scene Reference Toolbox", EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            if(_tools.Count == 0)
            {
                GUILayout.Label("No tools available.");
            }

            foreach (var tool in _tools)
            {
                tool.Draw();
            }
        }

        public override Vector2 GetWindowSize()
        {
            const int width = 200;
            var contentHeight = _tools.Count == 0 ? EditorGUIUtility.singleLineHeight : _tools.Sum(x => x.GetHeight()) + 5;
            return new Vector2(width, EditorGUIUtility.singleLineHeight + contentHeight);
        }
    }
}
