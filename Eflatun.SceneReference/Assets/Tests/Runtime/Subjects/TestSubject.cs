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

        [field: SerializeField] private SceneReference[] PropArrayPrivate { get; set; }
        public IReadOnlyList<SceneReference> PropArray => PropArrayPrivate;

        [SerializeField] private List<SceneReference> fieldList;
        public IReadOnlyList<SceneReference> FieldList => fieldList;

        [field: SerializeField] private List<SceneReference> PropListPrivate { get; set; }
        public IReadOnlyList<SceneReference> PropList => PropListPrivate;
    }
}
