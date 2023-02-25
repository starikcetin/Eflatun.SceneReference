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

            foreach (var enabled in _testMb.fieldArrayEnabledScene)
            {
                TestUtils.AssertEnabledSceneState(enabled);
            }

            foreach (var enabled in _testMb.PropArrayEnabledScene)
            {
                TestUtils.AssertEnabledSceneState(enabled);
            }

            foreach (var enabled in _testMb.fieldListEnabledScene)
            {
                TestUtils.AssertEnabledSceneState(enabled);
            }

            foreach (var enabled in _testMb.PropListEnabledScene)
            {
                TestUtils.AssertEnabledSceneState(enabled);
            }
        }

        [Test]
        public void ProvidesExpectedState_DisabledScene()
        {
            TestUtils.AssertDisabledSceneState(_testMb.fieldDisabledScene);
            TestUtils.AssertDisabledSceneState(_testMb.PropDisabledScene);

            foreach (var disabled in _testMb.fieldArrayDisabledScene)
            {
                TestUtils.AssertDisabledSceneState(disabled);
            }

            foreach (var disabled in _testMb.PropArrayDisabledScene)
            {
                TestUtils.AssertDisabledSceneState(disabled);
            }

            foreach (var disabled in _testMb.fieldListDisabledScene)
            {
                TestUtils.AssertDisabledSceneState(disabled);
            }

            foreach (var disabled in _testMb.PropListDisabledScene)
            {
                TestUtils.AssertDisabledSceneState(disabled);
            }
        }

        [Test]
        public void ProvidesExpectedState_NotInBuildScene()
        {
            TestUtils.AssertNotInBuildSceneState(_testMb.fieldNotInBuildScene);
            TestUtils.AssertNotInBuildSceneState(_testMb.PropNotInBuildScene);

            foreach (var notInBuild in _testMb.fieldArrayNotInBuildScene)
            {
                TestUtils.AssertNotInBuildSceneState(notInBuild);
            }

            foreach (var notInBuild in _testMb.PropArrayNotInBuildScene)
            {
                TestUtils.AssertNotInBuildSceneState(notInBuild);
            }

            foreach (var notInBuild in _testMb.fieldListNotInBuildScene)
            {
                TestUtils.AssertNotInBuildSceneState(notInBuild);
            }

            foreach (var notInBuild in _testMb.PropListNotInBuildScene)
            {
                TestUtils.AssertNotInBuildSceneState(notInBuild);
            }
        }

        [Test]
        public void ProvidesExpectedState_Empty()
        {
            TestUtils.AssertEmptyState(_testMb.fieldEmpty);
            TestUtils.AssertEmptyState(_testMb.PropEmpty);

            foreach (var empty in _testMb.fieldArrayEmpty)
            {
                TestUtils.AssertEmptyState(empty);
            }

            foreach (var empty in _testMb.PropArrayEmpty)
            {
                TestUtils.AssertEmptyState(empty);
            }

            foreach (var empty in _testMb.fieldListEmpty)
            {
                TestUtils.AssertEmptyState(empty);
            }

            foreach (var empty in _testMb.PropListEmpty)
            {
                TestUtils.AssertEmptyState(empty);
            }
        }

        [Test]
        public void ProvidesExpectedState_DeletedScene()
        {
            TestUtils.AssertDeletedSceneState(_testMb.fieldDeletedScene);
            TestUtils.AssertDeletedSceneState(_testMb.PropDeletedScene);

            foreach (var deleted in _testMb.fieldArrayDeletedScene)
            {
                TestUtils.AssertDeletedSceneState(deleted);
            }

            foreach (var deleted in _testMb.PropArrayDeletedScene)
            {
                TestUtils.AssertDeletedSceneState(deleted);
            }

            foreach (var deleted in _testMb.fieldListDeletedScene)
            {
                TestUtils.AssertDeletedSceneState(deleted);
            }

            foreach (var deleted in _testMb.PropListDeletedScene)
            {
                TestUtils.AssertDeletedSceneState(deleted);
            }
        }

        [Test]
        public void ProvidesExpectedState_NotExisting()
        {
            TestUtils.AssertNotExistingState(_testMb.fieldNotExisting);
            TestUtils.AssertNotExistingState(_testMb.PropNotExisting);

            foreach (var invalid in _testMb.fieldArrayNotExisting)
            {
                TestUtils.AssertNotExistingState(invalid);
            }

            foreach (var invalid in _testMb.PropArrayNotExisting)
            {
                TestUtils.AssertNotExistingState(invalid);
            }

            foreach (var invalid in _testMb.fieldListNotExisting)
            {
                TestUtils.AssertNotExistingState(invalid);
            }

            foreach (var invalid in _testMb.PropListNotExisting)
            {
                TestUtils.AssertNotExistingState(invalid);
            }
        }

        [Test]
        public void ProvidesExpectedState_NotSceneAsset()
        {
            TestUtils.AssertNotSceneAssetState(_testMb.fieldNotSceneAsset);
            TestUtils.AssertNotSceneAssetState(_testMb.PropNotSceneAsset);

            foreach (var invalid in _testMb.fieldArrayNotSceneAsset)
            {
                TestUtils.AssertNotSceneAssetState(invalid);
            }

            foreach (var invalid in _testMb.PropArrayNotSceneAsset)
            {
                TestUtils.AssertNotSceneAssetState(invalid);
            }

            foreach (var invalid in _testMb.fieldListNotSceneAsset)
            {
                TestUtils.AssertNotSceneAssetState(invalid);
            }

            foreach (var invalid in _testMb.PropListNotSceneAsset)
            {
                TestUtils.AssertNotSceneAssetState(invalid);
            }
        }
    }
}
