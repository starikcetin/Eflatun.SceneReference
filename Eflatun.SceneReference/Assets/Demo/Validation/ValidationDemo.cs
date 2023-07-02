using System;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace Eflatun.SceneReference.Demo
{
    public class ValidationDemo : MonoBehaviour
    {
        [SerializeField] private SceneReference valid;
        [SerializeField] private SceneReference disabled;
        [SerializeField] private SceneReference notInBuild;
        [SerializeField] private SceneReference empty;

        private void Start()
        {
            Log(valid, nameof(valid));
            Log(disabled, nameof(disabled));
            Log(notInBuild, nameof(notInBuild));
            Log(empty, nameof(empty));

            Assert.IsTrue(valid.State != SceneReferenceState.Unsafe);
            Assert.IsTrue(disabled.State == SceneReferenceState.Unsafe);
            Assert.IsTrue(notInBuild.State == SceneReferenceState.Unsafe);
            Assert.IsFalse(empty.State == SceneReferenceState.Unsafe);
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

            Debug.Log(sb);
        }

        private void AssertThrows<TException>(Func<object> func)
            where TException : Exception
        {
            void Act()
            {
                var x = func.Invoke();
            }

            AssertThrows<TException>(Act);
        }

        private void AssertThrows<TException>(Action action)
            where TException : Exception
        {
            bool didThrow;

            try
            {
                action.Invoke();
                didThrow = false;
            }
            catch (TException)
            {
                didThrow = true;
            }

            Assert.IsTrue(didThrow);
        }
    }
}
