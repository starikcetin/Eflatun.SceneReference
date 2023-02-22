using System;
using System.Linq;
using System.Reflection;
using Eflatun.SceneReference;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class AssignFromCodeDemo : MonoBehaviour
{
    [SerializeField] private SceneReference[] source;

    private void Start()
    {
        // from guids
        try
        {
            var target = source.Select(x => new SceneReference(x.AssetGuidHex)).ToArray();
            for (var i = 0; i < source.Length; i++)
            {
                Assert.AreEqual(source[i].AssetGuidHex, target[i].AssetGuidHex);
            }

            Debug.Log("from guids pass");
        }
        catch (Exception e)
        {
            Debug.Log("from guids fail");
            Debug.LogException(e);
        }

        // from objects
        try
        {
            var target = source.Select(x => new SceneReference(GetSceneAsset(x))).ToArray();
            for (var i = 0; i < source.Length; i++)
            {
                Assert.AreEqual(source[i].AssetGuidHex, target[i].AssetGuidHex);
            }

            Debug.Log("from objects pass");
        }
        catch (Exception e)
        {
            Debug.Log("from objects fail");
            Debug.LogException(e);
        }

        // from paths
        try
        {
            var target = source.Select(x => SceneReference.FromScenePath(x.Path)).ToArray();
            for (var i = 0; i < source.Length; i++)
            {
                Assert.AreEqual(source[i].AssetGuidHex, target[i].AssetGuidHex);
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
            Assert.AreEqual(empty1.AssetGuidHex, empty2.AssetGuidHex);
            Assert.AreEqual(GetSceneAsset(empty1), GetSceneAsset(empty2));
            AssertBothThrowsOrAreEqual<EmptySceneReferenceException, string>(() => empty1.Path, () => empty2.Path);

            Debug.Log("empty pass");
        }
        catch (Exception e)
        {
            Debug.Log("empty fail");
            Debug.LogException(e);
        }
    }

    private static UnityEngine.Object GetSceneAsset(SceneReference x)
    {
        const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        return (UnityEngine.Object) typeof(SceneReference).GetField("sceneAsset", bindingFlags)?.GetValue(x);
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
