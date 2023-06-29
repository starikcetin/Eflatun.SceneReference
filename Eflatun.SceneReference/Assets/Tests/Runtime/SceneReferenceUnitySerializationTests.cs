using System.Collections;
using Eflatun.SceneReference.Tests.Runtime.Subjects;
using Eflatun.SceneReference.Tests.Runtime.Utils;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Eflatun.SceneReference.Tests.Runtime
{
    public class SceneReferenceUnitySerializationTests
    {
        private TestSubjectContainer _testMb;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            yield return SceneManager.LoadSceneAsync(TestUtils.TestSubjectContainerScenePath, LoadSceneMode.Additive);
            _testMb = UnityEngine.Object.FindObjectOfType<TestSubjectContainer>();
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            var scene = SceneManager.GetSceneByPath(TestUtils.TestSubjectContainerScenePath);
            yield return SceneManager.UnloadSceneAsync(scene);
            _testMb = null;
        }

        [Test]
        public void ProvidesExpectedState_EnabledScene()
        {
            TestUtils.AssertEnabledSceneState(_testMb.fieldEnabledScene);
            TestUtils.AssertEnabledSceneState(_testMb.PropEnabledScene);

            foreach (var sr in _testMb.fieldArrayEnabledScene)
            {
                TestUtils.AssertEnabledSceneState(sr);
            }

            foreach (var sr in _testMb.PropArrayEnabledScene)
            {
                TestUtils.AssertEnabledSceneState(sr);
            }

            foreach (var sr in _testMb.fieldListEnabledScene)
            {
                TestUtils.AssertEnabledSceneState(sr);
            }

            foreach (var sr in _testMb.PropListEnabledScene)
            {
                TestUtils.AssertEnabledSceneState(sr);
            }
        }

        [Test]
        public void ProvidesExpectedState_DisabledScene()
        {
            TestUtils.AssertDisabledSceneState(_testMb.fieldDisabledScene);
            TestUtils.AssertDisabledSceneState(_testMb.PropDisabledScene);

            foreach (var sr in _testMb.fieldArrayDisabledScene)
            {
                TestUtils.AssertDisabledSceneState(sr);
            }

            foreach (var sr in _testMb.PropArrayDisabledScene)
            {
                TestUtils.AssertDisabledSceneState(sr);
            }

            foreach (var sr in _testMb.fieldListDisabledScene)
            {
                TestUtils.AssertDisabledSceneState(sr);
            }

            foreach (var sr in _testMb.PropListDisabledScene)
            {
                TestUtils.AssertDisabledSceneState(sr);
            }
        }

        [Test]
        public void ProvidesExpectedState_NotInBuildScene()
        {
            TestUtils.AssertNotInBuildSceneState(_testMb.fieldNotInBuildScene);
            TestUtils.AssertNotInBuildSceneState(_testMb.PropNotInBuildScene);

            foreach (var sr in _testMb.fieldArrayNotInBuildScene)
            {
                TestUtils.AssertNotInBuildSceneState(sr);
            }

            foreach (var sr in _testMb.PropArrayNotInBuildScene)
            {
                TestUtils.AssertNotInBuildSceneState(sr);
            }

            foreach (var sr in _testMb.fieldListNotInBuildScene)
            {
                TestUtils.AssertNotInBuildSceneState(sr);
            }

            foreach (var sr in _testMb.PropListNotInBuildScene)
            {
                TestUtils.AssertNotInBuildSceneState(sr);
            }
        }

        [Test]
        public void ProvidesExpectedState_Empty()
        {
            TestUtils.AssertEmptyState(_testMb.fieldEmpty);
            TestUtils.AssertEmptyState(_testMb.PropEmpty);

            foreach (var sr in _testMb.fieldArrayEmpty)
            {
                TestUtils.AssertEmptyState(sr);
            }

            foreach (var sr in _testMb.PropArrayEmpty)
            {
                TestUtils.AssertEmptyState(sr);
            }

            foreach (var sr in _testMb.fieldListEmpty)
            {
                TestUtils.AssertEmptyState(sr);
            }

            foreach (var sr in _testMb.PropListEmpty)
            {
                TestUtils.AssertEmptyState(sr);
            }
        }

        [Test]
        public void ProvidesExpectedState_DeletedScene()
        {
            TestUtils.AssertDeletedSceneState(_testMb.fieldDeletedScene);
            TestUtils.AssertDeletedSceneState(_testMb.PropDeletedScene);

            foreach (var sr in _testMb.fieldArrayDeletedScene)
            {
                TestUtils.AssertDeletedSceneState(sr);
            }

            foreach (var sr in _testMb.PropArrayDeletedScene)
            {
                TestUtils.AssertDeletedSceneState(sr);
            }

            foreach (var sr in _testMb.fieldListDeletedScene)
            {
                TestUtils.AssertDeletedSceneState(sr);
            }

            foreach (var sr in _testMb.PropListDeletedScene)
            {
                TestUtils.AssertDeletedSceneState(sr);
            }
        }

        [Test]
        public void ProvidesExpectedState_NotExisting()
        {
            TestUtils.AssertNotExistingState(_testMb.fieldNotExisting);
            TestUtils.AssertNotExistingState(_testMb.PropNotExisting);

            foreach (var sr in _testMb.fieldArrayNotExisting)
            {
                TestUtils.AssertNotExistingState(sr);
            }

            foreach (var sr in _testMb.PropArrayNotExisting)
            {
                TestUtils.AssertNotExistingState(sr);
            }

            foreach (var sr in _testMb.fieldListNotExisting)
            {
                TestUtils.AssertNotExistingState(sr);
            }

            foreach (var sr in _testMb.PropListNotExisting)
            {
                TestUtils.AssertNotExistingState(sr);
            }
        }

        [Test]
        public void ProvidesExpectedState_NotSceneAsset()
        {
            TestUtils.AssertNotSceneAssetState(_testMb.fieldNotSceneAsset);
            TestUtils.AssertNotSceneAssetState(_testMb.PropNotSceneAsset);

            foreach (var sr in _testMb.fieldArrayNotSceneAsset)
            {
                TestUtils.AssertNotSceneAssetState(sr);
            }

            foreach (var sr in _testMb.PropArrayNotSceneAsset)
            {
                TestUtils.AssertNotSceneAssetState(sr);
            }

            foreach (var sr in _testMb.fieldListNotSceneAsset)
            {
                TestUtils.AssertNotSceneAssetState(sr);
            }

            foreach (var sr in _testMb.PropListNotSceneAsset)
            {
                TestUtils.AssertNotSceneAssetState(sr);
            }
        }

        [Test]
        public void ProvidesExpectedState_Addressable1Scene()
        {
            TestUtils.AssertAddressable1SceneState(_testMb.fieldAddressable1Scene);
            TestUtils.AssertAddressable1SceneState(_testMb.PropAddressable1Scene);

            foreach (var sr in _testMb.fieldArrayAddressable1Scene)
            {
                TestUtils.AssertAddressable1SceneState(sr);
            }

            foreach (var sr in _testMb.PropArrayAddressable1Scene)
            {
                TestUtils.AssertAddressable1SceneState(sr);
            }

            foreach (var sr in _testMb.fieldListAddressable1Scene)
            {
                TestUtils.AssertAddressable1SceneState(sr);
            }

            foreach (var sr in _testMb.PropListAddressable1Scene)
            {
                TestUtils.AssertAddressable1SceneState(sr);
            }
        }

        [Test]
        public void ProvidesExpectedState_Addressable2Scene()
        {
            TestUtils.AssertAddressable2SceneState(_testMb.fieldAddressable2Scene);
            TestUtils.AssertAddressable2SceneState(_testMb.PropAddressable2Scene);

            foreach (var sr in _testMb.fieldArrayAddressable2Scene)
            {
                TestUtils.AssertAddressable2SceneState(sr);
            }

            foreach (var sr in _testMb.PropArrayAddressable2Scene)
            {
                TestUtils.AssertAddressable2SceneState(sr);
            }

            foreach (var sr in _testMb.fieldListAddressable2Scene)
            {
                TestUtils.AssertAddressable2SceneState(sr);
            }

            foreach (var sr in _testMb.PropListAddressable2Scene)
            {
                TestUtils.AssertAddressable2SceneState(sr);
            }
        }

        [Test]
        public void ProvidesExpectedState_AddressableDuplicateAddressAScene()
        {
            TestUtils.AssertAddressableDuplicateAddressASceneState(_testMb.fieldAddressableDuplicateAddressAScene);
            TestUtils.AssertAddressableDuplicateAddressASceneState(_testMb.PropAddressableDuplicateAddressAScene);

            foreach (var sr in _testMb.fieldArrayAddressableDuplicateAddressAScene)
            {
                TestUtils.AssertAddressableDuplicateAddressASceneState(sr);
            }

            foreach (var sr in _testMb.PropArrayAddressableDuplicateAddressAScene)
            {
                TestUtils.AssertAddressableDuplicateAddressASceneState(sr);
            }

            foreach (var sr in _testMb.fieldListAddressableDuplicateAddressAScene)
            {
                TestUtils.AssertAddressableDuplicateAddressASceneState(sr);
            }

            foreach (var sr in _testMb.PropListAddressableDuplicateAddressAScene)
            {
                TestUtils.AssertAddressableDuplicateAddressASceneState(sr);
            }
        }

        [Test]
        public void ProvidesExpectedState_AddressableDuplicateAddressBScene()
        {
            TestUtils.AssertAddressableDuplicateAddressBSceneState(_testMb.fieldAddressableDuplicateAddressBScene);
            TestUtils.AssertAddressableDuplicateAddressBSceneState(_testMb.PropAddressableDuplicateAddressBScene);

            foreach (var sr in _testMb.fieldArrayAddressableDuplicateAddressBScene)
            {
                TestUtils.AssertAddressableDuplicateAddressBSceneState(sr);
            }

            foreach (var sr in _testMb.PropArrayAddressableDuplicateAddressBScene)
            {
                TestUtils.AssertAddressableDuplicateAddressBSceneState(sr);
            }

            foreach (var sr in _testMb.fieldListAddressableDuplicateAddressBScene)
            {
                TestUtils.AssertAddressableDuplicateAddressBSceneState(sr);
            }

            foreach (var sr in _testMb.PropListAddressableDuplicateAddressBScene)
            {
                TestUtils.AssertAddressableDuplicateAddressBSceneState(sr);
            }
        }
    }
}
