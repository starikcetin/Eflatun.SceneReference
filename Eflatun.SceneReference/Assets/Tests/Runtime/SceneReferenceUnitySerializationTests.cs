using System.Collections;
using Eflatun.SceneReference.Tests.Runtime.Utils;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Eflatun.SceneReference.Tests.Runtime
{
    public class SceneReferenceUnitySerializationTests
    {
        private Test _testMb;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            yield return SceneManager.LoadSceneAsync(TestUtils.TestSceneContainerPath, LoadSceneMode.Additive);
            _testMb = UnityEngine.Object.FindObjectOfType<Test>();
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            var scene = SceneManager.GetSceneByPath(TestUtils.TestSceneContainerPath);
            yield return SceneManager.UnloadSceneAsync(scene);
            _testMb = null;
        }

        [Test]
        public void ProvidesExpectedState_Enabled()
        {
            TestUtils.AssertEnabledSceneReferenceState(_testMb.fieldEnabled);
            TestUtils.AssertEnabledSceneReferenceState(_testMb.PropEnabled);

            foreach (var enabled in _testMb.fieldArrayEnabled)
            {
                TestUtils.AssertEnabledSceneReferenceState(enabled);
            }

            foreach (var enabled in _testMb.PropArrayEnabled)
            {
                TestUtils.AssertEnabledSceneReferenceState(enabled);
            }

            foreach (var enabled in _testMb.fieldListEnabled)
            {
                TestUtils.AssertEnabledSceneReferenceState(enabled);
            }

            foreach (var enabled in _testMb.PropListEnabled)
            {
                TestUtils.AssertEnabledSceneReferenceState(enabled);
            }
        }

        [Test]
        public void ProvidesExpectedState_Disabled()
        {
            TestUtils.AssertDisabledSceneReferenceState(_testMb.fieldDisabled);
            TestUtils.AssertDisabledSceneReferenceState(_testMb.PropDisabled);

            foreach (var disabled in _testMb.fieldArrayDisabled)
            {
                TestUtils.AssertDisabledSceneReferenceState(disabled);
            }

            foreach (var disabled in _testMb.PropArrayDisabled)
            {
                TestUtils.AssertDisabledSceneReferenceState(disabled);
            }

            foreach (var disabled in _testMb.fieldListDisabled)
            {
                TestUtils.AssertDisabledSceneReferenceState(disabled);
            }

            foreach (var disabled in _testMb.PropListDisabled)
            {
                TestUtils.AssertDisabledSceneReferenceState(disabled);
            }
        }

        [Test]
        public void ProvidesExpectedState_NotInBuild()
        {
            TestUtils.AssertNotInBuildSceneReferenceState(_testMb.fieldNotInBuild);
            TestUtils.AssertNotInBuildSceneReferenceState(_testMb.PropNotInBuild);

            foreach (var notInBuild in _testMb.fieldArrayNotInBuild)
            {
                TestUtils.AssertNotInBuildSceneReferenceState(notInBuild);
            }

            foreach (var notInBuild in _testMb.PropArrayNotInBuild)
            {
                TestUtils.AssertNotInBuildSceneReferenceState(notInBuild);
            }

            foreach (var notInBuild in _testMb.fieldListNotInBuild)
            {
                TestUtils.AssertNotInBuildSceneReferenceState(notInBuild);
            }

            foreach (var notInBuild in _testMb.PropListNotInBuild)
            {
                TestUtils.AssertNotInBuildSceneReferenceState(notInBuild);
            }
        }

        [Test]
        public void ProvidesExpectedState_Unassigned()
        {
            TestUtils.AssertEmptySceneReferenceState(_testMb.fieldUnassigned);
            TestUtils.AssertEmptySceneReferenceState(_testMb.PropUnassigned);

            foreach (var unassigned in _testMb.fieldArrayUnassigned)
            {
                TestUtils.AssertEmptySceneReferenceState(unassigned);
            }

            foreach (var unassigned in _testMb.PropArrayUnassigned)
            {
                TestUtils.AssertEmptySceneReferenceState(unassigned);
            }

            foreach (var unassigned in _testMb.fieldListUnassigned)
            {
                TestUtils.AssertEmptySceneReferenceState(unassigned);
            }

            foreach (var unassigned in _testMb.PropListUnassigned)
            {
                TestUtils.AssertEmptySceneReferenceState(unassigned);
            }
        }

        [Test]
        public void ProvidesExpectedState_Empty()
        {
            TestUtils.AssertEmptySceneReferenceState(_testMb.fieldEmpty);
            TestUtils.AssertEmptySceneReferenceState(_testMb.PropEmpty);

            foreach (var empty in _testMb.fieldArrayEmpty)
            {
                TestUtils.AssertEmptySceneReferenceState(empty);
            }

            foreach (var empty in _testMb.PropArrayEmpty)
            {
                TestUtils.AssertEmptySceneReferenceState(empty);
            }

            foreach (var empty in _testMb.fieldListEmpty)
            {
                TestUtils.AssertEmptySceneReferenceState(empty);
            }

            foreach (var empty in _testMb.PropListEmpty)
            {
                TestUtils.AssertEmptySceneReferenceState(empty);
            }
        }

        [Test]
        public void ProvidesExpectedState_Deleted()
        {
            void Test(SceneReference sceneRef)
            {
                Assert.Throws<InvalidSceneReferenceException>(() => _ = sceneRef.Name);
                Assert.Throws<InvalidSceneReferenceException>(() => _ = sceneRef.Path);

#if UNITY_EDITOR
                Assert.IsFalse(!!sceneRef.sceneAsset);
#endif

                Assert.Throws<InvalidSceneReferenceException>(() => _ = sceneRef.BuildIndex);
                Assert.IsTrue(sceneRef.HasValue);
                Assert.AreEqual(TestUtils.DeletedSceneGuid, sceneRef.AssetGuidHex);
                Assert.AreEqual(TestUtils.DeletedSceneGuid, sceneRef.sceneAssetGuidHex);
                Assert.IsFalse(sceneRef.IsSafeToUse);
                Assert.Throws<InvalidSceneReferenceException>(() => _ = sceneRef.IsInBuildAndEnabled);
                Assert.IsFalse(sceneRef.IsInSceneGuidToPathMap);
            }

            Test(_testMb.fieldDeleted);
            Test(_testMb.PropDeleted);

            foreach (var deleted in _testMb.fieldArrayDeleted)
            {
                Test(deleted);
            }

            foreach (var deleted in _testMb.PropArrayDeleted)
            {
                Test(deleted);
            }

            foreach (var deleted in _testMb.fieldListDeleted)
            {
                Test(deleted);
            }

            foreach (var deleted in _testMb.PropListDeleted)
            {
                Test(deleted);
            }
        }

        [Test]
        public void ProvidesExpectedState_NotExisting()
        {
            void Test(SceneReference sceneRef)
            {
                Assert.Throws<InvalidSceneReferenceException>(() => _ = sceneRef.Name);
                Assert.Throws<InvalidSceneReferenceException>(() => _ = sceneRef.Path);

#if UNITY_EDITOR
                Assert.IsFalse(!!sceneRef.sceneAsset);
#endif

                Assert.Throws<InvalidSceneReferenceException>(() => _ = sceneRef.BuildIndex);
                Assert.IsTrue(sceneRef.HasValue);
                Assert.AreEqual(TestUtils.NotExistingGuid, sceneRef.AssetGuidHex);
                Assert.AreEqual(TestUtils.NotExistingGuid, sceneRef.sceneAssetGuidHex);
                Assert.IsFalse(sceneRef.IsSafeToUse);
                Assert.Throws<InvalidSceneReferenceException>(() => _ = sceneRef.IsInBuildAndEnabled);
                Assert.IsFalse(sceneRef.IsInSceneGuidToPathMap);
            }

            Test(_testMb.fieldNotExisting);
            Test(_testMb.PropNotExisting);

            foreach (var invalid in _testMb.fieldArrayNotExisting)
            {
                Test(invalid);
            }

            foreach (var invalid in _testMb.PropArrayNotExisting)
            {
                Test(invalid);
            }

            foreach (var invalid in _testMb.fieldListNotExisting)
            {
                Test(invalid);
            }

            foreach (var invalid in _testMb.PropListNotExisting)
            {
                Test(invalid);
            }
        }
    }
}
