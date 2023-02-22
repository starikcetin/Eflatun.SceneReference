using System.Collections.Generic;
using Eflatun.SceneReference.Editor.Utility;
using Eflatun.SceneReference.Utility;
using NUnit.Framework;

namespace Eflatun.SceneReference.Tests
{
    public class SceneGuidToPathMapProviderTests
    {
        private const string DisabledGuid = "7e37b14fa3517514a91937cec5cad27a";
        private const string DisabledPath = "Assets/TestUtils/TestScene_Disabled.unity";

        private const string EnabledGuid = "e3f2c1473b766c34ba5b37779d71787e";
        private const string EnabledPath = "Assets/TestUtils/TestScene_Enabled.unity";

        private const string NotInBuildGuid = "63c386231869c904c9b701dd79268476";
        private const string NotInBuildPath = "Assets/TestUtils/TestScene_NotInBuild.unity";

        [Test]
        public void SceneGuidToPathMap_ContainsUtilScenes()
        {
            Assert.IsTrue(SceneGuidToPathMapProvider.SceneGuidToPathMap.ContainsKey(EnabledGuid));
            Assert.AreEqual(EnabledPath, SceneGuidToPathMapProvider.SceneGuidToPathMap[EnabledGuid]);

            Assert.IsTrue(SceneGuidToPathMapProvider.SceneGuidToPathMap.ContainsKey(DisabledGuid));
            Assert.AreEqual(DisabledPath, SceneGuidToPathMapProvider.SceneGuidToPathMap[DisabledGuid]);

            Assert.IsTrue(SceneGuidToPathMapProvider.SceneGuidToPathMap.ContainsKey(NotInBuildGuid));
            Assert.AreEqual(NotInBuildPath, SceneGuidToPathMapProvider.SceneGuidToPathMap[NotInBuildGuid]);
        }

        [Test]
        public void SceneGuidToPathMap_DoesNotContainInvalidGuids()
        {
            Assert.IsFalse(SceneGuidToPathMapProvider.SceneGuidToPathMap.ContainsKey("00000000000000000000000000000000"));
            Assert.IsFalse(SceneGuidToPathMapProvider.SceneGuidToPathMap.ContainsKey(""));
            Assert.IsFalse(SceneGuidToPathMapProvider.SceneGuidToPathMap.ContainsKey("eb2424ee6d"));
            Assert.IsFalse(SceneGuidToPathMapProvider.SceneGuidToPathMap.ContainsKey("eb2424ee6dbe9094e9637f087446b90fabcde1234"));
            Assert.IsFalse(SceneGuidToPathMapProvider.SceneGuidToPathMap.ContainsKey("xb2424ee6dbe9094e9637f087446b90f"));
        }

        [Test]
        public void SceneGuidToPathMap_AllEntriesAreValid()
        {
            foreach (var (key, value) in SceneGuidToPathMapProvider.SceneGuidToPathMap)
            {
                Assert.IsTrue(key.IsValidGuidHex());
                Assert.IsTrue(value.IsSceneAssetPath());
                Assert.IsTrue(value.StartsWith("Assets/"));
            }
        }

        [Test]
        public void SceneGuidToPathMap_AllEntriesAreUnique()
        {
            CollectionAssert.AllItemsAreUnique(SceneGuidToPathMapProvider.SceneGuidToPathMap.Keys);
            CollectionAssert.AllItemsAreUnique(SceneGuidToPathMapProvider.SceneGuidToPathMap.Values);
        }

        [Test]
        public void DirectAssign_Works()
        {
            // cleanup
            var toRestore = new Dictionary<string, string>(SceneGuidToPathMapProvider.SceneGuidToPathMap);

            var expected = new Dictionary<string, string>()
            {
                {"foo", "a"},
                {"bar", "b"},
                {"baz", "c"},
            };
            SceneGuidToPathMapProvider.DirectAssign(expected);
            CollectionAssert.AreEquivalent(expected, SceneGuidToPathMapProvider.SceneGuidToPathMap);

            // cleanup
            SceneGuidToPathMapProvider.DirectAssign(toRestore);
        }
    }
}
