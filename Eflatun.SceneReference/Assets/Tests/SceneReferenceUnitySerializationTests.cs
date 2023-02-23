using System.Collections;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Eflatun.SceneReference.Tests
{
    public class SceneReferenceUnitySerializationTests
    {
        private const string TestSceneContainerPath = "Assets/Tests/Utils/TestScene_Container.unity";

        private const string EnabledName = "TestScene_Enabled";
        private const string EnabledPath = "Assets/Tests/Utils/TestScene_Enabled.unity";
        private const int EnabledBuildIndex = 0;
        private const string EnabledGuid = "e3f2c1473b766c34ba5b37779d71787e";

        private const string DisabledName = "TestScene_Disabled";
        private const string DisabledPath = "Assets/Tests/Utils/TestScene_Disabled.unity";
        private const string DisabledGuid = "7e37b14fa3517514a91937cec5cad27a";

        private const string NotInBuildName = "TestScene_NotInBuild";
        private const string NotInBuildPath = "Assets/Tests/Utils/TestScene_NotInBuild.unity";
        private const string NotInBuildGuid = "63c386231869c904c9b701dd79268476";

        private const string AllZeroGuid = "00000000000000000000000000000000";

        private const string DeletedGuid = "69c1683d94db0cc469d86e4e865f9f5d";

        private const string InvalidGuid = "2bc1683d94d80cc269d85e4e8a5fcf5d";

        private Test _testMb;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            yield return SceneManager.LoadSceneAsync(TestSceneContainerPath, LoadSceneMode.Additive);
            _testMb = UnityEngine.Object.FindObjectOfType<Test>();
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            var scene = SceneManager.GetSceneByPath(TestSceneContainerPath);
            yield return SceneManager.UnloadSceneAsync(scene);
        }

        [Test]
        public void ProvidesExpectedState_Enabled()
        {
            void Test(SceneReference sceneRef)
            {
                Assert.AreEqual(EnabledName, sceneRef.Name);
                Assert.AreEqual(EnabledPath, sceneRef.Path);

#if UNITY_EDITOR
                Assert.AreEqual(EnabledName, sceneRef.sceneAsset.name);
#endif

                Assert.AreEqual(EnabledBuildIndex, sceneRef.BuildIndex);
                Assert.IsTrue(sceneRef.HasValue);
                Assert.AreEqual(EnabledGuid, sceneRef.AssetGuidHex);
                Assert.AreEqual(EnabledGuid, sceneRef.sceneAssetGuidHex);
                Assert.IsTrue(sceneRef.IsSafeToUse);
                Assert.IsTrue(sceneRef.IsInBuildAndEnabled);
                Assert.IsTrue(sceneRef.IsInSceneGuidToPathMap);
            }

            Test(_testMb.fieldEnabled);
            Test(_testMb.PropEnabled);

            foreach (var enabled in _testMb.fieldArrayEnabled)
            {
                Test(enabled);
            }

            foreach (var enabled in _testMb.PropArrayEnabled)
            {
                Test(enabled);
            }

            foreach (var enabled in _testMb.fieldListEnabled)
            {
                Test(enabled);
            }

            foreach (var enabled in _testMb.PropListEnabled)
            {
                Test(enabled);
            }
        }

        [Test]
        public void ProvidesExpectedState_Disabled()
        {
            void Test(SceneReference sceneRef)
            {
                Assert.AreEqual(DisabledName, sceneRef.Name);
                Assert.AreEqual(DisabledPath, sceneRef.Path);

#if UNITY_EDITOR
                Assert.AreEqual(DisabledName, sceneRef.sceneAsset.name);
#endif

                Assert.AreEqual(-1, sceneRef.BuildIndex);
                Assert.IsTrue(sceneRef.HasValue);
                Assert.AreEqual(DisabledGuid, sceneRef.AssetGuidHex);
                Assert.AreEqual(DisabledGuid, sceneRef.sceneAssetGuidHex);
                Assert.IsFalse(sceneRef.IsSafeToUse);
                Assert.IsFalse(sceneRef.IsInBuildAndEnabled);
                Assert.IsTrue(sceneRef.IsInSceneGuidToPathMap);
            }

            Test(_testMb.fieldDisabled);
            Test(_testMb.PropDisabled);

            foreach (var disabled in _testMb.fieldArrayDisabled)
            {
                Test(disabled);
            }

            foreach (var disabled in _testMb.PropArrayDisabled)
            {
                Test(disabled);
            }

            foreach (var disabled in _testMb.fieldListDisabled)
            {
                Test(disabled);
            }

            foreach (var disabled in _testMb.PropListDisabled)
            {
                Test(disabled);
            }
        }

        [Test]
        public void ProvidesExpectedState_NotInBuild()
        {
            void Test(SceneReference sceneRef)
            {
                Assert.AreEqual(NotInBuildName, sceneRef.Name);
                Assert.AreEqual(NotInBuildPath, sceneRef.Path);

#if UNITY_EDITOR
                Assert.AreEqual(NotInBuildName, sceneRef.sceneAsset.name);
#endif

                Assert.AreEqual(-1, sceneRef.BuildIndex);
                Assert.IsTrue(sceneRef.HasValue);
                Assert.AreEqual(NotInBuildGuid, sceneRef.AssetGuidHex);
                Assert.AreEqual(NotInBuildGuid, sceneRef.sceneAssetGuidHex);
                Assert.IsFalse(sceneRef.IsSafeToUse);
                Assert.IsFalse(sceneRef.IsInBuildAndEnabled);
                Assert.IsTrue(sceneRef.IsInSceneGuidToPathMap);
            }

            Test(_testMb.fieldNotInBuild);
            Test(_testMb.PropNotInBuild);

            foreach (var notInBuild in _testMb.fieldArrayNotInBuild)
            {
                Test(notInBuild);
            }

            foreach (var notInBuild in _testMb.PropArrayNotInBuild)
            {
                Test(notInBuild);
            }

            foreach (var notInBuild in _testMb.fieldListNotInBuild)
            {
                Test(notInBuild);
            }

            foreach (var notInBuild in _testMb.PropListNotInBuild)
            {
                Test(notInBuild);
            }
        }

        [Test]
        public void ProvidesExpectedState_Unassigned()
        {
            void Test(SceneReference sceneRef)
            {
                Assert.Throws<EmptySceneReferenceException>(() => _ = sceneRef.Name);
                Assert.Throws<EmptySceneReferenceException>(() => _ = sceneRef.Path);

#if UNITY_EDITOR
                Assert.IsFalse(!!sceneRef.sceneAsset);
#endif

                Assert.Throws<EmptySceneReferenceException>(() => _ = sceneRef.BuildIndex);
                Assert.IsFalse(sceneRef.HasValue);
                Assert.AreEqual(AllZeroGuid, sceneRef.AssetGuidHex);
                Assert.AreEqual(AllZeroGuid, sceneRef.sceneAssetGuidHex);
                Assert.IsFalse(sceneRef.IsSafeToUse);
                Assert.Throws<EmptySceneReferenceException>(() => _ = sceneRef.IsInBuildAndEnabled);
                Assert.Throws<EmptySceneReferenceException>(() => _ = sceneRef.IsInSceneGuidToPathMap);
            }

            Test(_testMb.fieldUnassigned);
            Test(_testMb.PropUnassigned);

            foreach (var unassigned in _testMb.fieldArrayUnassigned)
            {
                Test(unassigned);
            }

            foreach (var unassigned in _testMb.PropArrayUnassigned)
            {
                Test(unassigned);
            }

            foreach (var unassigned in _testMb.fieldListUnassigned)
            {
                Test(unassigned);
            }

            foreach (var unassigned in _testMb.PropListUnassigned)
            {
                Test(unassigned);
            }
        }

        [Test]
        public void ProvidesExpectedState_Empty()
        {
            void Test(SceneReference sceneRef)
            {
                Assert.Throws<EmptySceneReferenceException>(() => _ = sceneRef.Name);
                Assert.Throws<EmptySceneReferenceException>(() => _ = sceneRef.Path);

#if UNITY_EDITOR
                Assert.IsFalse(!!sceneRef.sceneAsset);
#endif

                Assert.Throws<EmptySceneReferenceException>(() => _ = sceneRef.BuildIndex);
                Assert.IsFalse(sceneRef.HasValue);
                Assert.AreEqual(AllZeroGuid, sceneRef.AssetGuidHex);
                Assert.AreEqual(AllZeroGuid, sceneRef.sceneAssetGuidHex);
                Assert.IsFalse(sceneRef.IsSafeToUse);
                Assert.Throws<EmptySceneReferenceException>(() => _ = sceneRef.IsInBuildAndEnabled);
                Assert.Throws<EmptySceneReferenceException>(() => _ = sceneRef.IsInSceneGuidToPathMap);
            }

            Test(_testMb.fieldEmpty);
            Test(_testMb.PropEmpty);

            foreach (var empty in _testMb.fieldArrayEmpty)
            {
                Test(empty);
            }

            foreach (var empty in _testMb.PropArrayEmpty)
            {
                Test(empty);
            }

            foreach (var empty in _testMb.fieldListEmpty)
            {
                Test(empty);
            }

            foreach (var empty in _testMb.PropListEmpty)
            {
                Test(empty);
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
                Assert.AreEqual(DeletedGuid, sceneRef.AssetGuidHex);
                Assert.AreEqual(DeletedGuid, sceneRef.sceneAssetGuidHex);
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
        public void ProvidesExpectedState_Invalid()
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
                Assert.AreEqual(InvalidGuid, sceneRef.AssetGuidHex);
                Assert.AreEqual(InvalidGuid, sceneRef.sceneAssetGuidHex);
                Assert.IsFalse(sceneRef.IsSafeToUse);
                Assert.Throws<InvalidSceneReferenceException>(() => _ = sceneRef.IsInBuildAndEnabled);
                Assert.IsFalse(sceneRef.IsInSceneGuidToPathMap);
            }

            Test(_testMb.fieldInvalid);
            Test(_testMb.PropInvalid);

            foreach (var invalid in _testMb.fieldArrayInvalid)
            {
                Test(invalid);
            }

            foreach (var invalid in _testMb.PropArrayInvalid)
            {
                Test(invalid);
            }

            foreach (var invalid in _testMb.fieldListInvalid)
            {
                Test(invalid);
            }

            foreach (var invalid in _testMb.PropListInvalid)
            {
                Test(invalid);
            }
        }
    }
}
