using System.Collections.Generic;
using Eflatun.SceneReference.Exceptions;
using Eflatun.SceneReference.Tests.Runtime.Utils;
using NUnit.Framework;

namespace Eflatun.SceneReference.Tests.Runtime
{
    public class AddressableSceneGuidToAddressMapProviderTests
    {
        [Test]
        public void DirectAssign_Works()
        {
            // cleanup
            var toRestore = new Dictionary<string, string>(AddressableSceneGuidToAddressMapProvider.AddressableSceneGuidToAddressMap);

            var expected = new Dictionary<string, string>()
            {
                {"foo", "a"},
                {"bar", "b"},
                {"baz", "c"},
            };
            AddressableSceneGuidToAddressMapProvider.DirectAssign(expected);
            CollectionAssert.AreEquivalent(expected, AddressableSceneGuidToAddressMapProvider.AddressableSceneGuidToAddressMap);

            // cleanup
            AddressableSceneGuidToAddressMapProvider.DirectAssign(toRestore);
        }

        [Test]
        public void AddressableSceneGuidToAddressMap_ContainsSubjectScenes_WithAddressableSupport()
        {
            TestUtils.IgnoreIfAddressablesSupportIsDisabled();

            Assert.IsTrue(AddressableSceneGuidToAddressMapProvider.AddressableSceneGuidToAddressMap.ContainsKey(TestUtils.Addressable1SceneGuid));
            Assert.AreEqual(TestUtils.Addressable1SceneAddress, AddressableSceneGuidToAddressMapProvider.AddressableSceneGuidToAddressMap[TestUtils.Addressable1SceneGuid]);

            Assert.IsTrue(AddressableSceneGuidToAddressMapProvider.AddressableSceneGuidToAddressMap.ContainsKey(TestUtils.Addressable2SceneGuid));
            Assert.AreEqual(TestUtils.Addressable2SceneAddress, AddressableSceneGuidToAddressMapProvider.AddressableSceneGuidToAddressMap[TestUtils.Addressable2SceneGuid]);
        }

        [Test]
        public void AddressableSceneGuidToAddressMap_DoesNotContainInvalidGuids_WithAddressableSupport()
        {
            TestUtils.IgnoreIfAddressablesSupportIsDisabled();

            Assert.IsFalse(AddressableSceneGuidToAddressMapProvider.AddressableSceneGuidToAddressMap.ContainsKey(TestUtils.AllZeroGuid));
            Assert.IsFalse(AddressableSceneGuidToAddressMapProvider.AddressableSceneGuidToAddressMap.ContainsKey(""));
            Assert.IsFalse(AddressableSceneGuidToAddressMapProvider.AddressableSceneGuidToAddressMap.ContainsKey("eb2424ee6d"));
            Assert.IsFalse(AddressableSceneGuidToAddressMapProvider.AddressableSceneGuidToAddressMap.ContainsKey("eb2424ee6dbe9094e9637f087446b90fabcde1234"));
            Assert.IsFalse(AddressableSceneGuidToAddressMapProvider.AddressableSceneGuidToAddressMap.ContainsKey("xb2424ee6dbe9094e9637f087446b90f"));
        }

        [Test]
        public void AddressableSceneGuidToAddressMap_DoesNotContainNonAddressableScenes_WithAddressableSupport()
        {
            TestUtils.IgnoreIfAddressablesSupportIsDisabled();

            Assert.IsFalse(AddressableSceneGuidToAddressMapProvider.AddressableSceneGuidToAddressMap.ContainsKey(TestUtils.EnabledSceneGuid));
            Assert.IsFalse(AddressableSceneGuidToAddressMapProvider.AddressableSceneGuidToAddressMap.ContainsKey(TestUtils.DisabledSceneGuid));
            Assert.IsFalse(AddressableSceneGuidToAddressMapProvider.AddressableSceneGuidToAddressMap.ContainsKey(TestUtils.NotInBuildSceneGuid));
        }

        [Test]
        public void AddressableSceneGuidToAddressMap_AllEntriesAreUnique_WithAddressableSupport()
        {
            TestUtils.IgnoreIfAddressablesSupportIsDisabled();

            CollectionAssert.AllItemsAreUnique(AddressableSceneGuidToAddressMapProvider.AddressableSceneGuidToAddressMap);
            CollectionAssert.AllItemsAreUnique(AddressableSceneGuidToAddressMapProvider.AddressableSceneGuidToAddressMap.Keys);
            // NOTE: values don't have to be unique, as multiple assets can have the same address
        }

        [Test]
        public void GetGuidFromAddress_Works_WithAddressableSupport()
        {
            TestUtils.IgnoreIfAddressablesSupportIsDisabled();

            Assert.AreEqual(TestUtils.Addressable1SceneGuid, AddressableSceneGuidToAddressMapProvider.GetGuidFromAddress(TestUtils.Addressable1SceneAddress));
            Assert.AreEqual(TestUtils.Addressable2SceneGuid, AddressableSceneGuidToAddressMapProvider.GetGuidFromAddress(TestUtils.Addressable2SceneAddress));

            Assert.Throws<AddressNotFoundException>(() => AddressableSceneGuidToAddressMapProvider.GetGuidFromAddress(TestUtils.NonExistingAddress));
            Assert.Throws<AddressNotUniqueException>(() => AddressableSceneGuidToAddressMapProvider.GetGuidFromAddress(TestUtils.AddressableDuplicateAddressASceneAddress));
            Assert.Throws<AddressNotUniqueException>(() => AddressableSceneGuidToAddressMapProvider.GetGuidFromAddress(TestUtils.AddressableDuplicateAddressBSceneAddress));
        }

        [Test]
        public void TryGetGuidFromAddress_Works_WithAddressableSupport()
        {
            TestUtils.IgnoreIfAddressablesSupportIsDisabled();

            void Test(string address, bool expectedSuccess, string expectedGuid)
            {
                var success = AddressableSceneGuidToAddressMapProvider.TryGetGuidFromAddress(address, out var guid);

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
        public void AddressableSceneGuidToAddressMap_IsEmpty_WithoutAddressableSupport()
        {
            TestUtils.IgnoreIfAddressablesSupportIsEnabled();

            CollectionAssert.IsEmpty(AddressableSceneGuidToAddressMapProvider.AddressableSceneGuidToAddressMap);
        }

        [Test]
        public void GetGuidFromAddress_Throws_WithoutAddressableSupport()
        {
            TestUtils.IgnoreIfAddressablesSupportIsEnabled();

            Assert.Throws<AddressablesSupportDisabledException>(() => AddressableSceneGuidToAddressMapProvider.GetGuidFromAddress(TestUtils.Addressable1SceneAddress));
            Assert.Throws<AddressablesSupportDisabledException>(() => AddressableSceneGuidToAddressMapProvider.GetGuidFromAddress(TestUtils.Addressable2SceneAddress));
            Assert.Throws<AddressablesSupportDisabledException>(() => AddressableSceneGuidToAddressMapProvider.GetGuidFromAddress(TestUtils.NonExistingAddress));
            Assert.Throws<AddressablesSupportDisabledException>(() => AddressableSceneGuidToAddressMapProvider.GetGuidFromAddress(TestUtils.AddressableDuplicateAddressASceneAddress));
            Assert.Throws<AddressablesSupportDisabledException>(() => AddressableSceneGuidToAddressMapProvider.GetGuidFromAddress(TestUtils.AddressableDuplicateAddressBSceneAddress));
        }

        [Test]
        public void TryGetGuidFromAddress_IsAlwaysNegative_WithoutAddressableSupport()
        {
            TestUtils.IgnoreIfAddressablesSupportIsEnabled();

            void Test(string address, bool expectedSuccess, string expectedGuid)
            {
                var success = AddressableSceneGuidToAddressMapProvider.TryGetGuidFromAddress(address, out var guid);

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
