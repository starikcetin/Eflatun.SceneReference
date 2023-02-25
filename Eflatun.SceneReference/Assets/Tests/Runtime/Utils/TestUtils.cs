using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
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

        public static void AssertEnabledSceneState(SceneReference sr)
        {
            Assert.AreEqual(EnabledSceneName, sr.Name);
            Assert.AreEqual(EnabledScenePath, sr.Path);

#if UNITY_EDITOR
            Assert.AreEqual(EnabledSceneName, sr.asset.name);
#endif

            Assert.AreEqual(EnabledSceneBuildIndex, sr.BuildIndex);
            Assert.IsTrue(sr.HasValue);
            Assert.AreEqual(EnabledSceneGuid, sr.Guid);
            Assert.AreEqual(EnabledSceneGuid, sr.guid);
            Assert.IsTrue(sr.IsSafeToUse);
            Assert.IsTrue(sr.IsInBuildAndEnabled);
            Assert.IsTrue(sr.IsInSceneGuidToPathMap);
        }

        public static void AssertDisabledSceneState(SceneReference sr)
        {
            Assert.AreEqual(DisabledSceneName, sr.Name);
            Assert.AreEqual(DisabledScenePath, sr.Path);

#if UNITY_EDITOR
            Assert.AreEqual(DisabledSceneName, sr.asset.name);
#endif

            Assert.AreEqual(DisabledSceneBuildIndex, sr.BuildIndex);
            Assert.IsTrue(sr.HasValue);
            Assert.AreEqual(DisabledSceneGuid, sr.Guid);
            Assert.AreEqual(DisabledSceneGuid, sr.guid);

// TODO: Unity seems to be enabling all scenes before making a test build.
// Figure out a way to disable that behaviour and then get rid of this define check.
#if UNITY_EDITOR
            Assert.IsFalse(sr.IsSafeToUse);
            Assert.IsFalse(sr.IsInBuildAndEnabled);
#else // UNITY_EDITOR
            Assert.IsTrue(sr.IsSafeToUse);
            Assert.IsTrue(sr.IsInBuildAndEnabled);
#endif // UNITY_EDITOR

            Assert.IsTrue(sr.IsInSceneGuidToPathMap);
        }

        public static void AssertNotInBuildSceneState(SceneReference sr)
        {
            Assert.AreEqual(NotInBuildSceneName, sr.Name);
            Assert.AreEqual(NotInBuildScenePath, sr.Path);

#if UNITY_EDITOR
            Assert.AreEqual(NotInBuildSceneName, sr.asset.name);
#endif

            Assert.AreEqual(NotInBuildSceneBuildIndex, sr.BuildIndex);
            Assert.IsTrue(sr.HasValue);
            Assert.AreEqual(NotInBuildSceneGuid, sr.Guid);
            Assert.AreEqual(NotInBuildSceneGuid, sr.guid);
            Assert.IsFalse(sr.IsSafeToUse);
            Assert.IsFalse(sr.IsInBuildAndEnabled);
            Assert.IsTrue(sr.IsInSceneGuidToPathMap);
        }

        public static void AssertEmptyState(SceneReference sr)
        {
            Assert.Throws<EmptySceneReferenceException>(() => _ = sr.Name);
            Assert.Throws<EmptySceneReferenceException>(() => _ = sr.Path);

#if UNITY_EDITOR
            Assert.IsFalse(!!sr.asset);
#endif // UNITY_EDITOR

            Assert.Throws<EmptySceneReferenceException>(() => _ = sr.BuildIndex);
            Assert.IsFalse(sr.HasValue);
            Assert.AreEqual(AllZeroGuid, sr.Guid);
            Assert.AreEqual(AllZeroGuid, sr.guid);
            Assert.IsFalse(sr.IsSafeToUse);
            Assert.Throws<EmptySceneReferenceException>(() => _ = sr.IsInBuildAndEnabled);
            Assert.Throws<EmptySceneReferenceException>(() => _ = sr.IsInSceneGuidToPathMap);
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
            Assert.IsTrue(sr.HasValue);
            Assert.AreEqual(expectedGuid, sr.Guid);
            Assert.AreEqual(expectedGuid, sr.guid);
            Assert.IsFalse(sr.IsSafeToUse);
            Assert.Throws<InvalidSceneReferenceException>(() => _ = sr.IsInBuildAndEnabled);
            Assert.IsFalse(sr.IsInSceneGuidToPathMap);
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

        public static string SerializeToBinaryBase64(SceneReference sr)
        {
            var bf = new BinaryFormatter();
            using var ms = new MemoryStream();
            bf.Serialize(ms, sr);
            var serializedBytes = ms.ToArray();
            return Convert.ToBase64String(serializedBytes);
        }

        public static SceneReference DeserializeFromBinaryBase64(string binaryBase64)
        {
            var bytes = Convert.FromBase64String(binaryBase64);
            var bf = new BinaryFormatter();
            using var ms = new MemoryStream(bytes);
            return bf.Deserialize(ms) as SceneReference;
        }

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

        private static string GetRawJson(string guid) => @$"{{""{SceneReference.CustomSerializationGuidKey}"":""{guid}""}}";
        private static string GetRawXml(string guid) => $@"<?xml version=""1.0"" encoding=""utf-16""?><{SceneReference.XmlRootElementName}>{guid}</{SceneReference.XmlRootElementName}>";
    }
}
