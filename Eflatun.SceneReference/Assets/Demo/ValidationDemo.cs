using System;
using System.Text;
using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.Assertions;

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

        Assert.IsTrue(valid.IsSceneValid);
        Assert.IsTrue(disabled.IsSceneValid);
        Assert.IsTrue(notInBuild.IsSceneValid);
        Assert.IsFalse(empty.IsSceneValid);

        Assert.IsTrue(valid.IsInBuildAndEnabled);
        Assert.IsFalse(disabled.IsInBuildAndEnabled);
        Assert.IsFalse(notInBuild.IsInBuildAndEnabled);
        AssertThrows<InvalidSceneReferenceException>(() => empty.IsInBuildAndEnabled);

        Assert.IsTrue(valid.IsSafeToUse);
        Assert.IsFalse(disabled.IsSafeToUse);
        Assert.IsFalse(notInBuild.IsSafeToUse);
        Assert.IsFalse(empty.IsSafeToUse);

        Assert.AreNotEqual(-1, valid.BuildIndex);
        Assert.AreEqual(-1, disabled.BuildIndex);
        Assert.AreEqual(-1, notInBuild.BuildIndex);
        AssertThrows<InvalidSceneReferenceException>(() => empty.BuildIndex);
    }

    private void Log(SceneReference sceneReference, string memberName)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"SceneReference \"{memberName}\" properties:");

        sb.Append($"{nameof(sceneReference.AssetGuidHex)}: ");
        try
        {
            sb.AppendLine(sceneReference.AssetGuidHex.ToString());
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

        sb.Append($"{nameof(sceneReference.IsSceneValid)}: ");
        try
        {
            sb.AppendLine(sceneReference.IsSceneValid.ToString());
        }
        catch (Exception e)
        {
            sb.AppendLine($"throws {e.GetType().Name}");
        }

        sb.Append($"{nameof(sceneReference.IsInBuildAndEnabled)}: ");
        try
        {
            sb.AppendLine(sceneReference.IsInBuildAndEnabled.ToString());
        }
        catch (Exception e)
        {
            sb.AppendLine($"throws {e.GetType().Name}");
        }

        sb.Append($"{nameof(sceneReference.IsSafeToUse)}: ");
        try
        {
            sb.AppendLine(sceneReference.IsSafeToUse.ToString());
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
