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
        public void SerializesToJson_EnabledScene()
        {
            var jsonRaw = TestUtils.SerializeToJson(new SceneReference(TestUtils.EnabledSceneGuid));
            Assert.AreEqual(TestUtils.EnabledSceneJsonRaw, jsonRaw);
        }

        [Test]
        public void DeserializesFromJson_EnabledScene()
        {
            var deserialized = TestUtils.DeserializeFromJson(TestUtils.EnabledSceneJsonRaw);
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
        public void SerializesToJson_DisabledScene()
        {
            var jsonRaw = TestUtils.SerializeToJson(new SceneReference(TestUtils.DisabledSceneGuid));
            Assert.AreEqual(TestUtils.DisabledSceneJsonRaw, jsonRaw);
        }

        [Test]
        public void DeserializesFromJson_DisabledScene()
        {
            var deserialized = TestUtils.DeserializeFromJson(TestUtils.DisabledSceneJsonRaw);
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
        public void SerializesToJson_NotInBuildScene()
        {
            var jsonRaw = TestUtils.SerializeToJson(new SceneReference(TestUtils.NotInBuildSceneGuid));
            Assert.AreEqual(TestUtils.NotInBuildSceneJsonRaw, jsonRaw);
        }

        [Test]
        public void DeserializesFromJson_NotInBuildScene()
        {
            var deserialized = TestUtils.DeserializeFromJson(TestUtils.NotInBuildSceneJsonRaw);
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
        public void SerializesToJson_EmptyReference()
        {
            var jsonRaw = TestUtils.SerializeToJson(new SceneReference());
            Assert.AreEqual(TestUtils.EmptyReferenceJsonRaw, jsonRaw);
        }

        [Test]
        public void DeserializesFromJson_EmptyReference()
        {
            var deserialized = TestUtils.DeserializeFromJson(TestUtils.EmptyReferenceJsonRaw);
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
        public void DeserializesFromJson_DeletedScene()
        {
            var deserialized = TestUtils.DeserializeFromJson(TestUtils.DeletedSceneJsonRaw);
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
        public void DeserializesFromJson_NotExisting()
        {
            var deserialized = TestUtils.DeserializeFromJson(TestUtils.NotExistingJsonRaw);
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
        public void DeserializesFromJson_NotSceneAsset()
        {
            var deserialized = TestUtils.DeserializeFromJson(TestUtils.NotSceneAssetJsonRaw);
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

    }
}
