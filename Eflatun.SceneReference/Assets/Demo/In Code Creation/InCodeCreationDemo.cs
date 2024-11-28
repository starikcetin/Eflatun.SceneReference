using System;
using System.Linq;
using System.Reflection;
using Eflatun.SceneReference.Exceptions;
using UnityEngine;
using UnityEngine.Assertions;

namespace Eflatun.SceneReference.Demo.InCodeCreation
{
    public class InCodeCreationDemo : MonoBehaviour
    {
        [SerializeField] private SceneReference[] source;

        private void Start()
        {
            // from guids
            try
            {
                var target = source.Select(x => new SceneReference(x.Guid)).ToArray();
                for (var i = 0; i < source.Length; i++)
                {
                    Assert.AreEqual(source[i].Guid, target[i].Guid);
                }

                Debug.Log("from guids pass");
            }
            catch (Exception e)
            {
                Debug.Log("from guids fail");
                Debug.LogException(e);
            }

#if UNITY_EDITOR
            // from objects
            try
            {
                var target = source.Select(x => new SceneReference(GetAsset(x))).ToArray();
                for (var i = 0; i < source.Length; i++)
                {
                    Assert.AreEqual(source[i].Guid, target[i].Guid);
                }

                Debug.Log("from objects pass");
            }
            catch (Exception e)
            {
                Debug.Log("from objects fail");
                Debug.LogException(e);
            }
#endif // UNITY_EDITOR

            // from paths
            try
            {
                var target = source.Select(x => SceneReference.FromScenePath(x.Path)).ToArray();
                for (var i = 0; i < source.Length; i++)
                {
                    Assert.AreEqual(source[i].Guid, target[i].Guid);
                }

                Debug.Log("from paths pass");
            }
            catch (Exception e)
            {
                Debug.Log("from paths fail");
                Debug.LogException(e);
            }

            // empty
            try
            {
                var empty1 = new SceneReference();
                var empty2 = new SceneReference();
                Assert.AreEqual(empty1.Guid, empty2.Guid);
                Assert.AreEqual(GetAsset(empty1), GetAsset(empty2));
                AssertBothThrowsOrAreEqual<EmptySceneReferenceException, string>(() => empty1.Path, () => empty2.Path);

                Debug.Log("empty pass");
            }
            catch (Exception e)
            {
                Debug.Log("empty fail");
                Debug.LogException(e);
            }
        }

        private static UnityEngine.Object GetAsset(SceneReference x)
        {
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            return (UnityEngine.Object) typeof(SceneReference).GetField("asset", bindingFlags)?.GetValue(x);
        }

        private static void AssertBothThrowsOrAreEqual<TException, TValue>(Func<TValue> x, Func<TValue> y) where TException : Exception
        {
            bool xDidThrow, yDidThrow;
            TValue xValue, yValue;

            try
            {
                xValue = x();
                xDidThrow = false;
            }
            catch (TException)
            {
                xValue = default;
                xDidThrow = true;
            }

            try
            {
                yValue = y();
                yDidThrow = false;
            }
            catch (TException)
            {
                yValue = default;
                yDidThrow = true;
            }

            if (xDidThrow || yDidThrow)
            {
                Assert.AreEqual(xDidThrow, yDidThrow);
            }
            else
            {
                Assert.AreEqual(xValue, yValue);
            }
        }
    }
}
