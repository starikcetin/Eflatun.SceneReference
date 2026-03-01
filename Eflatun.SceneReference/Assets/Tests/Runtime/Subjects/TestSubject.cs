using System;
using System.Collections.Generic;
using UnityEngine;

namespace Eflatun.SceneReference.Tests.Runtime.Subjects
{
    [Serializable]
    public class TestSubject
    {
        [SerializeField] private SceneReference field;
        public SceneReference Field => field;

        [field: SerializeField] public SceneReference Prop { get; private set; }

        [SerializeField] private SceneReference[] fieldArray;
        public IReadOnlyList<SceneReference> FieldArray => fieldArray;

        [field: SerializeField] public SceneReference[] PropArray { get; private set; }

        [SerializeField] private List<SceneReference> fieldList;
        public IReadOnlyList<SceneReference> FieldList => fieldList;

        [field: SerializeField] public List<SceneReference> PropList { get; private set; }
    }
}
