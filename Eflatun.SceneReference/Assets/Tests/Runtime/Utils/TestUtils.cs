using System.IO;
using NUnit.Framework;
using UnityEngine.SceneManagement;

namespace Eflatun.SceneReference.Tests.Runtime.Utils
{
    public static class TestUtils
    {
        // NOTE: Do not hardcode the build indices, as they seem to change on test builds.
        // Possibly due to Unity inserting their test scene at the beginning of the list.

        public const string TestSceneContainerPath = "Assets/Tests/Runtime/Utils/TestScene_Container.unity";

        public const string EnabledSceneName = "TestScene_Enabled";
        public const string EnabledScenePath = "Assets/Tests/Runtime/Utils/TestScene_Enabled.unity";
        public static int EnabledSceneBuildIndex => SceneUtility.GetBuildIndexByScenePath(EnabledScenePath);
        public const string EnabledSceneGuid = "e3f2c1473b766c34ba5b37779d71787e";

        public const string DisabledSceneName = "TestScene_Disabled";
        public const string DisabledScenePath = "Assets/Tests/Runtime/Utils/TestScene_Disabled.unity";
        public static int DisabledSceneBuildIndex => SceneUtility.GetBuildIndexByScenePath(DisabledScenePath);
        public const string DisabledSceneGuid = "7e37b14fa3517514a91937cec5cad27a";

        public const string NotInBuildSceneName = "TestScene_NotInBuild";
        public const string NotInBuildScenePath = "Assets/Tests/Runtime/Utils/TestScene_NotInBuild.unity";
        public static int NotInBuildSceneBuildIndex => SceneUtility.GetBuildIndexByScenePath(NotInBuildScenePath);
        public const string NotInBuildSceneGuid = "63c386231869c904c9b701dd79268476";

        public const string AllZeroGuid = "00000000000000000000000000000000";

        public const string DeletedSceneGuid = "69c1683d94db0cc469d86e4e865f9f5d";

        public const string NotExistingGuid = "2bc1683d94d80cc269d85e4e8a5fcf5d";

        public const string NotSceneAssetGuid = "99d2aa5f58f54c44fba8671b66be5259";
        public const string NotSceneAssetPath = "Assets/Tests/Runtime/Utils/TestMaterial.mat";

        public static void AssertEnabledSceneReferenceState(SceneReference sr)
        {
            Assert.AreEqual(EnabledSceneName, sr.Name);
            Assert.AreEqual(EnabledScenePath, sr.Path);

#if UNITY_EDITOR
            Assert.AreEqual(EnabledSceneName, sr.sceneAsset.name);
#endif

            Assert.AreEqual(EnabledSceneBuildIndex, sr.BuildIndex);
            Assert.IsTrue(sr.HasValue);
            Assert.AreEqual(EnabledSceneGuid, sr.AssetGuidHex);
            Assert.AreEqual(EnabledSceneGuid, sr.sceneAssetGuidHex);
            Assert.IsTrue(sr.IsSafeToUse);
            Assert.IsTrue(sr.IsInBuildAndEnabled);
            Assert.IsTrue(sr.IsInSceneGuidToPathMap);
        }

        public static void AssertDisabledSceneReferenceState(SceneReference sr)
        {
            Assert.AreEqual(DisabledSceneName, sr.Name);
            Assert.AreEqual(DisabledScenePath, sr.Path);

#if UNITY_EDITOR
            Assert.AreEqual(DisabledSceneName, sr.sceneAsset.name);
#endif

            Assert.AreEqual(DisabledSceneBuildIndex, sr.BuildIndex);
            Assert.IsTrue(sr.HasValue);
            Assert.AreEqual(DisabledSceneGuid, sr.AssetGuidHex);
            Assert.AreEqual(DisabledSceneGuid, sr.sceneAssetGuidHex);

// TODO: Unity seems to be enabling all scenes before making a test build.
// Figure out a way to disable that behaviour and then get rid of this define check.
#if UNITY_EDITOR
            Assert.IsFalse(sr.IsSafeToUse);
            Assert.IsFalse(sr.IsInBuildAndEnabled);
#else // UNITY_EDITOR
            Assert.IsTrue(sr.IsSafeToUse);
            Assert.IsTrue(sr.IsInBuildAndEnabled);
#endif // UNITY_EDITOR

            Assert.IsTrue(sr.IsInSceneGuidToPathMap);
        }

        public static void AssertNotInBuildSceneReferenceState(SceneReference sr)
        {
            Assert.AreEqual(NotInBuildSceneName, sr.Name);
            Assert.AreEqual(NotInBuildScenePath, sr.Path);

#if UNITY_EDITOR
            Assert.AreEqual(NotInBuildSceneName, sr.sceneAsset.name);
#endif

            Assert.AreEqual(NotInBuildSceneBuildIndex, sr.BuildIndex);
            Assert.IsTrue(sr.HasValue);
            Assert.AreEqual(NotInBuildSceneGuid, sr.AssetGuidHex);
            Assert.AreEqual(NotInBuildSceneGuid, sr.sceneAssetGuidHex);
            Assert.IsFalse(sr.IsSafeToUse);
            Assert.IsFalse(sr.IsInBuildAndEnabled);
            Assert.IsTrue(sr.IsInSceneGuidToPathMap);
        }

        public static void AssertEmptySceneReferenceState(SceneReference sr)
        {
            Assert.Throws<EmptySceneReferenceException>(() => _ = sr.Name);
            Assert.Throws<EmptySceneReferenceException>(() => _ = sr.Path);

#if UNITY_EDITOR
            Assert.IsFalse(!!sr.sceneAsset);
#endif // UNITY_EDITOR

            Assert.Throws<EmptySceneReferenceException>(() => _ = sr.BuildIndex);
            Assert.IsFalse(sr.HasValue);
            Assert.AreEqual(AllZeroGuid, sr.AssetGuidHex);
            Assert.AreEqual(AllZeroGuid, sr.sceneAssetGuidHex);
            Assert.IsFalse(sr.IsSafeToUse);
            Assert.Throws<EmptySceneReferenceException>(() => _ = sr.IsInBuildAndEnabled);
            Assert.Throws<EmptySceneReferenceException>(() => _ = sr.IsInSceneGuidToPathMap);
        }

        public static bool IsSceneAssetPath(string path)
        {
            return Path.GetExtension(path) == ".unity";
        }
    }
}
