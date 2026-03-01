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
        public void SerializesToJson_EnabledScene()
        {
            var jsonRaw = TestUtils.SerializeToJson(TestSubjectContainer.EnabledScene.Field);
            Assert.AreEqual(TestUtils.EnabledSceneJsonRaw, jsonRaw);
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
        public void SerializesToJson_DisabledScene()
        {
            var jsonRaw = TestUtils.SerializeToJson(TestSubjectContainer.DisabledScene.Field);
            Assert.AreEqual(TestUtils.DisabledSceneJsonRaw, jsonRaw);
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
        public void SerializesToJson_NotInBuildScene()
        {
            var jsonRaw = TestUtils.SerializeToJson(TestSubjectContainer.NotInBuildScene.Field);
            Assert.AreEqual(TestUtils.NotInBuildSceneJsonRaw, jsonRaw);
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
        public void SerializesToJson_Empty()
        {
            var jsonRaw = TestUtils.SerializeToJson(TestSubjectContainer.Empty.Field);
            Assert.AreEqual(TestUtils.EmptyReferenceJsonRaw, jsonRaw);
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
        public void SerializesToJson_DeletedScene()
        {
            var jsonRaw = TestUtils.SerializeToJson(TestSubjectContainer.DeletedScene.Field);
            Assert.AreEqual(TestUtils.DeletedSceneJsonRaw, jsonRaw);
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
        public void SerializesToJson_NotExisting()
        {
            var jsonRaw = TestUtils.SerializeToJson(TestSubjectContainer.NotExisting.Field);
            Assert.AreEqual(TestUtils.NotExistingJsonRaw, jsonRaw);
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
        public void SerializesToJson_NotSceneAsset()
        {
            var jsonRaw = TestUtils.SerializeToJson(TestSubjectContainer.NotSceneAsset.Field);
            Assert.AreEqual(TestUtils.NotSceneAssetJsonRaw, jsonRaw);
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
        public void SerializesToJson_Addressable1Scene()
        {
            var jsonRaw = TestUtils.SerializeToJson(TestSubjectContainer.Addressable1Scene.Field);
            Assert.AreEqual(TestUtils.Addressable1SceneJsonRaw, jsonRaw);
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
        public void SerializesToJson_Addressable2Scene()
        {
            var jsonRaw = TestUtils.SerializeToJson(TestSubjectContainer.Addressable2Scene.Field);
            Assert.AreEqual(TestUtils.Addressable2SceneJsonRaw, jsonRaw);
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
        public void SerializesToJson_AddressableDuplicateAddressAScene()
        {
            var jsonRaw = TestUtils.SerializeToJson(TestSubjectContainer.AddressableDuplicateAddressAScene.Field);
            Assert.AreEqual(TestUtils.AddressableDuplicateAddressASceneJsonRaw, jsonRaw);
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
        public void SerializesToJson_AddressableDuplicateAddressBScene()
        {
            var jsonRaw = TestUtils.SerializeToJson(TestSubjectContainer.AddressableDuplicateAddressBScene.Field);
            Assert.AreEqual(TestUtils.AddressableDuplicateAddressBSceneJsonRaw, jsonRaw);
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
