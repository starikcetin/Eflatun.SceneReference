using System;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace Eflatun.SceneReference.Demo.Validation
{
    public class ValidationDemo : MonoBehaviour
    {
        [SerializeField] private SceneReference validRegular;
        [SerializeField] private SceneReference validAddressable;
        [SerializeField] private SceneReference disabledInBuild;
        [SerializeField] private SceneReference notInBuild;
        [SerializeField] private SceneReference empty;
        [SerializeField] private SceneReference gone;

        private void Start()
        {
            Log(validRegular, nameof(validRegular));
            Log(validAddressable, nameof(validAddressable));
            Log(disabledInBuild, nameof(disabledInBuild));
            Log(notInBuild, nameof(notInBuild));
            Log(empty, nameof(empty));
            Log(gone, nameof(gone));

            Assert.AreEqual(validRegular.State, SceneReferenceState.Regular);
            Assert.AreEqual(validRegular.UnsafeReason, SceneReferenceUnsafeReason.None);

            Assert.AreEqual(validAddressable.State, SceneReferenceState.Addressable);
            Assert.AreEqual(validAddressable.UnsafeReason, SceneReferenceUnsafeReason.None);

            Assert.AreEqual(disabledInBuild.State, SceneReferenceState.Unsafe);
            Assert.AreEqual(disabledInBuild.UnsafeReason, SceneReferenceUnsafeReason.NotInBuild);

            Assert.AreEqual(notInBuild.State, SceneReferenceState.Unsafe);
            Assert.AreEqual(notInBuild.UnsafeReason, SceneReferenceUnsafeReason.NotInBuild);

            Assert.AreEqual(empty.State, SceneReferenceState.Unsafe);
            Assert.AreEqual(empty.UnsafeReason, SceneReferenceUnsafeReason.Empty);

            Assert.AreEqual(gone.State, SceneReferenceState.Unsafe);
            Assert.AreEqual(gone.UnsafeReason, SceneReferenceUnsafeReason.NotInMaps);
        }

        private void Log(SceneReference sceneReference, string memberName)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"SceneReference \"{memberName}\" properties:");

            sb.Append($"{nameof(sceneReference.Guid)}: ");
            try
            {
                sb.AppendLine(sceneReference.Guid.ToString());
            }
            catch (Exception e)
            {
                sb.AppendLine($"throws {e.GetType().Name}");
            }

            sb.Append($"{nameof(sceneReference.Path)}: ");
            try
            {
                sb.AppendLine(sceneReference.Path.ToString());
            }
            catch (Exception e)
            {
                sb.AppendLine($"throws {e.GetType().Name}");
            }

            sb.Append($"{nameof(sceneReference.BuildIndex)}: ");
            try
            {
                sb.AppendLine(sceneReference.BuildIndex.ToString());
            }
            catch (Exception e)
            {
                sb.AppendLine($"throws {e.GetType().Name}");
            }

            sb.Append($"{nameof(sceneReference.Name)}: ");
            try
            {
                sb.AppendLine(sceneReference.Name.ToString());
            }
            catch (Exception e)
            {
                sb.AppendLine($"throws {e.GetType().Name}");
            }

            sb.Append($"{nameof(sceneReference.LoadedScene)}: ");
            try
            {
                sb.AppendLine(sceneReference.LoadedScene.ToString());
            }
            catch (Exception e)
            {
                sb.AppendLine($"throws {e.GetType().Name}");
            }

            sb.Append($"{nameof(sceneReference.Address)}: ");
            try
            {
                sb.AppendLine(sceneReference.Address.ToString());
            }
            catch (Exception e)
            {
                sb.AppendLine($"throws {e.GetType().Name}");
            }

            sb.Append($"{nameof(sceneReference.State)}: ");
            try
            {
                sb.AppendLine(sceneReference.State.ToString());
            }
            catch (Exception e)
            {
                sb.AppendLine($"throws {e.GetType().Name}");
            }

            sb.Append($"{nameof(sceneReference.UnsafeReason)}: ");
            try
            {
                sb.AppendLine(sceneReference.UnsafeReason.ToString());
            }
            catch (Exception e)
            {
                sb.AppendLine($"throws {e.GetType().Name}");
            }

            Debug.Log(sb);
        }
    }
}
