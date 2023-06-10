﻿using Eflatun.SceneReference.Exceptions;
using Eflatun.SceneReference.Tests.Runtime.Utils;
using NUnit.Framework;
using UnityEditor;

namespace Eflatun.SceneReference.Tests.Runtime
{
    public class SceneReferenceInCodeCreationTests
    {
        [Test]
        public void DefaultConstructor_ProvidesExpectedState()
        {
            var defaultRef = new SceneReference();
            TestUtils.AssertEmptyState(defaultRef);
        }

        [Test]
        public void GuidConstructor_ProvidesExpectedState()
        {
            var enabledRef = new SceneReference(TestUtils.EnabledSceneGuid);
            TestUtils.AssertEnabledSceneState(enabledRef);

            var disabledRef = new SceneReference(TestUtils.DisabledSceneGuid);
            TestUtils.AssertDisabledSceneState(disabledRef);

            var notInBuildRef = new SceneReference(TestUtils.NotInBuildSceneGuid);
            TestUtils.AssertNotInBuildSceneState(notInBuildRef);
        }

        [Test]
        public void GuidConstructor_ThrowsForInvalidArguments()
        {
            Assert.Throws<SceneReferenceCreationException>(() => _ = new SceneReference((string)null));
            Assert.Throws<SceneReferenceCreationException>(() => _ = new SceneReference("     "));
            Assert.Throws<SceneReferenceCreationException>(() => _ = new SceneReference(TestUtils.DeletedSceneGuid));
            Assert.Throws<SceneReferenceCreationException>(() => _ = new SceneReference(TestUtils.NotExistingGuid));
            Assert.Throws<SceneReferenceCreationException>(() => _ = new SceneReference(TestUtils.NotSceneAssetGuid));
        }

        [Test]
        public void AssetConstructor_ProvidesExpectedState()
        {
#if UNITY_EDITOR
            var enabledAsset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(TestUtils.EnabledScenePath);
            var enabledRef = new SceneReference(enabledAsset);
            TestUtils.AssertEnabledSceneState(enabledRef);

            var disabledAsset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(TestUtils.DisabledScenePath);
            var disabledRef = new SceneReference(disabledAsset);
            TestUtils.AssertDisabledSceneState(disabledRef);

            var notInBuildAsset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(TestUtils.NotInBuildScenePath);
            var notInBuildRef = new SceneReference(notInBuildAsset);
            TestUtils.AssertNotInBuildSceneState(notInBuildRef);

#else // UNITY_EDITOR
            Assert.Ignore("The asset constructor is editor-only, so this test is meaningless outside the editor.");
#endif // UNITY_EDITOR
        }

        [Test]
        public void AssetConstructor_ThrowsForInvalidArgs()
        {
#if UNITY_EDITOR
            Assert.Throws<SceneReferenceCreationException>(() => _ = new SceneReference((UnityEngine.Object)null));
            Assert.Throws<SceneReferenceCreationException>(() => _ = new SceneReference(new UnityEngine.Object()));

            var notSceneAsset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(TestUtils.NotSceneAssetPath);
            Assert.Throws<SceneReferenceCreationException>(() => _ = new SceneReference(notSceneAsset));

#else // UNITY_EDITOR
            Assert.Ignore("The asset constructor is editor-only, so this test is meaningless outside the editor.");
#endif // UNITY_EDITOR
        }

        [Test]
        public void PathFactoryMethod_ProvidesExpectedState()
        {
            var enabledRef = SceneReference.FromScenePath(TestUtils.EnabledScenePath);
            TestUtils.AssertEnabledSceneState(enabledRef);

            var disabledRef = SceneReference.FromScenePath(TestUtils.DisabledScenePath);
            TestUtils.AssertDisabledSceneState(disabledRef);

            var notInBuildRef = SceneReference.FromScenePath(TestUtils.NotInBuildScenePath);
            TestUtils.AssertNotInBuildSceneState(notInBuildRef);
        }

        [Test]
        public void PathFactoryMethod_ThrowsForInvalidArgs()
        {
            Assert.Throws<SceneReferenceCreationException>(() => _ = SceneReference.FromScenePath(null));
            Assert.Throws<SceneReferenceCreationException>(() => _ = SceneReference.FromScenePath("    "));
            Assert.Throws<SceneReferenceCreationException>(() => _ = SceneReference.FromScenePath("foo/bar/baz"));
            Assert.Throws<SceneReferenceCreationException>(() => _ = SceneReference.FromScenePath(TestUtils.NotSceneAssetPath));
        }
    }
}
