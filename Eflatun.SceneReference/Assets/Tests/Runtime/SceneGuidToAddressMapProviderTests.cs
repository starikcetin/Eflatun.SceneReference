using Eflatun.SceneReference.Exceptions;
using Eflatun.SceneReference.Tests.Runtime.Utils;
using Eflatun.SceneReference.Utility;
using NUnit.Framework;
using System.Collections.Generic;

namespace Eflatun.SceneReference.Tests.Runtime
{
    public class SceneGuidToAddressMapProviderTests
    {
        [Test]
        public void FillWith_Works()
        {
            // cleanup
            var toRestore = SceneGuidToAddressMapProvider.SceneGuidToAddressMap.ToDictionary();

            var expected = new Dictionary<string, string>()
            {
                {"foo", "a"},
                {"bar", "b"},
                {"baz", "c"},
            };
            SceneGuidToAddressMapProvider.FillWith(expected);
            CollectionAssert.AreEquivalent(expected, SceneGuidToAddressMapProvider.SceneGuidToAddressMap);

            // cleanup
            SceneGuidToAddressMapProvider.FillWith(toRestore);
        }

        [Test]
        public void SceneGuidToAddressMap_ContainsSubjectScenes_WithAddressableSupport()
        {
            TestUtils.IgnoreIfAddressablesSupportIsDisabled();

            Assert.IsTrue(SceneGuidToAddressMapProvider.SceneGuidToAddressMap.ContainsKey(TestUtils.Addressable1SceneGuid));
            Assert.AreEqual(TestUtils.Addressable1SceneAddress, SceneGuidToAddressMapProvider.SceneGuidToAddressMap[TestUtils.Addressable1SceneGuid]);

            Assert.IsTrue(SceneGuidToAddressMapProvider.SceneGuidToAddressMap.ContainsKey(TestUtils.Addressable2SceneGuid));
            Assert.AreEqual(TestUtils.Addressable2SceneAddress, SceneGuidToAddressMapProvider.SceneGuidToAddressMap[TestUtils.Addressable2SceneGuid]);
        }

        [Test]
        public void SceneGuidToAddressMap_DoesNotContainInvalidGuids_WithAddressableSupport()
        {
            TestUtils.IgnoreIfAddressablesSupportIsDisabled();

            Assert.IsFalse(SceneGuidToAddressMapProvider.SceneGuidToAddressMap.ContainsKey(TestUtils.AllZeroGuid));
            Assert.IsFalse(SceneGuidToAddressMapProvider.SceneGuidToAddressMap.ContainsKey(""));
            Assert.IsFalse(SceneGuidToAddressMapProvider.SceneGuidToAddressMap.ContainsKey("eb2424ee6d"));
            Assert.IsFalse(SceneGuidToAddressMapProvider.SceneGuidToAddressMap.ContainsKey("eb2424ee6dbe9094e9637f087446b90fabcde1234"));
            Assert.IsFalse(SceneGuidToAddressMapProvider.SceneGuidToAddressMap.ContainsKey("xb2424ee6dbe9094e9637f087446b90f"));
        }

        [Test]
        public void SceneGuidToAddressMap_DoesNotContainNonAddressableScenes_WithAddressableSupport()
        {
            TestUtils.IgnoreIfAddressablesSupportIsDisabled();

            Assert.IsFalse(SceneGuidToAddressMapProvider.SceneGuidToAddressMap.ContainsKey(TestUtils.EnabledSceneGuid));
            Assert.IsFalse(SceneGuidToAddressMapProvider.SceneGuidToAddressMap.ContainsKey(TestUtils.DisabledSceneGuid));
            Assert.IsFalse(SceneGuidToAddressMapProvider.SceneGuidToAddressMap.ContainsKey(TestUtils.NotInBuildSceneGuid));
        }

        [Test]
        public void SceneGuidToAddressMap_AllEntriesAreUnique_WithAddressableSupport()
        {
            TestUtils.IgnoreIfAddressablesSupportIsDisabled();

            CollectionAssert.AllItemsAreUnique(SceneGuidToAddressMapProvider.SceneGuidToAddressMap);
            CollectionAssert.AllItemsAreUnique(SceneGuidToAddressMapProvider.SceneGuidToAddressMap.Keys);
            // NOTE: values don't have to be unique, as multiple assets can have the same address
        }

        [Test]
        public void GetGuidFromAddress_Works_WithAddressableSupport()
        {
            TestUtils.IgnoreIfAddressablesSupportIsDisabled();

            Assert.AreEqual(TestUtils.Addressable1SceneGuid, SceneGuidToAddressMapProvider.GetGuidFromAddress(TestUtils.Addressable1SceneAddress));
            Assert.AreEqual(TestUtils.Addressable2SceneGuid, SceneGuidToAddressMapProvider.GetGuidFromAddress(TestUtils.Addressable2SceneAddress));

            Assert.Throws<AddressNotFoundException>(() => SceneGuidToAddressMapProvider.GetGuidFromAddress(TestUtils.NonExistingAddress));
            Assert.Throws<AddressNotUniqueException>(() => SceneGuidToAddressMapProvider.GetGuidFromAddress(TestUtils.AddressableDuplicateAddressASceneAddress));
            Assert.Throws<AddressNotUniqueException>(() => SceneGuidToAddressMapProvider.GetGuidFromAddress(TestUtils.AddressableDuplicateAddressBSceneAddress));
        }

        [Test]
        public void TryGetGuidFromAddress_Works_WithAddressableSupport()
        {
            TestUtils.IgnoreIfAddressablesSupportIsDisabled();

            void Test(string address, bool expectedSuccess, string expectedGuid)
            {
                var success = SceneGuidToAddressMapProvider.TryGetGuidFromAddress(address, out var guid);

                Assert.AreEqual(expectedSuccess, success);
                Assert.AreEqual(expectedGuid, guid);
            }

            Test(TestUtils.Addressable1SceneAddress, true, TestUtils.Addressable1SceneGuid);
            Test(TestUtils.Addressable2SceneAddress, true, TestUtils.Addressable2SceneGuid);
            Test(TestUtils.NonExistingAddress, false, null);
            Test(TestUtils.AddressableDuplicateAddressASceneAddress, false, null);
            Test(TestUtils.AddressableDuplicateAddressBSceneAddress, false, null);
        }

        [Test]
        public void SceneGuidToAddressMap_IsEmpty_WithoutAddressableSupport()
        {
            TestUtils.IgnoreIfAddressablesSupportIsEnabled();

            CollectionAssert.IsEmpty(SceneGuidToAddressMapProvider.SceneGuidToAddressMap);
        }

        [Test]
        public void GetGuidFromAddress_Throws_WithoutAddressableSupport()
        {
            TestUtils.IgnoreIfAddressablesSupportIsEnabled();

            Assert.Throws<AddressablesSupportDisabledException>(() => SceneGuidToAddressMapProvider.GetGuidFromAddress(TestUtils.Addressable1SceneAddress));
            Assert.Throws<AddressablesSupportDisabledException>(() => SceneGuidToAddressMapProvider.GetGuidFromAddress(TestUtils.Addressable2SceneAddress));
            Assert.Throws<AddressablesSupportDisabledException>(() => SceneGuidToAddressMapProvider.GetGuidFromAddress(TestUtils.NonExistingAddress));
            Assert.Throws<AddressablesSupportDisabledException>(() => SceneGuidToAddressMapProvider.GetGuidFromAddress(TestUtils.AddressableDuplicateAddressASceneAddress));
            Assert.Throws<AddressablesSupportDisabledException>(() => SceneGuidToAddressMapProvider.GetGuidFromAddress(TestUtils.AddressableDuplicateAddressBSceneAddress));
        }

        [Test]
        public void TryGetGuidFromAddress_IsAlwaysNegative_WithoutAddressableSupport()
        {
            TestUtils.IgnoreIfAddressablesSupportIsEnabled();

            void Test(string address, bool expectedSuccess, string expectedGuid)
            {
                var success = SceneGuidToAddressMapProvider.TryGetGuidFromAddress(address, out var guid);

                Assert.AreEqual(expectedSuccess, success);
                Assert.AreEqual(expectedGuid, guid);
            }

            Test(TestUtils.Addressable1SceneAddress, false, null);
            Test(TestUtils.Addressable2SceneAddress, false, null);
            Test(TestUtils.NonExistingAddress, false, null);
            Test(TestUtils.AddressableDuplicateAddressASceneAddress, false, null);
            Test(TestUtils.AddressableDuplicateAddressBSceneAddress, false, null);
        }
    }
}
