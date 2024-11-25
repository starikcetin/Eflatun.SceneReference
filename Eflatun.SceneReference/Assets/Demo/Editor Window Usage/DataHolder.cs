using UnityEngine;

namespace Eflatun.SceneReference.Demo.EditorWindowUsage
{
    [CreateAssetMenu(menuName = "Eflatun/Scene Reference/Demo/Editor Window Usage/Data Holder", fileName = "Data Holder")]
    public class DataHolder : ScriptableObject
    {
        public SceneReference PublicScene;
        [SerializeField] private SceneReference privateScene;

        private static DataHolder _instance;
        public static DataHolder Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<DataHolder>("Data Holder");
                }

                return _instance;
            }
        }
    }
}
