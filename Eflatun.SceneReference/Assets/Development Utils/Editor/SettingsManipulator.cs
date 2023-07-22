using Eflatun.SceneReference.Editor;
using UnityEditor;

namespace Eflatun.SceneReference.DevelopmentUtils.Editor
{
    public class SettingsManipulator : EditorWindow
    {
        [MenuItem("Tools/" + Constants.MenuPrefixBase + "/_Dev/Settings Manipulator")]
        public static void Display()
        {
            GetWindow<SettingsManipulator>();
        }

        private void OnGUI()
        {
            // Draw GenerationTriggers field.
            {
                var oldPublic = SettingsManager.SceneDataMaps.GenerationTriggers.value;
                var oldInternal = (InternalSceneDataMapsGeneratorTriggers)oldPublic;
                var newInternal = EditorGUILayout.EnumFlagsField("Generation Triggers", oldInternal);
                var newPublic = (SceneDataMapsGeneratorTriggers)newInternal;
                if (newPublic != oldPublic)
                {
                    SettingsManager.SceneDataMaps.GenerationTriggers.value = newPublic;
                }
            }
        }
    }
}
