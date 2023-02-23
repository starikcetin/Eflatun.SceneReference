using System.Collections.Generic;
using Eflatun.SceneReference;
using UnityEngine;

public class Test : MonoBehaviour
{
    [field: Header("Enabled")]
    public SceneReference fieldEnabled;
    [field: SerializeField] public SceneReference PropEnabled { get; private set; }
    public SceneReference[] fieldArrayEnabled;
    [field: SerializeField] public SceneReference[] PropArrayEnabled { get; private set; }
    public List<SceneReference> fieldListEnabled;
    [field: SerializeField] public List<SceneReference> PropListEnabled { get; private set; }

    [field: Header("Disabled")]
    public SceneReference fieldDisabled;
    [field: SerializeField] public SceneReference PropDisabled { get; private set; }
    public SceneReference[] fieldArrayDisabled;
    [field: SerializeField] public SceneReference[] PropArrayDisabled { get; private set; }
    public List<SceneReference> fieldListDisabled;
    [field: SerializeField] public List<SceneReference> PropListDisabled { get; private set; }

    [field: Header("NotInBuild")]
    public SceneReference fieldNotInBuild;
    [field: SerializeField] public SceneReference PropNotInBuild { get; private set; }
    public SceneReference[] fieldArrayNotInBuild;
    [field: SerializeField] public SceneReference[] PropArrayNotInBuild { get; private set; }
    public List<SceneReference> fieldListNotInBuild;
    [field: SerializeField] public List<SceneReference> PropListNotInBuild { get; private set; }

    [field: Header("Unassigned")]
    public SceneReference fieldUnassigned;
    [field: SerializeField] public SceneReference PropUnassigned { get; private set; }
    public SceneReference[] fieldArrayUnassigned;
    [field: SerializeField] public SceneReference[] PropArrayUnassigned { get; private set; }
    public List<SceneReference> fieldListUnassigned;
    [field: SerializeField] public List<SceneReference> PropListUnassigned { get; private set; }

    [field: Header("Empty")]
    public SceneReference fieldEmpty;
    [field: SerializeField] public SceneReference PropEmpty { get; private set; }
    public SceneReference[] fieldArrayEmpty;
    [field: SerializeField] public SceneReference[] PropArrayEmpty { get; private set; }
    public List<SceneReference> fieldListEmpty;
    [field: SerializeField] public List<SceneReference> PropListEmpty { get; private set; }

    [field: Header("Deleted")]
    public SceneReference fieldDeleted;
    [field: SerializeField] public SceneReference PropDeleted { get; private set; }
    public SceneReference[] fieldArrayDeleted;
    [field: SerializeField] public SceneReference[] PropArrayDeleted { get; private set; }
    public List<SceneReference> fieldListDeleted;
    [field: SerializeField] public List<SceneReference> PropListDeleted { get; private set; }

    [field: Header("Invalid")]
    public SceneReference fieldInvalid;
    [field: SerializeField] public SceneReference PropInvalid { get; private set; }
    public SceneReference[] fieldArrayInvalid;
    [field: SerializeField] public SceneReference[] PropArrayInvalid { get; private set; }
    public List<SceneReference> fieldListInvalid;
    [field: SerializeField] public List<SceneReference> PropListInvalid { get; private set; }
}
