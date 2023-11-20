using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Eflatun.SceneReference.DevelopmentUtils;
using Eflatun.SceneReference.Exceptions;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEngine.SceneManagement;

namespace Eflatun.SceneReference.Tests.Runtime.Utils
{
    public static class TestUtils
    {
        // NOTE: Do not hardcode the build indices, as they seem to change on test builds.
        // Possibly due to Unity inserting their test scene at the beginning of the list.

        public const string TestSubjectContainerScenePath = "Assets/Tests/Runtime/Subjects/TestSubjectContainer.unity";

        public const string EnabledSceneName = "TestSubject_Enabled";
        public const string EnabledScenePath = "Assets/Tests/Runtime/Subjects/TestSubject_Enabled.unity";
        public static int EnabledSceneBuildIndex => SceneUtility.GetBuildIndexByScenePath(EnabledScenePath);
        public const string EnabledSceneGuid = "e3f2c1473b766c34ba5b37779d71787e";
        public static string EnabledSceneJsonRaw => GetRawJson(EnabledSceneGuid);
        public static string EnabledSceneXmlRaw => GetRawXml(EnabledSceneGuid);
        public const string EnabledSceneBinaryBase64 = @"AAEAAAD/////AQAAAAAAAAAMAgAAAE1FZmxhdHVuLlNjZW5lUmVmZXJlbmNlLCBWZXJzaW9uPTAuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbAUBAAAAJUVmbGF0dW4uU2NlbmVSZWZlcmVuY2UuU2NlbmVSZWZlcmVuY2UBAAAAEXNjZW5lQXNzZXRHdWlkSGV4AQIAAAAGAwAAACBlM2YyYzE0NzNiNzY2YzM0YmE1YjM3Nzc5ZDcxNzg3ZQs=";

        public const string DisabledSceneName = "TestSubject_Disabled";
        public const string DisabledScenePath = "Assets/Tests/Runtime/Subjects/TestSubject_Disabled.unity";
        public static int DisabledSceneBuildIndex => SceneUtility.GetBuildIndexByScenePath(DisabledScenePath);
        public const string DisabledSceneGuid = "7e37b14fa3517514a91937cec5cad27a";
        public static string DisabledSceneJsonRaw => GetRawJson(DisabledSceneGuid);
        public static string DisabledSceneXmlRaw => GetRawXml(DisabledSceneGuid);
        public const string DisabledSceneBinaryBase64 = @"AAEAAAD/////AQAAAAAAAAAMAgAAAE1FZmxhdHVuLlNjZW5lUmVmZXJlbmNlLCBWZXJzaW9uPTAuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbAUBAAAAJUVmbGF0dW4uU2NlbmVSZWZlcmVuY2UuU2NlbmVSZWZlcmVuY2UBAAAAEXNjZW5lQXNzZXRHdWlkSGV4AQIAAAAGAwAAACA3ZTM3YjE0ZmEzNTE3NTE0YTkxOTM3Y2VjNWNhZDI3YQs=";

        public const string NotInBuildSceneName = "TestSubject_NotInBuild";
        public const string NotInBuildScenePath = "Assets/Tests/Runtime/Subjects/TestSubject_NotInBuild.unity";
        public static int NotInBuildSceneBuildIndex => SceneUtility.GetBuildIndexByScenePath(NotInBuildScenePath);
        public const string NotInBuildSceneGuid = "63c386231869c904c9b701dd79268476";
        public static string NotInBuildSceneJsonRaw => GetRawJson(NotInBuildSceneGuid);
        public static string NotInBuildSceneXmlRaw => GetRawXml(NotInBuildSceneGuid);
        public const string NotInBuildSceneBinaryBase64 = @"AAEAAAD/////AQAAAAAAAAAMAgAAAE1FZmxhdHVuLlNjZW5lUmVmZXJlbmNlLCBWZXJzaW9uPTAuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbAUBAAAAJUVmbGF0dW4uU2NlbmVSZWZlcmVuY2UuU2NlbmVSZWZlcmVuY2UBAAAAEXNjZW5lQXNzZXRHdWlkSGV4AQIAAAAGAwAAACA2M2MzODYyMzE4NjljOTA0YzliNzAxZGQ3OTI2ODQ3Ngs=";

        public const string AllZeroGuid = "00000000000000000000000000000000";
        public static string EmptyReferenceJsonRaw => GetRawJson(AllZeroGuid);
        public static string EmptyReferenceXmlRaw => GetRawXml(AllZeroGuid);
        public const string EmptyReferenceBinaryBase64 = @"AAEAAAD/////AQAAAAAAAAAMAgAAAE1FZmxhdHVuLlNjZW5lUmVmZXJlbmNlLCBWZXJzaW9uPTAuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbAUBAAAAJUVmbGF0dW4uU2NlbmVSZWZlcmVuY2UuU2NlbmVSZWZlcmVuY2UBAAAAEXNjZW5lQXNzZXRHdWlkSGV4AQIAAAAGAwAAACAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMAs=";

        public const string DeletedSceneGuid = "69c1683d94db0cc469d86e4e865f9f5d";
        public static string DeletedSceneJsonRaw => GetRawJson(DeletedSceneGuid);
        public static string DeletedSceneXmlRaw => GetRawXml(DeletedSceneGuid);
        public const string DeletedSceneBinaryBase64 = "AAEAAAD/////AQAAAAAAAAAMAgAAAE1FZmxhdHVuLlNjZW5lUmVmZXJlbmNlLCBWZXJzaW9uPTAuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbAUBAAAAJUVmbGF0dW4uU2NlbmVSZWZlcmVuY2UuU2NlbmVSZWZlcmVuY2UBAAAAEXNjZW5lQXNzZXRHdWlkSGV4AQIAAAAGAwAAACA2OWMxNjgzZDk0ZGIwY2M0NjlkODZlNGU4NjVmOWY1ZAs=";

        public const string NotExistingGuid = "2bc1683d94d80cc269d85e4e8a5fcf5d";
        public static string NotExistingJsonRaw => GetRawJson(NotExistingGuid);
        public static string NotExistingXmlRaw => GetRawXml(NotExistingGuid);
        public const string NotExistingBinaryBase64 = "AAEAAAD/////AQAAAAAAAAAMAgAAAE1FZmxhdHVuLlNjZW5lUmVmZXJlbmNlLCBWZXJzaW9uPTAuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbAUBAAAAJUVmbGF0dW4uU2NlbmVSZWZlcmVuY2UuU2NlbmVSZWZlcmVuY2UBAAAAEXNjZW5lQXNzZXRHdWlkSGV4AQIAAAAGAwAAACAyYmMxNjgzZDk0ZDgwY2MyNjlkODVlNGU4YTVmY2Y1ZAs=";

        public const string NotSceneAssetGuid = "99d2aa5f58f54c44fba8671b66be5259";
        public const string NotSceneAssetPath = "Assets/Tests/Runtime/Utils/TestSubject_Material.mat";
        public static string NotSceneAssetJsonRaw => GetRawJson(NotSceneAssetGuid);
        public static string NotSceneAssetXmlRaw => GetRawXml(NotSceneAssetGuid);
        public const string NotSceneAssetBinaryBase64 = "AAEAAAD/////AQAAAAAAAAAMAgAAAE1FZmxhdHVuLlNjZW5lUmVmZXJlbmNlLCBWZXJzaW9uPTAuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbAUBAAAAJUVmbGF0dW4uU2NlbmVSZWZlcmVuY2UuU2NlbmVSZWZlcmVuY2UBAAAAEXNjZW5lQXNzZXRHdWlkSGV4AQIAAAAGAwAAACA5OWQyYWE1ZjU4ZjU0YzQ0ZmJhODY3MWI2NmJlNTI1OQs=";

        public const string Addressable1SceneName = "TestSubject_Addressable1";
        public const string Addressable1ScenePath = "Assets/Tests/Runtime/Subjects/TestSubject_Addressable1.unity";
        public static int Addressable1SceneBuildIndex => SceneUtility.GetBuildIndexByScenePath(Addressable1ScenePath);
        public const string Addressable1SceneGuid = "8b3f523138018e04ebcb86e1230451b1";
        public static string Addressable1SceneJsonRaw => GetRawJson(Addressable1SceneGuid);
        public static string Addressable1SceneXmlRaw => GetRawXml(Addressable1SceneGuid);
        public const string Addressable1SceneBinaryBase64 = "AAEAAAD/////AQAAAAAAAAAMAgAAAE1FZmxhdHVuLlNjZW5lUmVmZXJlbmNlLCBWZXJzaW9uPTAuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbAUBAAAAJUVmbGF0dW4uU2NlbmVSZWZlcmVuY2UuU2NlbmVSZWZlcmVuY2UBAAAAEXNjZW5lQXNzZXRHdWlkSGV4AQIAAAAGAwAAACA4YjNmNTIzMTM4MDE4ZTA0ZWJjYjg2ZTEyMzA0NTFiMQs=";
        public const string Addressable1SceneAddress = "Test Subject Addressable1";

        public const string Addressable2SceneName = "TestSubject_Addressable2";
        public const string Addressable2ScenePath = "Assets/Tests/Runtime/Subjects/TestSubject_Addressable2.unity";
        public static int Addressable2SceneBuildIndex => SceneUtility.GetBuildIndexByScenePath(Addressable2ScenePath);
        public const string Addressable2SceneGuid = "32b8f3692a793ae4693d74167b0f093f";
        public static string Addressable2SceneJsonRaw => GetRawJson(Addressable2SceneGuid);
        public static string Addressable2SceneXmlRaw => GetRawXml(Addressable2SceneGuid);
        public const string Addressable2SceneBinaryBase64 = "AAEAAAD/////AQAAAAAAAAAMAgAAAE1FZmxhdHVuLlNjZW5lUmVmZXJlbmNlLCBWZXJzaW9uPTAuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbAUBAAAAJUVmbGF0dW4uU2NlbmVSZWZlcmVuY2UuU2NlbmVSZWZlcmVuY2UBAAAAEXNjZW5lQXNzZXRHdWlkSGV4AQIAAAAGAwAAACAzMmI4ZjM2OTJhNzkzYWU0NjkzZDc0MTY3YjBmMDkzZgs=";
        public const string Addressable2SceneAddress = "Test Subject Addressable2";

        public const string AddressableDuplicateAddressASceneName = "TestSubject_AddressableDuplicateAddressA";
        public const string AddressableDuplicateAddressAScenePath = "Assets/Tests/Runtime/Subjects/TestSubject_AddressableDuplicateAddressA.unity";
        public static int AddressableDuplicateAddressASceneBuildIndex => SceneUtility.GetBuildIndexByScenePath(AddressableDuplicateAddressAScenePath);
        public const string AddressableDuplicateAddressASceneGuid = "97f5e006e871a7440b0ffe04cb128c15";
        public static string AddressableDuplicateAddressASceneJsonRaw => GetRawJson(AddressableDuplicateAddressASceneGuid);
        public static string AddressableDuplicateAddressASceneXmlRaw => GetRawXml(AddressableDuplicateAddressASceneGuid);
        public const string AddressableDuplicateAddressASceneBinaryBase64 = "AAEAAAD/////AQAAAAAAAAAMAgAAAE1FZmxhdHVuLlNjZW5lUmVmZXJlbmNlLCBWZXJzaW9uPTAuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbAUBAAAAJUVmbGF0dW4uU2NlbmVSZWZlcmVuY2UuU2NlbmVSZWZlcmVuY2UBAAAAEXNjZW5lQXNzZXRHdWlkSGV4AQIAAAAGAwAAACA5N2Y1ZTAwNmU4NzFhNzQ0MGIwZmZlMDRjYjEyOGMxNQs=";
        public const string AddressableDuplicateAddressASceneAddress = "Test Subject AddressableDuplicateAddress";

        public const string AddressableDuplicateAddressBSceneName = "TestSubject_AddressableDuplicateAddressB";
        public const string AddressableDuplicateAddressBScenePath = "Assets/Tests/Runtime/Subjects/TestSubject_AddressableDuplicateAddressB.unity";
        public static int AddressableDuplicateAddressBSceneBuildIndex => SceneUtility.GetBuildIndexByScenePath(AddressableDuplicateAddressBScenePath);
        public const string AddressableDuplicateAddressBSceneGuid = "c367bc2503743ea4b8abccc44b2fefbc";
        public static string AddressableDuplicateAddressBSceneJsonRaw => GetRawJson(AddressableDuplicateAddressBSceneGuid);
        public static string AddressableDuplicateAddressBSceneXmlRaw => GetRawXml(AddressableDuplicateAddressBSceneGuid);
        public const string AddressableDuplicateAddressBSceneBinaryBase64 = "AAEAAAD/////AQAAAAAAAAAMAgAAAE1FZmxhdHVuLlNjZW5lUmVmZXJlbmNlLCBWZXJzaW9uPTAuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbAUBAAAAJUVmbGF0dW4uU2NlbmVSZWZlcmVuY2UuU2NlbmVSZWZlcmVuY2UBAAAAEXNjZW5lQXNzZXRHdWlkSGV4AQIAAAAGAwAAACBjMzY3YmMyNTAzNzQzZWE0YjhhYmNjYzQ0YjJmZWZiYws=";
        public const string AddressableDuplicateAddressBSceneAddress = "Test Subject AddressableDuplicateAddress";

        public const string NonExistingAddress = "This Address Should Never Exist ___ Foo ___ Bar";

        public static bool IsAddressablesPackagePresent =>
#if ESR_ADDRESSABLES
            true
#else // ESR_ADDRESSABLES
            false
#endif // ESR_ADDRESSABLES
        ;

        public static void AssertEnabledSceneState(SceneReference sr)
        {
            Assert.AreEqual(EnabledSceneName, sr.Name);
            Assert.AreEqual(EnabledScenePath, sr.Path);

#if UNITY_EDITOR
            Assert.AreEqual(EnabledSceneName, sr.asset.name);
#endif

            Assert.AreEqual(EnabledSceneBuildIndex, sr.BuildIndex);
            Assert.AreEqual(EnabledSceneGuid, sr.Guid);
            Assert.AreEqual(EnabledSceneGuid, sr.guid);
            Assert.AreEqual(SceneReferenceState.Regular, sr.State);
            Assert.AreEqual(SceneReferenceUnsafeReason.None, sr.UnsafeReason);

            if (IsAddressablesPackagePresent)
            {
                Assert.Throws<SceneNotAddressableException>(() => _ = sr.Address);
            }
            else
            {
                Assert.Throws<AddressablesSupportDisabledException>(() => _ = sr.Address);
            }
        }

        public static void AssertDisabledSceneState(SceneReference sr)
        {
            Assert.AreEqual(DisabledSceneName, sr.Name);
            Assert.AreEqual(DisabledScenePath, sr.Path);

#if UNITY_EDITOR
            Assert.AreEqual(DisabledSceneName, sr.asset.name);
#endif

            Assert.AreEqual(DisabledSceneBuildIndex, sr.BuildIndex);
            Assert.AreEqual(DisabledSceneGuid, sr.Guid);
            Assert.AreEqual(DisabledSceneGuid, sr.guid);

// TODO: Unity seems to be enabling all scenes before making a test build.
// Figure out a way to disable that behaviour and then get rid of this define check.
#if UNITY_EDITOR
            Assert.AreEqual(SceneReferenceState.Unsafe, sr.State);
            Assert.AreEqual(SceneReferenceUnsafeReason.NotInBuild, sr.UnsafeReason);
#else // UNITY_EDITOR
            Assert.AreEqual(SceneReferenceState.Regular, sr.State);
            Assert.AreEqual(SceneReferenceUnsafeReason.None, sr.UnsafeReason);
#endif // UNITY_EDITOR

            if (IsAddressablesPackagePresent)
            {
                Assert.Throws<SceneNotAddressableException>(() => _ = sr.Address);
            }
            else
            {
                Assert.Throws<AddressablesSupportDisabledException>(() => _ = sr.Address);
            }
        }

        public static void AssertNotInBuildSceneState(SceneReference sr)
        {
            Assert.AreEqual(NotInBuildSceneName, sr.Name);
            Assert.AreEqual(NotInBuildScenePath, sr.Path);

#if UNITY_EDITOR
            Assert.AreEqual(NotInBuildSceneName, sr.asset.name);
#endif

            Assert.AreEqual(NotInBuildSceneBuildIndex, sr.BuildIndex);
            Assert.AreEqual(NotInBuildSceneGuid, sr.Guid);
            Assert.AreEqual(NotInBuildSceneGuid, sr.guid);
            Assert.AreEqual(SceneReferenceState.Unsafe, sr.State);
            Assert.AreEqual(SceneReferenceUnsafeReason.NotInBuild, sr.UnsafeReason);

            if (IsAddressablesPackagePresent)
            {
                Assert.Throws<SceneNotAddressableException>(() => _ = sr.Address);
            }
            else
            {
                Assert.Throws<AddressablesSupportDisabledException>(() => _ = sr.Address);
            }
        }

        public static void AssertEmptyState(SceneReference sr)
        {
            Assert.Throws<EmptySceneReferenceException>(() => _ = sr.Name);
            Assert.Throws<EmptySceneReferenceException>(() => _ = sr.Path);

#if UNITY_EDITOR
            Assert.IsFalse(!!sr.asset);
#endif // UNITY_EDITOR

            Assert.Throws<EmptySceneReferenceException>(() => _ = sr.BuildIndex);
            Assert.AreEqual(AllZeroGuid, sr.Guid);
            Assert.AreEqual(AllZeroGuid, sr.guid);
            Assert.AreEqual(SceneReferenceState.Unsafe, sr.State);
            Assert.AreEqual(SceneReferenceUnsafeReason.Empty, sr.UnsafeReason);

            if (IsAddressablesPackagePresent)
            {
                Assert.Throws<EmptySceneReferenceException>(() => _ = sr.Address);
            }
            else
            {
                Assert.Throws<AddressablesSupportDisabledException>(() => _ = sr.Address);
            }
        }

        public static void AssertDeletedSceneState(SceneReference sr) => AssertInvalidState(sr, DeletedSceneGuid);

        public static void AssertNotExistingState(SceneReference sr) => AssertInvalidState(sr, NotExistingGuid);

        public static void AssertNotSceneAssetState(SceneReference sr) => AssertInvalidState(sr, NotSceneAssetGuid);

        private static void AssertInvalidState(SceneReference sr, string expectedGuid)
        {
            Assert.Throws<InvalidSceneReferenceException>(() => _ = sr.Name);
            Assert.Throws<InvalidSceneReferenceException>(() => _ = sr.Path);

#if UNITY_EDITOR
            Assert.IsFalse(!!sr.asset);
#endif

            Assert.Throws<InvalidSceneReferenceException>(() => _ = sr.BuildIndex);
            Assert.AreEqual(expectedGuid, sr.Guid);
            Assert.AreEqual(expectedGuid, sr.guid);
            Assert.AreEqual(SceneReferenceState.Unsafe, sr.State);
            Assert.AreEqual(SceneReferenceUnsafeReason.NotInMaps, sr.UnsafeReason);

            if (IsAddressablesPackagePresent)
            {
                Assert.Throws<InvalidSceneReferenceException>(() => _ = sr.Address);
            }
            else
            {
                Assert.Throws<AddressablesSupportDisabledException>(() => _ = sr.Address);
            }
        }

        public static void AssertAddressable1SceneState(SceneReference sr) => AssertAddressableSceneState(sr, Addressable1SceneName, Addressable1ScenePath, Addressable1SceneGuid, Addressable1SceneAddress);

        public static void AssertAddressable2SceneState(SceneReference sr) => AssertAddressableSceneState(sr, Addressable2SceneName, Addressable2ScenePath, Addressable2SceneGuid, Addressable2SceneAddress);

        public static void AssertAddressableDuplicateAddressASceneState(SceneReference sr) => AssertAddressableSceneState(sr, AddressableDuplicateAddressASceneName, AddressableDuplicateAddressAScenePath, AddressableDuplicateAddressASceneGuid, AddressableDuplicateAddressASceneAddress);

        public static void AssertAddressableDuplicateAddressBSceneState(SceneReference sr) => AssertAddressableSceneState(sr, AddressableDuplicateAddressBSceneName, AddressableDuplicateAddressBScenePath, AddressableDuplicateAddressBSceneGuid, AddressableDuplicateAddressBSceneAddress);

        private static void AssertAddressableSceneState(SceneReference sr, string expectedName, string expectedPath, string expectedGuid, string expectedAddress)
        {
            Assert.AreEqual(expectedName, sr.Name);
            Assert.AreEqual(expectedPath, sr.Path);

#if UNITY_EDITOR
            Assert.AreEqual(expectedName, sr.asset.name);
#endif

            Assert.AreEqual(-1, sr.BuildIndex);
            Assert.AreEqual(expectedGuid, sr.Guid);
            Assert.AreEqual(expectedGuid, sr.guid);

            if (IsAddressablesPackagePresent)
            {
                Assert.AreEqual(SceneReferenceState.Addressable, sr.State);
                Assert.AreEqual(SceneReferenceUnsafeReason.None, sr.UnsafeReason);
                Assert.AreEqual(expectedAddress, sr.Address);
            }
            else
            {
                Assert.AreEqual(SceneReferenceState.Unsafe, sr.State);
                Assert.AreEqual(SceneReferenceUnsafeReason.NotInBuild, sr.UnsafeReason);
                Assert.Throws<AddressablesSupportDisabledException>(() => _ = sr.Address);
            }
        }

        public static bool IsScenePath(string path)
        {
            return Path.GetExtension(path) == ".unity";
        }

        public static string SerializeToJson(SceneReference sr)
        {
            return JsonConvert.SerializeObject(sr, Newtonsoft.Json.Formatting.None);
        }

        public static SceneReference DeserializeFromJson(string jsonRaw)
        {
            return JsonConvert.DeserializeObject<SceneReference>(jsonRaw);
        }

        public static string SerializeToBinaryBase64(SceneReference sr) =>
            BinarySerializationUtils.SerializeToBinaryBase64(sr);

        public static SceneReference DeserializeFromBinaryBase64(string binaryBase64) =>
            BinarySerializationUtils.DeserializeFromBinaryBase64<SceneReference>(binaryBase64);

        public static string SerializeToXml(SceneReference sr)
        {
            var xmlSerializer = new XmlSerializer(typeof(SceneReference));
            var sb = new StringBuilder();
            using var xmlWriter = XmlWriter.Create(sb);
            xmlSerializer.Serialize(xmlWriter, sr);
            return sb.ToString();
        }

        public static SceneReference DeserializeFromXml(string xmlRaw)
        {
            var xmlSerializer = new XmlSerializer(typeof(SceneReference));
            using var stringReader = new StringReader(xmlRaw);
            using var xmlReader = XmlReader.Create(stringReader);
            return xmlSerializer.Deserialize(xmlReader) as SceneReference;
        }

        public static void IgnoreIfAddressablesSupportIsDisabled()
        {
            if (!IsAddressablesPackagePresent)
            {
                Assert.Ignore("This test is meaningless when Addressables support is disabled.");
            }
        }

        public static void IgnoreIfAddressablesSupportIsEnabled()
        {
            if (IsAddressablesPackagePresent)
            {
                Assert.Ignore("This test is meaningless when Addressables support is enabled.");
            }
        }

        private static string GetRawJson(string guid) => @$"{{""{SceneReference.CustomSerializationGuidKey}"":""{guid}""}}";

        private static string GetRawXml(string guid) => $@"<?xml version=""1.0"" encoding=""utf-16""?><{SceneReference.XmlRootElementName}>{guid}</{SceneReference.XmlRootElementName}>";
    }
}
