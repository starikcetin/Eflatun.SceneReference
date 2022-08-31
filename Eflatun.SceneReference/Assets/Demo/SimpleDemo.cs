using System.Reflection;
using System.Text;
using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.Assertions;

public class SimpleDemo : MonoBehaviour
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

        Assert.IsTrue(valid.IsSafeToUse);
        Assert.IsFalse(disabled.IsSafeToUse);
        Assert.IsFalse(notInBuild.IsSafeToUse);
        Assert.IsFalse(empty.IsSafeToUse);
    }

    private void Log(SceneReference sceneReference, string memberName)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"SceneReference \"{memberName}\" properties:");

        var fieldInfos = sceneReference.GetType().GetFields((BindingFlags)~0);
        foreach (var fieldInfo in fieldInfos)
        {
            sb.Append($"{fieldInfo.Name}: ");
            try
            {
                sb.AppendLine(fieldInfo.GetValue(sceneReference).ToString());
            }
            catch (TargetInvocationException e)
            {
                sb.AppendLine($"throws {e.InnerException.GetType().Name}");
            }
        }

        var propertyInfos = sceneReference.GetType().GetProperties((BindingFlags)~0);
        foreach (var propertyInfo in propertyInfos)
        {
            sb.Append($"{propertyInfo.Name}: ");
            try
            {
                sb.AppendLine(propertyInfo.GetValue(sceneReference).ToString());
            }
            catch (TargetInvocationException e)
            {
                sb.AppendLine($"throws {e.InnerException.GetType().Name}");
            }
        }

        Debug.Log(sb);
    }
}
