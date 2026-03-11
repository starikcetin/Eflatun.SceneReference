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
        public void SerializesViaBinaryFormatter_EnabledScene()
        {
            var expected = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.EnabledSceneGuid);
            var actual = TestUtils.SerializeToBase64ViaBinaryFormatter(new SceneReference(TestUtils.EnabledSceneGuid));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializesViaBinaryFormatter_EnabledScene()
        {
            var base64 = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.EnabledSceneGuid);
            var deserialized = TestUtils.DeserializeFromBase64ViaBinaryFormatter(base64);
            TestUtils.AssertEnabledSceneState(deserialized);
        }

        [Test]
        public void SerializesViaSystemXml_EnabledScene()
        {
            var expected = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.EnabledSceneGuid);
            var actual = TestUtils.SerializeViaSystemXml(new SceneReference(TestUtils.EnabledSceneGuid));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializesViaSystemXml_EnabledScene()
        {
            var xml = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.EnabledSceneGuid);
            var deserialized = TestUtils.DeserializeViaSystemXml(xml);
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
        public void SerializesViaBinaryFormatter_DisabledScene()
        {
            var expected = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.DisabledSceneGuid);
            var actual = TestUtils.SerializeToBase64ViaBinaryFormatter(new SceneReference(TestUtils.DisabledSceneGuid));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializesViaBinaryFormatter_DisabledScene()
        {
            var base64 = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.DisabledSceneGuid);
            var deserialized = TestUtils.DeserializeFromBase64ViaBinaryFormatter(base64);
            TestUtils.AssertDisabledSceneState(deserialized);
        }

        [Test]
        public void SerializesViaSystemXml_DisabledScene()
        {
            var expected = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.DisabledSceneGuid);
            var actual = TestUtils.SerializeViaSystemXml(new SceneReference(TestUtils.DisabledSceneGuid));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializesViaSystemXml_DisabledScene()
        {
            var xml = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.DisabledSceneGuid);
            var deserialized = TestUtils.DeserializeViaSystemXml(xml);
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
        public void SerializesViaBinaryFormatter_NotInBuildScene()
        {
            var expected = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.NotInBuildSceneGuid);
            var actual = TestUtils.SerializeToBase64ViaBinaryFormatter(new SceneReference(TestUtils.NotInBuildSceneGuid));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializesViaBinaryFormatter_NotInBuildScene()
        {
            var base64 = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.NotInBuildSceneGuid);
            var deserialized = TestUtils.DeserializeFromBase64ViaBinaryFormatter(base64);
            TestUtils.AssertNotInBuildSceneState(deserialized);
        }

        [Test]
        public void SerializesViaSystemXml_NotInBuildScene()
        {
            var expected = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.NotInBuildSceneGuid);
            var actual = TestUtils.SerializeViaSystemXml(new SceneReference(TestUtils.NotInBuildSceneGuid));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializesViaSystemXml_NotInBuildScene()
        {
            var xml = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.NotInBuildSceneGuid);
            var deserialized = TestUtils.DeserializeViaSystemXml(xml);
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
        public void SerializesViaBinaryFormatter_EmptyReference()
        {
            var expected = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.AllZeroGuid);
            var actual = TestUtils.SerializeToBase64ViaBinaryFormatter(new SceneReference());
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializesViaBinaryFormatter_EmptyReference()
        {
            var base64 = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.AllZeroGuid);
            var deserialized = TestUtils.DeserializeFromBase64ViaBinaryFormatter(base64);
            TestUtils.AssertEmptyState(deserialized);
        }

        [Test]
        public void SerializesViaSystemXml_EmptyReference()
        {
            var expected = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.AllZeroGuid);
            var actual = TestUtils.SerializeViaSystemXml(new SceneReference());
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializesViaSystemXml_EmptyReference()
        {
            var xml = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.AllZeroGuid);
            var deserialized = TestUtils.DeserializeViaSystemXml(xml);
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
        public void DeserializesViaBinaryFormatter_DeletedScene()
        {
            var base64 = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.DeletedSceneGuid);
            var deserialized = TestUtils.DeserializeFromBase64ViaBinaryFormatter(base64);
            TestUtils.AssertDeletedSceneState(deserialized);
        }

        [Test]
        public void DeserializesViaSystemXml_DeletedScene()
        {
            var xml = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.DeletedSceneGuid);
            var deserialized = TestUtils.DeserializeViaSystemXml(xml);
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
        public void DeserializesViaBinaryFormatter_NotExisting()
        {
            var base64 = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.NotExistingGuid);
            var deserialized = TestUtils.DeserializeFromBase64ViaBinaryFormatter(base64);
            TestUtils.AssertNotExistingState(deserialized);
        }

        [Test]
        public void DeserializesViaSystemXml_NotExisting()
        {
            var xml = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.NotExistingGuid);
            var deserialized = TestUtils.DeserializeViaSystemXml(xml);
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
        public void DeserializesViaBinaryFormatter_NotSceneAsset()
        {
            var base64 = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.NotSceneAssetGuid);
            var deserialized = TestUtils.DeserializeFromBase64ViaBinaryFormatter(base64);
            TestUtils.AssertNotSceneAssetState(deserialized);
        }

        [Test]
        public void DeserializesViaSystemXml_NotSceneAsset()
        {
            var xml = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.NotSceneAssetGuid);
            var deserialized = TestUtils.DeserializeViaSystemXml(xml);
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
        public void SerializesViaBinaryFormatter_Addressable1Scene()
        {
            var expected = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.Addressable1SceneGuid);
            var actual = TestUtils.SerializeToBase64ViaBinaryFormatter(new SceneReference(TestUtils.Addressable1SceneGuid));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializesViaBinaryFormatter_Addressable1Scene()
        {
            var base64 = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.Addressable1SceneGuid);
            var deserialized = TestUtils.DeserializeFromBase64ViaBinaryFormatter(base64);
            TestUtils.AssertAddressable1SceneState(deserialized);
        }

        [Test]
        public void SerializesViaSystemXml_Addressable1Scene()
        {
            var expected = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.Addressable1SceneGuid);
            var actual = TestUtils.SerializeViaSystemXml(new SceneReference(TestUtils.Addressable1SceneGuid));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializesViaSystemXml_Addressable1Scene()
        {
            var xml = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.Addressable1SceneGuid);
            var deserialized = TestUtils.DeserializeViaSystemXml(xml);
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
        public void SerializesViaBinaryFormatter_Addressable2Scene()
        {
            var expected = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.Addressable2SceneGuid);
            var actual = TestUtils.SerializeToBase64ViaBinaryFormatter(new SceneReference(TestUtils.Addressable2SceneGuid));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializesViaBinaryFormatter_Addressable2Scene()
        {
            var base64 = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.Addressable2SceneGuid);
            var deserialized = TestUtils.DeserializeFromBase64ViaBinaryFormatter(base64);
            TestUtils.AssertAddressable2SceneState(deserialized);
        }

        [Test]
        public void SerializesViaSystemXml_Addressable2Scene()
        {
            var expected = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.Addressable2SceneGuid);
            var actual = TestUtils.SerializeViaSystemXml(new SceneReference(TestUtils.Addressable2SceneGuid));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializesViaSystemXml_Addressable2Scene()
        {
            var xml = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.Addressable2SceneGuid);
            var deserialized = TestUtils.DeserializeViaSystemXml(xml);
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
        public void SerializesViaBinaryFormatter_AddressableDuplicateAddressAScene()
        {
            var expected = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.AddressableDuplicateAddressASceneGuid);
            var actual = TestUtils.SerializeToBase64ViaBinaryFormatter(new SceneReference(TestUtils.AddressableDuplicateAddressASceneGuid));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializesViaBinaryFormatter_AddressableDuplicateAddressAScene()
        {
            var base64 = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.AddressableDuplicateAddressASceneGuid);
            var deserialized = TestUtils.DeserializeFromBase64ViaBinaryFormatter(base64);
            TestUtils.AssertAddressableDuplicateAddressASceneState(deserialized);
        }

        [Test]
        public void SerializesViaSystemXml_AddressableDuplicateAddressAScene()
        {
            var expected = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.AddressableDuplicateAddressASceneGuid);
            var actual = TestUtils.SerializeViaSystemXml(new SceneReference(TestUtils.AddressableDuplicateAddressASceneGuid));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializesViaSystemXml_AddressableDuplicateAddressAScene()
        {
            var xml = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.AddressableDuplicateAddressASceneGuid);
            var deserialized = TestUtils.DeserializeViaSystemXml(xml);
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
        public void SerializesViaBinaryFormatter_AddressableDuplicateAddressBScene()
        {
            var expected = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.AddressableDuplicateAddressBSceneGuid);
            var actual = TestUtils.SerializeToBase64ViaBinaryFormatter(new SceneReference(TestUtils.AddressableDuplicateAddressBSceneGuid));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializesViaBinaryFormatter_AddressableDuplicateAddressBScene()
        {
            var base64 = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.AddressableDuplicateAddressBSceneGuid);
            var deserialized = TestUtils.DeserializeFromBase64ViaBinaryFormatter(base64);
            TestUtils.AssertAddressableDuplicateAddressBSceneState(deserialized);
        }

        [Test]
        public void SerializesViaSystemXml_AddressableDuplicateAddressBScene()
        {
            var expected = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.AddressableDuplicateAddressBSceneGuid);
            var actual = TestUtils.SerializeViaSystemXml(new SceneReference(TestUtils.AddressableDuplicateAddressBSceneGuid));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializesViaSystemXml_AddressableDuplicateAddressBScene()
        {
            var xml = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.AddressableDuplicateAddressBSceneGuid);
            var deserialized = TestUtils.DeserializeViaSystemXml(xml);
            TestUtils.AssertAddressableDuplicateAddressBSceneState(deserialized);
        }

        [Test]
        public void DeserializesViaNewtonsoftJson_UppercaseGuid_EnabledScene()
        {
            var json = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.EnabledSceneGuid.ToUpperInvariant());
            var deserialized = TestUtils.DeserializeViaNewtonsoftJson(json);
            TestUtils.AssertEnabledSceneState(deserialized);
        }

        [Test]
        public void DeserializesViaBinaryFormatter_UppercaseGuid_EnabledScene()
        {
            var base64 = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.EnabledSceneGuid.ToUpperInvariant());
            var deserialized = TestUtils.DeserializeFromBase64ViaBinaryFormatter(base64);
            TestUtils.AssertEnabledSceneState(deserialized);
        }

        [Test]
        public void DeserializesViaSystemXml_UppercaseGuid_EnabledScene()
        {
            var xml = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.EnabledSceneGuid.ToUpperInvariant());
            var deserialized = TestUtils.DeserializeViaSystemXml(xml);
            TestUtils.AssertEnabledSceneState(deserialized);
        }

        [Test]
        public void DeserializesViaNewtonsoftJson_UppercaseGuid_DisabledScene()
        {
            var json = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.DisabledSceneGuid.ToUpperInvariant());
            var deserialized = TestUtils.DeserializeViaNewtonsoftJson(json);
            TestUtils.AssertDisabledSceneState(deserialized);
        }

        [Test]
        public void DeserializesViaBinaryFormatter_UppercaseGuid_DisabledScene()
        {
            var base64 = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.DisabledSceneGuid.ToUpperInvariant());
            var deserialized = TestUtils.DeserializeFromBase64ViaBinaryFormatter(base64);
            TestUtils.AssertDisabledSceneState(deserialized);
        }

        [Test]
        public void DeserializesViaSystemXml_UppercaseGuid_DisabledScene()
        {
            var xml = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.DisabledSceneGuid.ToUpperInvariant());
            var deserialized = TestUtils.DeserializeViaSystemXml(xml);
            TestUtils.AssertDisabledSceneState(deserialized);
        }

        [Test]
        public void DeserializesViaNewtonsoftJson_UppercaseGuid_NotInBuildScene()
        {
            var json = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.NotInBuildSceneGuid.ToUpperInvariant());
            var deserialized = TestUtils.DeserializeViaNewtonsoftJson(json);
            TestUtils.AssertNotInBuildSceneState(deserialized);
        }

        [Test]
        public void DeserializesViaBinaryFormatter_UppercaseGuid_NotInBuildScene()
        {
            var base64 = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.NotInBuildSceneGuid.ToUpperInvariant());
            var deserialized = TestUtils.DeserializeFromBase64ViaBinaryFormatter(base64);
            TestUtils.AssertNotInBuildSceneState(deserialized);
        }

        [Test]
        public void DeserializesViaSystemXml_UppercaseGuid_NotInBuildScene()
        {
            var xml = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.NotInBuildSceneGuid.ToUpperInvariant());
            var deserialized = TestUtils.DeserializeViaSystemXml(xml);
            TestUtils.AssertNotInBuildSceneState(deserialized);
        }
    }
}
