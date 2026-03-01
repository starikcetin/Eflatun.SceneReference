using Eflatun.SceneReference.Tests.Runtime.Utils;
using NUnit.Framework;

namespace Eflatun.SceneReference.Tests.Runtime
{
    /// <remarks>
    /// Since it is impossible to create invalid scene references in-code, they are only tested for deserializations.
    /// </remarks>
    public class SceneReferenceCustomSerializationInCodeCreationTests
    {
        [Test]
        public void SerializesViaNewtonsoftJson_EnabledScene()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.EnabledSceneGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(new SceneReference(TestUtils.EnabledSceneGuid));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializesViaNewtonsoftJson_EnabledScene()
        {
            var json = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.EnabledSceneGuid);
            var deserialized = TestUtils.DeserializeViaNewtonsoftJson(json);
            TestUtils.AssertEnabledSceneState(deserialized);
        }

        [Test]
        public void SerializesToBinary_EnabledScene()
        {
            var binaryBase64 = TestUtils.SerializeToBinaryBase64(new SceneReference(TestUtils.EnabledSceneGuid));
            Assert.AreEqual(TestUtils.EnabledSceneBinaryBase64, binaryBase64);
        }

        [Test]
        public void DeserializesFromBinary_EnabledScene()
        {
            var deserialized = TestUtils.DeserializeFromBinaryBase64(TestUtils.EnabledSceneBinaryBase64);
            TestUtils.AssertEnabledSceneState(deserialized);
        }

        [Test]
        public void SerializesToXml_EnabledScene()
        {
            var xmlRaw = TestUtils.SerializeToXml(new SceneReference(TestUtils.EnabledSceneGuid));
            Assert.AreEqual(TestUtils.EnabledSceneXmlRaw, xmlRaw);
        }

        [Test]
        public void DeserializesFromXml_EnabledScene()
        {
            var deserialized = TestUtils.DeserializeFromXml(TestUtils.EnabledSceneXmlRaw);
            TestUtils.AssertEnabledSceneState(deserialized);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_DisabledScene()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.DisabledSceneGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(new SceneReference(TestUtils.DisabledSceneGuid));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializesViaNewtonsoftJson_DisabledScene()
        {
            var json = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.DisabledSceneGuid);
            var deserialized = TestUtils.DeserializeViaNewtonsoftJson(json);
            TestUtils.AssertDisabledSceneState(deserialized);
        }

        [Test]
        public void SerializesToBinary_DisabledScene()
        {
            var binaryBase64 = TestUtils.SerializeToBinaryBase64(new SceneReference(TestUtils.DisabledSceneGuid));
            Assert.AreEqual(TestUtils.DisabledSceneBinaryBase64, binaryBase64);
        }

        [Test]
        public void DeserializesFromBinary_DisabledScene()
        {
            var deserialized = TestUtils.DeserializeFromBinaryBase64(TestUtils.DisabledSceneBinaryBase64);
            TestUtils.AssertDisabledSceneState(deserialized);
        }

        [Test]
        public void SerializesToXml_DisabledScene()
        {
            var xmlRaw = TestUtils.SerializeToXml(new SceneReference(TestUtils.DisabledSceneGuid));
            Assert.AreEqual(TestUtils.DisabledSceneXmlRaw, xmlRaw);
        }

        [Test]
        public void DeserializesFromXml_DisabledScene()
        {
            var deserialized = TestUtils.DeserializeFromXml(TestUtils.DisabledSceneXmlRaw);
            TestUtils.AssertDisabledSceneState(deserialized);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_NotInBuildScene()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.NotInBuildSceneGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(new SceneReference(TestUtils.NotInBuildSceneGuid));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializesViaNewtonsoftJson_NotInBuildScene()
        {
            var json = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.NotInBuildSceneGuid);
            var deserialized = TestUtils.DeserializeViaNewtonsoftJson(json);
            TestUtils.AssertNotInBuildSceneState(deserialized);
        }

        [Test]
        public void SerializesToBinary_NotInBuildScene()
        {
            var binaryBase64 = TestUtils.SerializeToBinaryBase64(new SceneReference(TestUtils.NotInBuildSceneGuid));
            Assert.AreEqual(TestUtils.NotInBuildSceneBinaryBase64, binaryBase64);
        }

        [Test]
        public void DeserializesFromBinary_NotInBuildScene()
        {
            var deserialized = TestUtils.DeserializeFromBinaryBase64(TestUtils.NotInBuildSceneBinaryBase64);
            TestUtils.AssertNotInBuildSceneState(deserialized);
        }

        [Test]
        public void SerializesToXml_NotInBuildScene()
        {
            var xmlRaw = TestUtils.SerializeToXml(new SceneReference(TestUtils.NotInBuildSceneGuid));
            Assert.AreEqual(TestUtils.NotInBuildSceneXmlRaw, xmlRaw);
        }

        [Test]
        public void DeserializesFromXml_NotInBuildScene()
        {
            var deserialized = TestUtils.DeserializeFromXml(TestUtils.NotInBuildSceneXmlRaw);
            TestUtils.AssertNotInBuildSceneState(deserialized);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_EmptyReference()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.AllZeroGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(new SceneReference());
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializesViaNewtonsoftJson_EmptyReference()
        {
            var json = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.AllZeroGuid);
            var deserialized = TestUtils.DeserializeViaNewtonsoftJson(json);
            TestUtils.AssertEmptyState(deserialized);
        }

        [Test]
        public void SerializesToBinary_EmptyReference()
        {
            var binaryBase64 = TestUtils.SerializeToBinaryBase64(new SceneReference());
            Assert.AreEqual(TestUtils.EmptyReferenceBinaryBase64, binaryBase64);
        }

        [Test]
        public void DeserializesFromBinary_EmptyReference()
        {
            var deserialized = TestUtils.DeserializeFromBinaryBase64(TestUtils.EmptyReferenceBinaryBase64);
            TestUtils.AssertEmptyState(deserialized);
        }

        [Test]
        public void SerializesToXml_EmptyReference()
        {
            var xmlRaw = TestUtils.SerializeToXml(new SceneReference());
            Assert.AreEqual(TestUtils.EmptyReferenceXmlRaw, xmlRaw);
        }

        [Test]
        public void DeserializesFromXml_EmptyReference()
        {
            var deserialized = TestUtils.DeserializeFromXml(TestUtils.EmptyReferenceXmlRaw);
            TestUtils.AssertEmptyState(deserialized);
        }

        [Test]
        public void DeserializesViaNewtonsoftJson_DeletedScene()
        {
            var json = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.DeletedSceneGuid);
            var deserialized = TestUtils.DeserializeViaNewtonsoftJson(json);
            TestUtils.AssertDeletedSceneState(deserialized);
        }

        [Test]
        public void DeserializesFromBinary_DeletedScene()
        {
            var deserialized = TestUtils.DeserializeFromBinaryBase64(TestUtils.DeletedSceneBinaryBase64);
            TestUtils.AssertDeletedSceneState(deserialized);
        }

        [Test]
        public void DeserializesFromXml_DeletedScene()
        {
            var deserialized = TestUtils.DeserializeFromXml(TestUtils.DeletedSceneXmlRaw);
            TestUtils.AssertDeletedSceneState(deserialized);
        }

        [Test]
        public void DeserializesViaNewtonsoftJson_NotExisting()
        {
            var json = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.NotExistingGuid);
            var deserialized = TestUtils.DeserializeViaNewtonsoftJson(json);
            TestUtils.AssertNotExistingState(deserialized);
        }

        [Test]
        public void DeserializesFromBinary_NotExisting()
        {
            var deserialized = TestUtils.DeserializeFromBinaryBase64(TestUtils.NotExistingBinaryBase64);
            TestUtils.AssertNotExistingState(deserialized);
        }

        [Test]
        public void DeserializesFromXml_NotExisting()
        {
            var deserialized = TestUtils.DeserializeFromXml(TestUtils.NotExistingXmlRaw);
            TestUtils.AssertNotExistingState(deserialized);
        }

        [Test]
        public void DeserializesViaNewtonsoftJson_NotSceneAsset()
        {
            var json = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.NotSceneAssetGuid);
            var deserialized = TestUtils.DeserializeViaNewtonsoftJson(json);
            TestUtils.AssertNotSceneAssetState(deserialized);
        }

        [Test]
        public void DeserializesFromBinary_NotSceneAsset()
        {
            var deserialized = TestUtils.DeserializeFromBinaryBase64(TestUtils.NotSceneAssetBinaryBase64);
            TestUtils.AssertNotSceneAssetState(deserialized);
        }

        [Test]
        public void DeserializesFromXml_NotSceneAsset()
        {
            var deserialized = TestUtils.DeserializeFromXml(TestUtils.NotSceneAssetXmlRaw);
            TestUtils.AssertNotSceneAssetState(deserialized);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_Addressable1Scene()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.Addressable1SceneGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(new SceneReference(TestUtils.Addressable1SceneGuid));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializesViaNewtonsoftJson_Addressable1Scene()
        {
            var json = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.Addressable1SceneGuid);
            var deserialized = TestUtils.DeserializeViaNewtonsoftJson(json);
            TestUtils.AssertAddressable1SceneState(deserialized);
        }

        [Test]
        public void SerializesToBinary_Addressable1Scene()
        {
            var binaryBase64 = TestUtils.SerializeToBinaryBase64(new SceneReference(TestUtils.Addressable1SceneGuid));
            Assert.AreEqual(TestUtils.Addressable1SceneBinaryBase64, binaryBase64);
        }

        [Test]
        public void DeserializesFromBinary_Addressable1Scene()
        {
            var deserialized = TestUtils.DeserializeFromBinaryBase64(TestUtils.Addressable1SceneBinaryBase64);
            TestUtils.AssertAddressable1SceneState(deserialized);
        }

        [Test]
        public void SerializesToXml_Addressable1Scene()
        {
            var xmlRaw = TestUtils.SerializeToXml(new SceneReference(TestUtils.Addressable1SceneGuid));
            Assert.AreEqual(TestUtils.Addressable1SceneXmlRaw, xmlRaw);
        }

        [Test]
        public void DeserializesFromXml_Addressable1Scene()
        {
            var deserialized = TestUtils.DeserializeFromXml(TestUtils.Addressable1SceneXmlRaw);
            TestUtils.AssertAddressable1SceneState(deserialized);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_Addressable2Scene()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.Addressable2SceneGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(new SceneReference(TestUtils.Addressable2SceneGuid));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializesViaNewtonsoftJson_Addressable2Scene()
        {
            var json = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.Addressable2SceneGuid);
            var deserialized = TestUtils.DeserializeViaNewtonsoftJson(json);
            TestUtils.AssertAddressable2SceneState(deserialized);
        }

        [Test]
        public void SerializesToBinary_Addressable2Scene()
        {
            var binaryBase64 = TestUtils.SerializeToBinaryBase64(new SceneReference(TestUtils.Addressable2SceneGuid));
            Assert.AreEqual(TestUtils.Addressable2SceneBinaryBase64, binaryBase64);
        }

        [Test]
        public void DeserializesFromBinary_Addressable2Scene()
        {
            var deserialized = TestUtils.DeserializeFromBinaryBase64(TestUtils.Addressable2SceneBinaryBase64);
            TestUtils.AssertAddressable2SceneState(deserialized);
        }

        [Test]
        public void SerializesToXml_Addressable2Scene()
        {
            var xmlRaw = TestUtils.SerializeToXml(new SceneReference(TestUtils.Addressable2SceneGuid));
            Assert.AreEqual(TestUtils.Addressable2SceneXmlRaw, xmlRaw);
        }

        [Test]
        public void DeserializesFromXml_Addressable2Scene()
        {
            var deserialized = TestUtils.DeserializeFromXml(TestUtils.Addressable2SceneXmlRaw);
            TestUtils.AssertAddressable2SceneState(deserialized);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_AddressableDuplicateAddressAScene()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.AddressableDuplicateAddressASceneGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(new SceneReference(TestUtils.AddressableDuplicateAddressASceneGuid));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializesViaNewtonsoftJson_AddressableDuplicateAddressAScene()
        {
            var json = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.AddressableDuplicateAddressASceneGuid);
            var deserialized = TestUtils.DeserializeViaNewtonsoftJson(json);
            TestUtils.AssertAddressableDuplicateAddressASceneState(deserialized);
        }

        [Test]
        public void SerializesToBinary_AddressableDuplicateAddressAScene()
        {
            var binaryBase64 = TestUtils.SerializeToBinaryBase64(new SceneReference(TestUtils.AddressableDuplicateAddressASceneGuid));
            Assert.AreEqual(TestUtils.AddressableDuplicateAddressASceneBinaryBase64, binaryBase64);
        }

        [Test]
        public void DeserializesFromBinary_AddressableDuplicateAddressAScene()
        {
            var deserialized = TestUtils.DeserializeFromBinaryBase64(TestUtils.AddressableDuplicateAddressASceneBinaryBase64);
            TestUtils.AssertAddressableDuplicateAddressASceneState(deserialized);
        }

        [Test]
        public void SerializesToXml_AddressableDuplicateAddressAScene()
        {
            var xmlRaw = TestUtils.SerializeToXml(new SceneReference(TestUtils.AddressableDuplicateAddressASceneGuid));
            Assert.AreEqual(TestUtils.AddressableDuplicateAddressASceneXmlRaw, xmlRaw);
        }

        [Test]
        public void DeserializesFromXml_AddressableDuplicateAddressAScene()
        {
            var deserialized = TestUtils.DeserializeFromXml(TestUtils.AddressableDuplicateAddressASceneXmlRaw);
            TestUtils.AssertAddressableDuplicateAddressASceneState(deserialized);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_AddressableDuplicateAddressBScene()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.AddressableDuplicateAddressBSceneGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(new SceneReference(TestUtils.AddressableDuplicateAddressBSceneGuid));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializesViaNewtonsoftJson_AddressableDuplicateAddressBScene()
        {
            var json = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.AddressableDuplicateAddressBSceneGuid);
            var deserialized = TestUtils.DeserializeViaNewtonsoftJson(json);
            TestUtils.AssertAddressableDuplicateAddressBSceneState(deserialized);
        }

        [Test]
        public void SerializesToBinary_AddressableDuplicateAddressBScene()
        {
            var binaryBase64 = TestUtils.SerializeToBinaryBase64(new SceneReference(TestUtils.AddressableDuplicateAddressBSceneGuid));
            Assert.AreEqual(TestUtils.AddressableDuplicateAddressBSceneBinaryBase64, binaryBase64);
        }

        [Test]
        public void DeserializesFromBinary_AddressableDuplicateAddressBScene()
        {
            var deserialized = TestUtils.DeserializeFromBinaryBase64(TestUtils.AddressableDuplicateAddressBSceneBinaryBase64);
            TestUtils.AssertAddressableDuplicateAddressBSceneState(deserialized);
        }

        [Test]
        public void SerializesToXml_AddressableDuplicateAddressBScene()
        {
            var xmlRaw = TestUtils.SerializeToXml(new SceneReference(TestUtils.AddressableDuplicateAddressBSceneGuid));
            Assert.AreEqual(TestUtils.AddressableDuplicateAddressBSceneXmlRaw, xmlRaw);
        }

        [Test]
        public void DeserializesFromXml_AddressableDuplicateAddressBScene()
        {
            var deserialized = TestUtils.DeserializeFromXml(TestUtils.AddressableDuplicateAddressBSceneXmlRaw);
            TestUtils.AssertAddressableDuplicateAddressBSceneState(deserialized);
        }
    }
}
