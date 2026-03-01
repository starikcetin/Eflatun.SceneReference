using System.Collections;
using Eflatun.SceneReference.Tests.Runtime.Subjects;
using Eflatun.SceneReference.Tests.Runtime.Utils;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Eflatun.SceneReference.Tests.Runtime
{
    /// <remarks>
    /// This class only tests serializations, since deserializations are already tested by <see cref="SceneReferenceCustomSerializationInCodeCreationTests"/>.
    /// </remarks>
    public class SceneReferenceCustomSerializationUnitySerializedTests
    {
        [UnitySetUp]
        public IEnumerator Setup() => TestSubjectContainer.CacheIfNotAlready();

        [Test]
        public void SerializesViaNewtonsoftJson_EnabledScene()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.EnabledSceneGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(TestSubjectContainer.EnabledScene.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesToBinary_EnabledScene()
        {
            var binaryBase64 = TestUtils.SerializeToBinaryBase64(TestSubjectContainer.EnabledScene.Field);
            Assert.AreEqual(TestUtils.EnabledSceneBinaryBase64, binaryBase64);
        }

        [Test]
        public void SerializesToXml_EnabledScene()
        {
            var xmlRaw = TestUtils.SerializeToXml(TestSubjectContainer.EnabledScene.Field);
            Assert.AreEqual(TestUtils.EnabledSceneXmlRaw, xmlRaw);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_DisabledScene()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.DisabledSceneGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(TestSubjectContainer.DisabledScene.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesToBinary_DisabledScene()
        {
            var binaryBase64 = TestUtils.SerializeToBinaryBase64(TestSubjectContainer.DisabledScene.Field);
            Assert.AreEqual(TestUtils.DisabledSceneBinaryBase64, binaryBase64);
        }

        [Test]
        public void SerializesToXml_DisabledScene()
        {
            var xmlRaw = TestUtils.SerializeToXml(TestSubjectContainer.DisabledScene.Field);
            Assert.AreEqual(TestUtils.DisabledSceneXmlRaw, xmlRaw);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_NotInBuildScene()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.NotInBuildSceneGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(TestSubjectContainer.NotInBuildScene.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesToBinary_NotInBuildScene()
        {
            var binaryBase64 = TestUtils.SerializeToBinaryBase64(TestSubjectContainer.NotInBuildScene.Field);
            Assert.AreEqual(TestUtils.NotInBuildSceneBinaryBase64, binaryBase64);
        }

        [Test]
        public void SerializesToXml_NotInBuildScene()
        {
            var xmlRaw = TestUtils.SerializeToXml(TestSubjectContainer.NotInBuildScene.Field);
            Assert.AreEqual(TestUtils.NotInBuildSceneXmlRaw, xmlRaw);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_Empty()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.AllZeroGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(TestSubjectContainer.Empty.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesToBinary_Empty()
        {
            var binaryBase64 = TestUtils.SerializeToBinaryBase64(TestSubjectContainer.Empty.Field);
            Assert.AreEqual(TestUtils.EmptyReferenceBinaryBase64, binaryBase64);
        }

        [Test]
        public void SerializesToXml_Empty()
        {
            var xmlRaw = TestUtils.SerializeToXml(TestSubjectContainer.Empty.Field);
            Assert.AreEqual(TestUtils.EmptyReferenceXmlRaw, xmlRaw);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_DeletedScene()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.DeletedSceneGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(TestSubjectContainer.DeletedScene.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesToBinary_DeletedScene()
        {
            var binaryBase64 = TestUtils.SerializeToBinaryBase64(TestSubjectContainer.DeletedScene.Field);
            Assert.AreEqual(TestUtils.DeletedSceneBinaryBase64, binaryBase64);
        }

        [Test]
        public void SerializesToXml_DeletedScene()
        {
            var xmlRaw = TestUtils.SerializeToXml(TestSubjectContainer.DeletedScene.Field);
            Assert.AreEqual(TestUtils.DeletedSceneXmlRaw, xmlRaw);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_NotExisting()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.NotExistingGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(TestSubjectContainer.NotExisting.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesToBinary_NotExisting()
        {
            var binaryBase64 = TestUtils.SerializeToBinaryBase64(TestSubjectContainer.NotExisting.Field);
            Assert.AreEqual(TestUtils.NotExistingBinaryBase64, binaryBase64);
        }

        [Test]
        public void SerializesToXml_NotExisting()
        {
            var xmlRaw = TestUtils.SerializeToXml(TestSubjectContainer.NotExisting.Field);
            Assert.AreEqual(TestUtils.NotExistingXmlRaw, xmlRaw);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_NotSceneAsset()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.NotSceneAssetGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(TestSubjectContainer.NotSceneAsset.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesToBinary_NotSceneAsset()
        {
            var binaryBase64 = TestUtils.SerializeToBinaryBase64(TestSubjectContainer.NotSceneAsset.Field);
            Assert.AreEqual(TestUtils.NotSceneAssetBinaryBase64, binaryBase64);
        }

        [Test]
        public void SerializesToXml_NotSceneAsset()
        {
            var xmlRaw = TestUtils.SerializeToXml(TestSubjectContainer.NotSceneAsset.Field);
            Assert.AreEqual(TestUtils.NotSceneAssetXmlRaw, xmlRaw);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_Addressable1Scene()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.Addressable1SceneGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(TestSubjectContainer.Addressable1Scene.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesToBinary_Addressable1Scene()
        {
            var binaryBase64 = TestUtils.SerializeToBinaryBase64(TestSubjectContainer.Addressable1Scene.Field);
            Assert.AreEqual(TestUtils.Addressable1SceneBinaryBase64, binaryBase64);
        }

        [Test]
        public void SerializesToXml_Addressable1Scene()
        {
            var xmlRaw = TestUtils.SerializeToXml(TestSubjectContainer.Addressable1Scene.Field);
            Assert.AreEqual(TestUtils.Addressable1SceneXmlRaw, xmlRaw);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_Addressable2Scene()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.Addressable2SceneGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(TestSubjectContainer.Addressable2Scene.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesToBinary_Addressable2Scene()
        {
            var binaryBase64 = TestUtils.SerializeToBinaryBase64(TestSubjectContainer.Addressable2Scene.Field);
            Assert.AreEqual(TestUtils.Addressable2SceneBinaryBase64, binaryBase64);
        }

        [Test]
        public void SerializesToXml_Addressable2Scene()
        {
            var xmlRaw = TestUtils.SerializeToXml(TestSubjectContainer.Addressable2Scene.Field);
            Assert.AreEqual(TestUtils.Addressable2SceneXmlRaw, xmlRaw);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_AddressableDuplicateAddressAScene()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.AddressableDuplicateAddressASceneGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(TestSubjectContainer.AddressableDuplicateAddressAScene.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesToBinary_AddressableDuplicateAddressAScene()
        {
            var binaryBase64 = TestUtils.SerializeToBinaryBase64(TestSubjectContainer.AddressableDuplicateAddressAScene.Field);
            Assert.AreEqual(TestUtils.AddressableDuplicateAddressASceneBinaryBase64, binaryBase64);
        }

        [Test]
        public void SerializesToXml_AddressableDuplicateAddressAScene()
        {
            var xmlRaw = TestUtils.SerializeToXml(TestSubjectContainer.AddressableDuplicateAddressAScene.Field);
            Assert.AreEqual(TestUtils.AddressableDuplicateAddressASceneXmlRaw, xmlRaw);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_AddressableDuplicateAddressBScene()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.AddressableDuplicateAddressBSceneGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(TestSubjectContainer.AddressableDuplicateAddressBScene.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesToBinary_AddressableDuplicateAddressBScene()
        {
            var binaryBase64 = TestUtils.SerializeToBinaryBase64(TestSubjectContainer.AddressableDuplicateAddressBScene.Field);
            Assert.AreEqual(TestUtils.AddressableDuplicateAddressBSceneBinaryBase64, binaryBase64);
        }

        [Test]
        public void SerializesToXml_AddressableDuplicateAddressBScene()
        {
            var xmlRaw = TestUtils.SerializeToXml(TestSubjectContainer.AddressableDuplicateAddressBScene.Field);
            Assert.AreEqual(TestUtils.AddressableDuplicateAddressBSceneXmlRaw, xmlRaw);
        }
    }
}
