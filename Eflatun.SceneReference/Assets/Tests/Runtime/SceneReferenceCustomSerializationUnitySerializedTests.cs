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
        public void SerializesViaBinaryFormatter_EnabledScene()
        {
            var expected = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.EnabledSceneGuid);
            var base64 = TestUtils.SerializeToBase64ViaBinaryFormatter(TestSubjectContainer.EnabledScene.Field);
            Assert.AreEqual(expected, base64);
        }

        [Test]
        public void SerializesViaSystemXml_EnabledScene()
        {
            var expected = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.EnabledSceneGuid);
            var actual = TestUtils.SerializeViaSystemXml(TestSubjectContainer.EnabledScene.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_DisabledScene()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.DisabledSceneGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(TestSubjectContainer.DisabledScene.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesViaBinaryFormatter_DisabledScene()
        {
            var expected = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.DisabledSceneGuid);
            var base64 = TestUtils.SerializeToBase64ViaBinaryFormatter(TestSubjectContainer.DisabledScene.Field);
            Assert.AreEqual(expected, base64);
        }

        [Test]
        public void SerializesViaSystemXml_DisabledScene()
        {
            var expected = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.DisabledSceneGuid);
            var actual = TestUtils.SerializeViaSystemXml(TestSubjectContainer.DisabledScene.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_NotInBuildScene()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.NotInBuildSceneGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(TestSubjectContainer.NotInBuildScene.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesViaBinaryFormatter_NotInBuildScene()
        {
            var expected = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.NotInBuildSceneGuid);
            var base64 = TestUtils.SerializeToBase64ViaBinaryFormatter(TestSubjectContainer.NotInBuildScene.Field);
            Assert.AreEqual(expected, base64);
        }

        [Test]
        public void SerializesViaSystemXml_NotInBuildScene()
        {
            var expected = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.NotInBuildSceneGuid);
            var actual = TestUtils.SerializeViaSystemXml(TestSubjectContainer.NotInBuildScene.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_Empty()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.AllZeroGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(TestSubjectContainer.Empty.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesViaBinaryFormatter_Empty()
        {
            var expected = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.AllZeroGuid);
            var base64 = TestUtils.SerializeToBase64ViaBinaryFormatter(TestSubjectContainer.Empty.Field);
            Assert.AreEqual(expected, base64);
        }

        [Test]
        public void SerializesViaSystemXml_Empty()
        {
            var expected = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.AllZeroGuid);
            var actual = TestUtils.SerializeViaSystemXml(TestSubjectContainer.Empty.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_DeletedScene()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.DeletedSceneGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(TestSubjectContainer.DeletedScene.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesViaBinaryFormatter_DeletedScene()
        {
            var expected = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.DeletedSceneGuid);
            var base64 = TestUtils.SerializeToBase64ViaBinaryFormatter(TestSubjectContainer.DeletedScene.Field);
            Assert.AreEqual(expected, base64);
        }

        [Test]
        public void SerializesViaSystemXml_DeletedScene()
        {
            var expected = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.DeletedSceneGuid);
            var actual = TestUtils.SerializeViaSystemXml(TestSubjectContainer.DeletedScene.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_NotExisting()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.NotExistingGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(TestSubjectContainer.NotExisting.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesViaBinaryFormatter_NotExisting()
        {
            var expected = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.NotExistingGuid);
            var base64 = TestUtils.SerializeToBase64ViaBinaryFormatter(TestSubjectContainer.NotExisting.Field);
            Assert.AreEqual(expected, base64);
        }

        [Test]
        public void SerializesViaSystemXml_NotExisting()
        {
            var expected = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.NotExistingGuid);
            var actual = TestUtils.SerializeViaSystemXml(TestSubjectContainer.NotExisting.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_NotSceneAsset()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.NotSceneAssetGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(TestSubjectContainer.NotSceneAsset.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesViaBinaryFormatter_NotSceneAsset()
        {
            var expected = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.NotSceneAssetGuid);
            var base64 = TestUtils.SerializeToBase64ViaBinaryFormatter(TestSubjectContainer.NotSceneAsset.Field);
            Assert.AreEqual(expected, base64);
        }

        [Test]
        public void SerializesViaSystemXml_NotSceneAsset()
        {
            var expected = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.NotSceneAssetGuid);
            var actual = TestUtils.SerializeViaSystemXml(TestSubjectContainer.NotSceneAsset.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_Addressable1Scene()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.Addressable1SceneGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(TestSubjectContainer.Addressable1Scene.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesViaBinaryFormatter_Addressable1Scene()
        {
            var expected = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.Addressable1SceneGuid);
            var base64 = TestUtils.SerializeToBase64ViaBinaryFormatter(TestSubjectContainer.Addressable1Scene.Field);
            Assert.AreEqual(expected, base64);
        }

        [Test]
        public void SerializesViaSystemXml_Addressable1Scene()
        {
            var expected = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.Addressable1SceneGuid);
            var actual = TestUtils.SerializeViaSystemXml(TestSubjectContainer.Addressable1Scene.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_Addressable2Scene()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.Addressable2SceneGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(TestSubjectContainer.Addressable2Scene.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesViaBinaryFormatter_Addressable2Scene()
        {
            var expected = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.Addressable2SceneGuid);
            var base64 = TestUtils.SerializeToBase64ViaBinaryFormatter(TestSubjectContainer.Addressable2Scene.Field);
            Assert.AreEqual(expected, base64);
        }

        [Test]
        public void SerializesViaSystemXml_Addressable2Scene()
        {
            var expected = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.Addressable2SceneGuid);
            var actual = TestUtils.SerializeViaSystemXml(TestSubjectContainer.Addressable2Scene.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_AddressableDuplicateAddressAScene()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.AddressableDuplicateAddressASceneGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(TestSubjectContainer.AddressableDuplicateAddressAScene.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesViaBinaryFormatter_AddressableDuplicateAddressAScene()
        {
            var expected = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.AddressableDuplicateAddressASceneGuid);
            var base64 = TestUtils.SerializeToBase64ViaBinaryFormatter(TestSubjectContainer.AddressableDuplicateAddressAScene.Field);
            Assert.AreEqual(expected, base64);
        }

        [Test]
        public void SerializesViaSystemXml_AddressableDuplicateAddressAScene()
        {
            var expected = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.AddressableDuplicateAddressASceneGuid);
            var actual = TestUtils.SerializeViaSystemXml(TestSubjectContainer.AddressableDuplicateAddressAScene.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesViaNewtonsoftJson_AddressableDuplicateAddressBScene()
        {
            var expected = TestUtils.GetExpectedOutputOfNewtonsoftJson(TestUtils.AddressableDuplicateAddressBSceneGuid);
            var actual = TestUtils.SerializeViaNewtonsoftJson(TestSubjectContainer.AddressableDuplicateAddressBScene.Field);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializesViaBinaryFormatter_AddressableDuplicateAddressBScene()
        {
            var expected = TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(TestUtils.AddressableDuplicateAddressBSceneGuid);
            var base64 = TestUtils.SerializeToBase64ViaBinaryFormatter(TestSubjectContainer.AddressableDuplicateAddressBScene.Field);
            Assert.AreEqual(expected, base64);
        }

        [Test]
        public void SerializesViaSystemXml_AddressableDuplicateAddressBScene()
        {
            var expected = TestUtils.GetExpectedOutputOfSystemXml(TestUtils.AddressableDuplicateAddressBSceneGuid);
            var actual = TestUtils.SerializeViaSystemXml(TestSubjectContainer.AddressableDuplicateAddressBScene.Field);
            Assert.AreEqual(expected, actual);
        }
    }
}
