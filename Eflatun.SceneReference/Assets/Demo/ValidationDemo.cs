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

        Assert.IsTrue(valid.HasValue);
        Assert.IsTrue(disabled.HasValue);
        Assert.IsTrue(notInBuild.HasValue);
        Assert.IsFalse(empty.HasValue);

        Assert.IsTrue(valid.IsInSceneGuidToPathMap);
        Assert.IsTrue(disabled.IsInSceneGuidToPathMap);
        Assert.IsTrue(notInBuild.IsInSceneGuidToPathMap);
        AssertThrows<EmptySceneReferenceException>(() => empty.IsInSceneGuidToPathMap);

        Assert.IsTrue(valid.IsInBuildAndEnabled);
        Assert.IsFalse(disabled.IsInBuildAndEnabled);
        Assert.IsFalse(notInBuild.IsInBuildAndEnabled);
        AssertThrows<EmptySceneReferenceException>(() => empty.IsInBuildAndEnabled);

        Assert.IsTrue(valid.IsSafeToUse);
        Assert.IsFalse(disabled.IsSafeToUse);
        Assert.IsFalse(notInBuild.IsSafeToUse);
        Assert.IsFalse(empty.IsSafeToUse);

        Assert.AreNotEqual(-1, valid.BuildIndex);
        Assert.AreEqual(-1, disabled.BuildIndex);
        Assert.AreEqual(-1, notInBuild.BuildIndex);
        AssertThrows<EmptySceneReferenceException>(() => empty.BuildIndex);
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
        
        sb.Append($"{nameof(sceneReference.HasValue)}: ");
        try
        {
            sb.AppendLine(sceneReference.HasValue.ToString());
        }
        catch (Exception e)
        {
            sb.AppendLine($"throws {e.GetType().Name}");
        }

        sb.Append($"{nameof(sceneReference.IsInSceneGuidToPathMap)}: ");
        try
        {
            sb.AppendLine(sceneReference.IsInSceneGuidToPathMap.ToString());
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
