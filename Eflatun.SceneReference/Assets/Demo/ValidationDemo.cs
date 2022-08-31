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

        Assert.IsTrue(valid.IsValidSceneAsset);
        Assert.IsTrue(disabled.IsValidSceneAsset);
        Assert.IsTrue(notInBuild.IsValidSceneAsset);
        Assert.IsFalse(empty.IsValidSceneAsset);

        Assert.IsTrue(valid.IsInBuildAndEnabled);
        Assert.IsFalse(disabled.IsInBuildAndEnabled);
        Assert.IsFalse(notInBuild.IsInBuildAndEnabled);
        // Assert.IsFalse(empty.IsInBuildAndEnabled); // throws

        Assert.IsTrue(valid.IsSafeToUse);
        Assert.IsFalse(disabled.IsSafeToUse);
        Assert.IsFalse(notInBuild.IsSafeToUse);
        Assert.IsFalse(empty.IsSafeToUse);
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

        sb.Append($"{nameof(sceneReference.IsValidSceneAsset)}: ");
        try
        {
            sb.AppendLine(sceneReference.IsValidSceneAsset.ToString());
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
}
