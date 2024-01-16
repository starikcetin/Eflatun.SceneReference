using Eflatun.SceneReference.Tests.Runtime.Utils;
using Eflatun.SceneReference.Utility;
using NUnit.Framework;
using System.Collections.Generic;

namespace Eflatun.SceneReference.Tests.Runtime
{
    public class SceneGuidToPathMapProviderTests
    {
        [Test]
        public void SceneGuidToPathMap_ContainsSubjectScenes()
        {
            Assert.IsTrue(SceneGuidToPathMapProvider.SceneGuidToPathMap.ContainsKey(TestUtils.EnabledSceneGuid));
            Assert.AreEqual(TestUtils.EnabledScenePath, SceneGuidToPathMapProvider.SceneGuidToPathMap[TestUtils.EnabledSceneGuid]);

            Assert.IsTrue(SceneGuidToPathMapProvider.SceneGuidToPathMap.ContainsKey(TestUtils.DisabledSceneGuid));
            Assert.AreEqual(TestUtils.DisabledScenePath, SceneGuidToPathMapProvider.SceneGuidToPathMap[TestUtils.DisabledSceneGuid]);

            Assert.IsTrue(SceneGuidToPathMapProvider.SceneGuidToPathMap.ContainsKey(TestUtils.NotInBuildSceneGuid));
            Assert.AreEqual(TestUtils.NotInBuildScenePath, SceneGuidToPathMapProvider.SceneGuidToPathMap[TestUtils.NotInBuildSceneGuid]);
        }

        [Test]
        public void SceneGuidToPathMap_DoesNotContainInvalidGuids()
        {
            Assert.IsFalse(SceneGuidToPathMapProvider.SceneGuidToPathMap.ContainsKey(TestUtils.AllZeroGuid));
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
                Assert.IsTrue(key.IsValidGuid());
                Assert.IsTrue(TestUtils.IsScenePath(value));
                Assert.IsTrue(value.StartsWith("Assets/") || value.StartsWith("Packages/"));
            }
        }

        [Test]
        public void SceneGuidToPathMap_AllEntriesAreUnique()
        {
            CollectionAssert.AllItemsAreUnique(SceneGuidToPathMapProvider.SceneGuidToPathMap);
            CollectionAssert.AllItemsAreUnique(SceneGuidToPathMapProvider.SceneGuidToPathMap.Keys);
            CollectionAssert.AllItemsAreUnique(SceneGuidToPathMapProvider.SceneGuidToPathMap.Values);
        }

        [Test]
        public void FillWith_Works()
        {
            // cleanup
            var toRestore = SceneGuidToPathMapProvider.SceneGuidToPathMap.ToDictionary();

            var expected = new Dictionary<string, string>()
            {
                {"foo", "a"},
                {"bar", "b"},
                {"baz", "c"},
            };
            SceneGuidToPathMapProvider.FillWith(expected);
            CollectionAssert.AreEquivalent(expected, SceneGuidToPathMapProvider.SceneGuidToPathMap);

            // cleanup
            SceneGuidToPathMapProvider.FillWith(toRestore);
        }

        [Test]
        public void SceneGuidToPathMap_And_ScenePathToGuidMap_AreEquivalent()
        {
            var g2p = SceneGuidToPathMapProvider.SceneGuidToPathMap;
            var p2g = SceneGuidToPathMapProvider.ScenePathToGuidMap;

            Assert.AreEqual(g2p.Count, p2g.Count);
            CollectionAssert.AreEquivalent(g2p.Keys, p2g.Values);
            CollectionAssert.AreEquivalent(g2p.Values, p2g.Keys);

            foreach (var (guidFromG2P, pathFromG2P) in g2p)
            {
                Assert.AreEqual(guidFromG2P, p2g[pathFromG2P]);
            }

            foreach (var (pathFromP2G, guidFromP2G) in p2g)
            {
                Assert.AreEqual(pathFromP2G, g2p[guidFromP2G]);
            }
        }
    }
}
