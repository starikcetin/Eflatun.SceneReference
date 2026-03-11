using System;
using System.Collections.Generic;
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

        public const string DisabledSceneName = "TestSubject_Disabled";
        public const string DisabledScenePath = "Assets/Tests/Runtime/Subjects/TestSubject_Disabled.unity";
        public static int DisabledSceneBuildIndex => SceneUtility.GetBuildIndexByScenePath(DisabledScenePath);
        public const string DisabledSceneGuid = "7e37b14fa3517514a91937cec5cad27a";

        public const string NotInBuildSceneName = "TestSubject_NotInBuild";
        public const string NotInBuildScenePath = "Assets/Tests/Runtime/Subjects/TestSubject_NotInBuild.unity";
        public static int NotInBuildSceneBuildIndex => SceneUtility.GetBuildIndexByScenePath(NotInBuildScenePath);
        public const string NotInBuildSceneGuid = "63c386231869c904c9b701dd79268476";

        public const string AllZeroGuid = "00000000000000000000000000000000";

        public const string DeletedSceneGuid = "69c1683d94db0cc469d86e4e865f9f5d";

        public const string NotExistingGuid = "2bc1683d94d80cc269d85e4e8a5fcf5d";

        public const string NotSceneAssetGuid = "99d2aa5f58f54c44fba8671b66be5259";
        public const string NotSceneAssetPath = "Assets/Tests/Runtime/Utils/TestSubject_Material.mat";

        public const string Addressable1SceneName = "TestSubject_Addressable1";
        public const string Addressable1ScenePath = "Assets/Tests/Runtime/Subjects/TestSubject_Addressable1.unity";
        public static int Addressable1SceneBuildIndex => SceneUtility.GetBuildIndexByScenePath(Addressable1ScenePath);
        public const string Addressable1SceneGuid = "8b3f523138018e04ebcb86e1230451b1";
        public const string Addressable1SceneAddress = "Test Subject Addressable1";

        public const string Addressable2SceneName = "TestSubject_Addressable2";
        public const string Addressable2ScenePath = "Assets/Tests/Runtime/Subjects/TestSubject_Addressable2.unity";
        public static int Addressable2SceneBuildIndex => SceneUtility.GetBuildIndexByScenePath(Addressable2ScenePath);
        public const string Addressable2SceneGuid = "32b8f3692a793ae4693d74167b0f093f";
        public const string Addressable2SceneAddress = "Test Subject Addressable2";

        public const string AddressableDuplicateAddressASceneName = "TestSubject_AddressableDuplicateAddressA";
        public const string AddressableDuplicateAddressAScenePath = "Assets/Tests/Runtime/Subjects/TestSubject_AddressableDuplicateAddressA.unity";
        public static int AddressableDuplicateAddressASceneBuildIndex => SceneUtility.GetBuildIndexByScenePath(AddressableDuplicateAddressAScenePath);
        public const string AddressableDuplicateAddressASceneGuid = "97f5e006e871a7440b0ffe04cb128c15";
        public const string AddressableDuplicateAddressASceneAddress = "Test Subject AddressableDuplicateAddress";

        public const string AddressableDuplicateAddressBSceneName = "TestSubject_AddressableDuplicateAddressB";
        public const string AddressableDuplicateAddressBScenePath = "Assets/Tests/Runtime/Subjects/TestSubject_AddressableDuplicateAddressB.unity";
        public static int AddressableDuplicateAddressBSceneBuildIndex => SceneUtility.GetBuildIndexByScenePath(AddressableDuplicateAddressBScenePath);
        public const string AddressableDuplicateAddressBSceneGuid = "c367bc2503743ea4b8abccc44b2fefbc";
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

            var couldGetName = sr.TryGetName(out var name);
            Assert.IsTrue(couldGetName);
            Assert.AreEqual(EnabledSceneName, name);

            Assert.AreEqual(EnabledScenePath, sr.Path);

            var couldGetPath = sr.TryGetPath(out var path);
            Assert.IsTrue(couldGetPath);
            Assert.AreEqual(EnabledScenePath, path);

#if UNITY_EDITOR
            Assert.AreEqual(EnabledSceneName, sr.asset.name);
#endif

            Assert.AreEqual(EnabledSceneBuildIndex, sr.BuildIndex);

            var couldGetBuildIndex = sr.TryGetBuildIndex(out var buildIndex);
            Assert.IsTrue(couldGetBuildIndex);
            Assert.AreEqual(EnabledSceneBuildIndex, buildIndex);

            StringAssert.AreEqualIgnoringCase(EnabledSceneGuid, sr.Guid);
            StringAssert.AreEqualIgnoringCase(EnabledSceneGuid, sr.guid);
            Assert.AreEqual(SceneReferenceState.Regular, sr.State);
            Assert.AreEqual(SceneReferenceUnsafeReason.None, sr.UnsafeReason);

            if (IsAddressablesPackagePresent)
            {
                Assert.Throws<SceneNotAddressableException>(() => _ = sr.Address);

                var couldGetAddress = sr.TryGetAddress(out var address);
                Assert.IsFalse(couldGetAddress);
                Assert.IsNull(address);
            }
            else
            {
                Assert.Throws<AddressablesSupportDisabledException>(() => _ = sr.Address);
                Assert.Throws<AddressablesSupportDisabledException>(() => sr.TryGetAddress(out _));
            }
        }

        public static void AssertDisabledSceneState(SceneReference sr)
        {
            Assert.AreEqual(DisabledSceneName, sr.Name);

            var couldGetName = sr.TryGetName(out var name);
            Assert.IsTrue(couldGetName);
            Assert.AreEqual(DisabledSceneName, name);

            Assert.AreEqual(DisabledScenePath, sr.Path);

            var couldGetPath = sr.TryGetPath(out var path);
            Assert.IsTrue(couldGetPath);
            Assert.AreEqual(DisabledScenePath, path);

#if UNITY_EDITOR
            Assert.AreEqual(DisabledSceneName, sr.asset.name);
#endif

            Assert.AreEqual(DisabledSceneBuildIndex, sr.BuildIndex);

            var couldGetBuildIndex = sr.TryGetBuildIndex(out var buildIndex);
            Assert.IsTrue(couldGetBuildIndex);
            Assert.AreEqual(DisabledSceneBuildIndex, buildIndex);

            StringAssert.AreEqualIgnoringCase(DisabledSceneGuid, sr.Guid);
            StringAssert.AreEqualIgnoringCase(DisabledSceneGuid, sr.guid);

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

                var couldGetAddress = sr.TryGetAddress(out var address);
                Assert.IsFalse(couldGetAddress);
                Assert.IsNull(address);
            }
            else
            {
                Assert.Throws<AddressablesSupportDisabledException>(() => _ = sr.Address);
                Assert.Throws<AddressablesSupportDisabledException>(() => sr.TryGetAddress(out _));
            }
        }

        public static void AssertNotInBuildSceneState(SceneReference sr)
        {
            Assert.AreEqual(NotInBuildSceneName, sr.Name);

            var couldGetName = sr.TryGetName(out var name);
            Assert.IsTrue(couldGetName);
            Assert.AreEqual(NotInBuildSceneName, name);

            Assert.AreEqual(NotInBuildScenePath, sr.Path);

            var couldGetPath = sr.TryGetPath(out var path);
            Assert.IsTrue(couldGetPath);
            Assert.AreEqual(NotInBuildScenePath, path);

#if UNITY_EDITOR
            Assert.AreEqual(NotInBuildSceneName, sr.asset.name);
#endif

            Assert.AreEqual(NotInBuildSceneBuildIndex, sr.BuildIndex);

            var couldGetBuildIndex = sr.TryGetBuildIndex(out var buildIndex);
            Assert.IsTrue(couldGetBuildIndex);
            Assert.AreEqual(NotInBuildSceneBuildIndex, buildIndex);

            StringAssert.AreEqualIgnoringCase(NotInBuildSceneGuid, sr.Guid);
            StringAssert.AreEqualIgnoringCase(NotInBuildSceneGuid, sr.guid);
            Assert.AreEqual(SceneReferenceState.Unsafe, sr.State);
            Assert.AreEqual(SceneReferenceUnsafeReason.NotInBuild, sr.UnsafeReason);

            if (IsAddressablesPackagePresent)
            {
                Assert.Throws<SceneNotAddressableException>(() => _ = sr.Address);

                var couldGetAddress = sr.TryGetAddress(out var address);
                Assert.IsFalse(couldGetAddress);
                Assert.IsNull(address);
            }
            else
            {
                Assert.Throws<AddressablesSupportDisabledException>(() => _ = sr.Address);
                Assert.Throws<AddressablesSupportDisabledException>(() => sr.TryGetAddress(out _));
            }
        }

        public static void AssertEmptyState(SceneReference sr)
        {
            Assert.Throws<EmptySceneReferenceException>(() => _ = sr.Name);

            var couldGetName = sr.TryGetName(out var name);
            Assert.IsFalse(couldGetName);
            Assert.IsNull(name);

            Assert.Throws<EmptySceneReferenceException>(() => _ = sr.Path);

            var couldGetPath = sr.TryGetPath(out var path);
            Assert.IsFalse(couldGetPath);
            Assert.IsNull(path);

#if UNITY_EDITOR
            Assert.IsFalse(!!sr.asset);
#endif // UNITY_EDITOR

            Assert.Throws<EmptySceneReferenceException>(() => _ = sr.BuildIndex);

            var couldGetBuildIndex = sr.TryGetBuildIndex(out var buildIndex);
            Assert.IsFalse(couldGetBuildIndex);
            Assert.AreEqual(-1, buildIndex);

            StringAssert.AreEqualIgnoringCase(AllZeroGuid, sr.Guid);
            StringAssert.AreEqualIgnoringCase(AllZeroGuid, sr.guid);
            Assert.AreEqual(SceneReferenceState.Unsafe, sr.State);
            Assert.AreEqual(SceneReferenceUnsafeReason.Empty, sr.UnsafeReason);

            if (IsAddressablesPackagePresent)
            {
                Assert.Throws<EmptySceneReferenceException>(() => _ = sr.Address);

                var couldGetAddress = sr.TryGetAddress(out var address);
                Assert.IsFalse(couldGetAddress);
                Assert.IsNull(address);
            }
            else
            {
                Assert.Throws<AddressablesSupportDisabledException>(() => _ = sr.Address);
                Assert.Throws<AddressablesSupportDisabledException>(() => sr.TryGetAddress(out _));
            }
        }

        public static void AssertDeletedSceneState(SceneReference sr) => AssertInvalidState(sr, DeletedSceneGuid);

        public static void AssertNotExistingState(SceneReference sr) => AssertInvalidState(sr, NotExistingGuid);

        public static void AssertNotSceneAssetState(SceneReference sr) => AssertInvalidState(sr, NotSceneAssetGuid);

        private static void AssertInvalidState(SceneReference sr, string expectedGuid)
        {
            Assert.Throws<InvalidSceneReferenceException>(() => _ = sr.Name);

            var couldGetName = sr.TryGetName(out var name);
            Assert.IsFalse(couldGetName);
            Assert.IsNull(name);

            Assert.Throws<InvalidSceneReferenceException>(() => _ = sr.Path);

            var couldGetPath = sr.TryGetPath(out var path);
            Assert.IsFalse(couldGetPath);
            Assert.IsNull(path);

#if UNITY_EDITOR
            Assert.IsFalse(!!sr.asset);
#endif

            Assert.Throws<InvalidSceneReferenceException>(() => _ = sr.BuildIndex);

            var couldGetBuildIndex = sr.TryGetBuildIndex(out var buildIndex);
            Assert.IsFalse(couldGetBuildIndex);
            Assert.AreEqual(-1, buildIndex);

            StringAssert.AreEqualIgnoringCase(expectedGuid, sr.Guid);
            StringAssert.AreEqualIgnoringCase(expectedGuid, sr.guid);
            Assert.AreEqual(SceneReferenceState.Unsafe, sr.State);
            Assert.AreEqual(SceneReferenceUnsafeReason.NotInMaps, sr.UnsafeReason);

            if (IsAddressablesPackagePresent)
            {
                Assert.Throws<InvalidSceneReferenceException>(() => _ = sr.Address);

                var couldGetAddress = sr.TryGetAddress(out var address);
                Assert.IsFalse(couldGetAddress);
                Assert.IsNull(address);
            }
            else
            {
                Assert.Throws<AddressablesSupportDisabledException>(() => _ = sr.Address);
                Assert.Throws<AddressablesSupportDisabledException>(() => sr.TryGetAddress(out _));
            }
        }

        public static void AssertAddressable1SceneState(SceneReference sr) => AssertAddressableSceneState(sr, Addressable1SceneName, Addressable1ScenePath, Addressable1SceneGuid, Addressable1SceneAddress);

        public static void AssertAddressable2SceneState(SceneReference sr) => AssertAddressableSceneState(sr, Addressable2SceneName, Addressable2ScenePath, Addressable2SceneGuid, Addressable2SceneAddress);

        public static void AssertAddressableDuplicateAddressASceneState(SceneReference sr) => AssertAddressableSceneState(sr, AddressableDuplicateAddressASceneName, AddressableDuplicateAddressAScenePath, AddressableDuplicateAddressASceneGuid, AddressableDuplicateAddressASceneAddress);

        public static void AssertAddressableDuplicateAddressBSceneState(SceneReference sr) => AssertAddressableSceneState(sr, AddressableDuplicateAddressBSceneName, AddressableDuplicateAddressBScenePath, AddressableDuplicateAddressBSceneGuid, AddressableDuplicateAddressBSceneAddress);

        private static void AssertAddressableSceneState(SceneReference sr, string expectedName, string expectedPath, string expectedGuid, string expectedAddress)
        {
            Assert.AreEqual(expectedName, sr.Name);

            var couldGetName = sr.TryGetName(out var name);
            Assert.IsTrue(couldGetName);
            Assert.AreEqual(expectedName, name);

            Assert.AreEqual(expectedPath, sr.Path);

            var couldGetPath = sr.TryGetPath(out var path);
            Assert.IsTrue(couldGetPath);
            Assert.AreEqual(expectedPath, path);

#if UNITY_EDITOR
            Assert.AreEqual(expectedName, sr.asset.name);
#endif

            Assert.AreEqual(-1, sr.BuildIndex);

            var couldGetBuildIndex = sr.TryGetBuildIndex(out var buildIndex);
            Assert.IsTrue(couldGetBuildIndex);
            Assert.AreEqual(-1, buildIndex);

            StringAssert.AreEqualIgnoringCase(expectedGuid, sr.Guid);
            StringAssert.AreEqualIgnoringCase(expectedGuid, sr.guid);

            if (IsAddressablesPackagePresent)
            {
                Assert.AreEqual(SceneReferenceState.Addressable, sr.State);
                Assert.AreEqual(SceneReferenceUnsafeReason.None, sr.UnsafeReason);
                Assert.AreEqual(expectedAddress, sr.Address);

                var couldGetAddress = sr.TryGetAddress(out var address);
                Assert.IsTrue(couldGetAddress);
                Assert.AreEqual(expectedAddress, address);
            }
            else
            {
                Assert.AreEqual(SceneReferenceState.Unsafe, sr.State);
                Assert.AreEqual(SceneReferenceUnsafeReason.NotInBuild, sr.UnsafeReason);
                Assert.Throws<AddressablesSupportDisabledException>(() => _ = sr.Address);
                Assert.Throws<AddressablesSupportDisabledException>(() => sr.TryGetAddress(out _));
            }
        }

        public static bool IsScenePath(string path)
        {
            return Path.GetExtension(path) == ".unity";
        }

        public static string SerializeViaNewtonsoftJson(SceneReference sceneReference)
            => JsonConvert.SerializeObject(sceneReference, Newtonsoft.Json.Formatting.None);

        public static SceneReference DeserializeViaNewtonsoftJson(string json)
            => JsonConvert.DeserializeObject<SceneReference>(json);

        public static string SerializeToBase64ViaBinaryFormatter(SceneReference sceneReference) =>
            BinaryFormatterUtils.SerializeToBase64ViaBinaryFormatter(sceneReference);

        public static SceneReference DeserializeFromBase64ViaBinaryFormatter(string base64) =>
            BinaryFormatterUtils.DeserializeFromBase64ViaBinaryFormatter<SceneReference>(base64);

        public static string SerializeViaSystemXml(SceneReference sceneReference)
        {
            var xmlSerializer = new XmlSerializer(typeof(SceneReference));
            var sb = new StringBuilder();
            using var xmlWriter = XmlWriter.Create(sb);
            xmlSerializer.Serialize(xmlWriter, sceneReference);
            return sb.ToString();
        }

        public static SceneReference DeserializeViaSystemXml(string xml)
        {
            var xmlSerializer = new XmlSerializer(typeof(SceneReference));
            using var stringReader = new StringReader(xml);
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

        public static string GetExpectedOutputOfNewtonsoftJson(string guid)
            => @$"{{""{SceneReference.CustomSerializationGuidKey}"":""{guid}""}}";

        public static string GetExpectedOutputOfSystemXml(string guid)
            => $@"<?xml version=""1.0"" encoding=""utf-16""?><{SceneReference.XmlRootElementName}>{guid}</{SceneReference.XmlRootElementName}>";

        public static string GetAsBase64ExpectedOutputOfBinaryFormatter(string guid)
        {
            // BinaryFormatter output format reference: https://stackoverflow.com/a/30176566/6301627

            var bytes = new List<byte>
            {
                // Before GUID
                0x00, 0x01, 0x00, 0x00, 0x00, 0xff, 0xff, 0xff,
                0xff, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x0c, 0x02, 0x00, 0x00, 0x00, 0x4d, 0x45,
                0x66, 0x6c, 0x61, 0x74, 0x75, 0x6e, 0x2e, 0x53,
                0x63, 0x65, 0x6e, 0x65, 0x52, 0x65, 0x66, 0x65,
                0x72, 0x65, 0x6e, 0x63, 0x65, 0x2c, 0x20, 0x56,
                0x65, 0x72, 0x73, 0x69, 0x6f, 0x6e, 0x3d, 0x30,
                0x2e, 0x30, 0x2e, 0x30, 0x2e, 0x30, 0x2c, 0x20,
                0x43, 0x75, 0x6c, 0x74, 0x75, 0x72, 0x65, 0x3d,
                0x6e, 0x65, 0x75, 0x74, 0x72, 0x61, 0x6c, 0x2c,
                0x20, 0x50, 0x75, 0x62, 0x6c, 0x69, 0x63, 0x4b,
                0x65, 0x79, 0x54, 0x6f, 0x6b, 0x65, 0x6e, 0x3d,
                0x6e, 0x75, 0x6c, 0x6c, 0x05, 0x01, 0x00, 0x00,
                0x00, 0x25, 0x45, 0x66, 0x6c, 0x61, 0x74, 0x75,
                0x6e, 0x2e, 0x53, 0x63, 0x65, 0x6e, 0x65, 0x52,
                0x65, 0x66, 0x65, 0x72, 0x65, 0x6e, 0x63, 0x65,
                0x2e, 0x53, 0x63, 0x65, 0x6e, 0x65, 0x52, 0x65,
                0x66, 0x65, 0x72, 0x65, 0x6e, 0x63, 0x65, 0x01,
                0x00, 0x00, 0x00, 0x11, 0x73, 0x63, 0x65, 0x6e,
                0x65, 0x41, 0x73, 0x73, 0x65, 0x74, 0x47, 0x75,
                0x69, 0x64, 0x48, 0x65, 0x78, 0x01, 0x02, 0x00,
                0x00, 0x00, 0x06, 0x03, 0x00, 0x00, 0x00,

                // GUID length
                (byte)Encoding.UTF8.GetByteCount(guid),
            };

            // GUID
            bytes.AddRange(Encoding.UTF8.GetBytes(guid));

            // After GUID: MessageEnd record
            bytes.Add(0x0b);

            return Convert.ToBase64String(bytes.ToArray());
        }
    }
}
