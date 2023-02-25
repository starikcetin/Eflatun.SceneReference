using Eflatun.SceneReference.Tests.Utils;
using NUnit.Framework;
using UnityEditor;

namespace Eflatun.SceneReference.Tests
{
    public class SceneReferenceInCodeCreationTests
    {
        [Test]
        public void DefaultConstructor_ProvidesExpectedState()
        {
            var defaultRef = new SceneReference();
            TestUtils.AssertEmptySceneReferenceState(defaultRef);
        }

        [Test]
        public void GuidConstructor_ProvidesExpectedState()
        {
            var enabledRef = new SceneReference(TestUtils.EnabledSceneGuid);
            TestUtils.AssertEnabledSceneReferenceState(enabledRef);

            var disabledRef = new SceneReference(TestUtils.DisabledSceneGuid);
            TestUtils.AssertDisabledSceneReferenceState(disabledRef);

            var notInBuildRef = new SceneReference(TestUtils.NotInBuildSceneGuid);
            TestUtils.AssertNotInBuildSceneReferenceState(notInBuildRef);
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
            TestUtils.AssertEnabledSceneReferenceState(enabledRef);

            var disabledAsset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(TestUtils.DisabledScenePath);
            var disabledRef = new SceneReference(disabledAsset);
            TestUtils.AssertDisabledSceneReferenceState(disabledRef);

            var notInBuildAsset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(TestUtils.NotInBuildScenePath);
            var notInBuildRef = new SceneReference(notInBuildAsset);
            TestUtils.AssertNotInBuildSceneReferenceState(notInBuildRef);

#else // UNITY_EDITOR
            // The asset constructor is editor-only, so this test is meaningless outside the editor.
            Assert.Pass();
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
            // The asset constructor is editor-only, so this test is meaningless outside the editor.
            Assert.Pass();
#endif // UNITY_EDITOR
        }

        [Test]
        public void PathFactoryMethod_ProvidesExpectedState()
        {
            var enabledRef = SceneReference.FromScenePath(TestUtils.EnabledScenePath);
            TestUtils.AssertEnabledSceneReferenceState(enabledRef);

            var disabledRef = SceneReference.FromScenePath(TestUtils.DisabledScenePath);
            TestUtils.AssertDisabledSceneReferenceState(disabledRef);

            var notInBuildRef = SceneReference.FromScenePath(TestUtils.NotInBuildScenePath);
            TestUtils.AssertNotInBuildSceneReferenceState(notInBuildRef);
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
