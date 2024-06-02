using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Eflatun.SceneReference
{
    /// <summary>
    /// An adapter to allow passing a statically assigned <see cref="SceneReference"/> as a parameter to a <see cref="UnityEvent">.
    /// </summary>
    /// <remarks>
    /// This adapter is only useful when you want to use a statically assigned <see cref="SceneReference"/> as a parameter 
    /// to a <see cref="UnityEvent">. If you already have a <see cref="UnityEvent"/> that has a dynamically provided 
    /// <see cref="SceneReference"/> as a parameter, then you can just listen to the event directly without this adapter.
    /// <br/>
    /// See also: https://github.com/starikcetin/Eflatun.SceneReference/issues/97
    /// </remarks>
    [PublicAPI]
    public class SceneReferenceUnityEventAdapter : MonoBehaviour
    {
        [SerializeField] private SceneReference scene;

        /// <summary>
        /// Will be invoked with the assigned <see cref="SceneReference"/> when <see cref="Raise"/> is called.
        /// </summary>
        [field: SerializeField] public UnityEvent<SceneReference> Raised { get; private set; }

        /// <summary>
        /// Invokes <see cref="Raised"> with the assigned <see cref="SceneReference"/>.
        /// </summary>
        public void Raise()
        {
            Raised.Invoke(scene);
        }
    }
}
