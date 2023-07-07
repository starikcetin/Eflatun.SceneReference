using System.Collections.Generic;
using UnityEngine;

namespace Eflatun.SceneReference.Tests.Runtime.Subjects
{
    public class TestSubjectContainer : MonoBehaviour
    {
        [field: Header("EnabledScene")]
        public SceneReference fieldEnabledScene;
        [field: SerializeField] public SceneReference PropEnabledScene { get; private set; }
        public SceneReference[] fieldArrayEnabledScene;
        [field: SerializeField] public SceneReference[] PropArrayEnabledScene { get; private set; }
        public List<SceneReference> fieldListEnabledScene;
        [field: SerializeField] public List<SceneReference> PropListEnabledScene { get; private set; }

        [field: Header("DisabledScene")]
        public SceneReference fieldDisabledScene;
        [field: SerializeField] public SceneReference PropDisabledScene { get; private set; }
        public SceneReference[] fieldArrayDisabledScene;
        [field: SerializeField] public SceneReference[] PropArrayDisabledScene { get; private set; }
        public List<SceneReference> fieldListDisabledScene;
        [field: SerializeField] public List<SceneReference> PropListDisabledScene { get; private set; }

        [field: Header("NotInBuildScene")]
        public SceneReference fieldNotInBuildScene;
        [field: SerializeField] public SceneReference PropNotInBuildScene { get; private set; }
        public SceneReference[] fieldArrayNotInBuildScene;
        [field: SerializeField] public SceneReference[] PropArrayNotInBuildScene { get; private set; }
        public List<SceneReference> fieldListNotInBuildScene;
        [field: SerializeField] public List<SceneReference> PropListNotInBuildScene { get; private set; }

        [field: Header("Empty")]
        public SceneReference fieldEmpty;
        [field: SerializeField] public SceneReference PropEmpty { get; private set; }
        public SceneReference[] fieldArrayEmpty;
        [field: SerializeField] public SceneReference[] PropArrayEmpty { get; private set; }
        public List<SceneReference> fieldListEmpty;
        [field: SerializeField] public List<SceneReference> PropListEmpty { get; private set; }

        [field: Header("DeletedScene")]
        public SceneReference fieldDeletedScene;
        [field: SerializeField] public SceneReference PropDeletedScene { get; private set; }
        public SceneReference[] fieldArrayDeletedScene;
        [field: SerializeField] public SceneReference[] PropArrayDeletedScene { get; private set; }
        public List<SceneReference> fieldListDeletedScene;
        [field: SerializeField] public List<SceneReference> PropListDeletedScene { get; private set; }

        [field: Header("NotExisting")]
        public SceneReference fieldNotExisting;
        [field: SerializeField] public SceneReference PropNotExisting { get; private set; }
        public SceneReference[] fieldArrayNotExisting;
        [field: SerializeField] public SceneReference[] PropArrayNotExisting { get; private set; }
        public List<SceneReference> fieldListNotExisting;
        [field: SerializeField] public List<SceneReference> PropListNotExisting { get; private set; }

        [field: Header("NotSceneAsset")]
        public SceneReference fieldNotSceneAsset;
        [field: SerializeField] public SceneReference PropNotSceneAsset { get; private set; }
        public SceneReference[] fieldArrayNotSceneAsset;
        [field: SerializeField] public SceneReference[] PropArrayNotSceneAsset { get; private set; }
        public List<SceneReference> fieldListNotSceneAsset;
        [field: SerializeField] public List<SceneReference> PropListNotSceneAsset { get; private set; }

        [field: Header("Addressable1Scene")]
        public SceneReference fieldAddressable1Scene;
        [field: SerializeField] public SceneReference PropAddressable1Scene { get; private set; }
        public SceneReference[] fieldArrayAddressable1Scene;
        [field: SerializeField] public SceneReference[] PropArrayAddressable1Scene { get; private set; }
        public List<SceneReference> fieldListAddressable1Scene;
        [field: SerializeField] public List<SceneReference> PropListAddressable1Scene { get; private set; }

        [field: Header("Addressable2Scene")]
        public SceneReference fieldAddressable2Scene;
        [field: SerializeField] public SceneReference PropAddressable2Scene { get; private set; }
        public SceneReference[] fieldArrayAddressable2Scene;
        [field: SerializeField] public SceneReference[] PropArrayAddressable2Scene { get; private set; }
        public List<SceneReference> fieldListAddressable2Scene;
        [field: SerializeField] public List<SceneReference> PropListAddressable2Scene { get; private set; }

        [field: Header("AddressableDuplicateAddressAScene")]
        public SceneReference fieldAddressableDuplicateAddressAScene;
        [field: SerializeField] public SceneReference PropAddressableDuplicateAddressAScene { get; private set; }
        public SceneReference[] fieldArrayAddressableDuplicateAddressAScene;
        [field: SerializeField] public SceneReference[] PropArrayAddressableDuplicateAddressAScene { get; private set; }
        public List<SceneReference> fieldListAddressableDuplicateAddressAScene;
        [field: SerializeField] public List<SceneReference> PropListAddressableDuplicateAddressAScene { get; private set; }

        [field: Header("AddressableDuplicateAddressBScene")]
        public SceneReference fieldAddressableDuplicateAddressBScene;
        [field: SerializeField] public SceneReference PropAddressableDuplicateAddressBScene { get; private set; }
        public SceneReference[] fieldArrayAddressableDuplicateAddressBScene;
        [field: SerializeField] public SceneReference[] PropArrayAddressableDuplicateAddressBScene { get; private set; }
        public List<SceneReference> fieldListAddressableDuplicateAddressBScene;
        [field: SerializeField] public List<SceneReference> PropListAddressableDuplicateAddressBScene { get; private set; }
    }
}
