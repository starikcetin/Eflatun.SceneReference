using System;
using System.Collections;
using Eflatun.SceneReference.Tests.Runtime.Subjects;
using Eflatun.SceneReference.Tests.Runtime.Utils;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Eflatun.SceneReference.Tests.Runtime.EqualityAndHashCode
{
    public class SceneReferenceHashCodeTests
    {
        private TestSubjectContainer _testMb;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            yield return SceneManager.LoadSceneAsync(TestUtils.TestSubjectContainerScenePath, LoadSceneMode.Additive);
            _testMb = UnityEngine.Object.FindObjectOfType<TestSubjectContainer>();
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            var scene = SceneManager.GetSceneByPath(TestUtils.TestSubjectContainerScenePath);
            yield return SceneManager.UnloadSceneAsync(scene);
            _testMb = null;
        }

        [Test]
        public void Valid(
            [Values] SceneType sceneType,
            [Values] ConceptionType conceptionType
        ) {
            var guid = sceneType switch
            {
                SceneType.NotInBuild => TestUtils.NotInBuildSceneGuid,
                SceneType.Disabled => TestUtils.DisabledSceneGuid,
                SceneType.Enabled => TestUtils.EnabledSceneGuid,
                SceneType.Addressable1 => TestUtils.Addressable1SceneGuid,
                SceneType.Addressable2 => TestUtils.Addressable2SceneGuid,
                SceneType.AddressableDuplicateAddressA => TestUtils.AddressableDuplicateAddressASceneGuid,
                SceneType.AddressableDuplicateAddressB => TestUtils.AddressableDuplicateAddressBSceneGuid,
                _ => throw new ArgumentOutOfRangeException(nameof(sceneType), sceneType, null),
            };

            var sr = conceptionType switch
            {
                ConceptionType.CreatedInCode => new SceneReference(guid),
                ConceptionType.DeserializedFromJson => TestUtils.DeserializeFromJson(TestUtils.GetRawJson(guid)),
                ConceptionType.DeserializedFromXml => TestUtils.DeserializeFromXml(TestUtils.GetRawXml(guid)),
                ConceptionType.DeserializedFromBinary => TestUtils.DeserializeFromBinaryBase64(TestUtils.GetRawBinaryBase64(guid)),
                ConceptionType.UnitySerialized => _testMb.GetSceneReference(sceneType),
                _ => throw new ArgumentOutOfRangeException(nameof(conceptionType), conceptionType, null),
            };

            Assert.AreEqual(
                StringComparer.OrdinalIgnoreCase.GetHashCode(guid),
                sr.GetHashCode()
            );
        }

        [Test]
        public void Invalid(
            [Values] ConceptionType conceptionType,
            [Values] InvalidReason invalidReason
        ) {
            if (conceptionType == ConceptionType.CreatedInCode && (invalidReason == InvalidReason.InvalidGuid || invalidReason == InvalidReason.NotExisting || invalidReason == InvalidReason.NotSceneAsset))
            {
                Assert.Ignore("It is not possible to create a SceneReference in code with a guid that is not a valid scene asset because the constructor validates the guid and throws if it is not for a valid scene asset.");
            }

            if (invalidReason == InvalidReason.Null)
            {
                Assert.Ignore("It is not possible to test an instance method on a null reference.");
            }

            if (invalidReason == InvalidReason.Empty)
            {
                Assert.AreEqual(
                    StringComparer.OrdinalIgnoreCase.GetHashCode(TestUtils.AllZeroGuid),
                    new SceneReference().GetHashCode()
                );

                return;
            }

            if (conceptionType == ConceptionType.UnitySerialized)
            {
                if (invalidReason == InvalidReason.InvalidGuid)
                {
                    Assert.Ignore("It would take a very dedicated person doing very weird things to get a Unity serialized SceneReference with an invalid guid.");
                }

                switch (invalidReason)
                {
                    case InvalidReason.Null:
                    case InvalidReason.Empty:
                    case InvalidReason.InvalidGuid:
                        throw new Exception("This branch is already handled above with an if statement.");

                    case InvalidReason.NotExisting:
                    {
                        Assert.AreEqual(
                            StringComparer.OrdinalIgnoreCase.GetHashCode(TestUtils.NotExistingGuid),
                            _testMb.fieldNotExisting.GetHashCode()
                        );

                        return;
                    }

                    case InvalidReason.NotSceneAsset:
                    {
                        Assert.AreEqual(
                            StringComparer.OrdinalIgnoreCase.GetHashCode(TestUtils.NotSceneAssetGuid),
                            _testMb.fieldNotSceneAsset.GetHashCode()
                        );

                        return;
                    }

                    default:
                        throw new ArgumentOutOfRangeException(nameof(invalidReason), invalidReason, null);
                }
            }

            var guid = invalidReason switch
            {
                InvalidReason.Null => throw new Exception("This branch is already handled above with an if statement."),
                InvalidReason.Empty => throw new Exception("This branch is already handled above with an if statement."),
                InvalidReason.InvalidGuid => "invalid guid",
                InvalidReason.NotExisting => TestUtils.NotExistingGuid,
                InvalidReason.NotSceneAsset => TestUtils.NotSceneAssetGuid,
                _ => throw new ArgumentOutOfRangeException(nameof(invalidReason), invalidReason, null),
            };

            var sr = conceptionType switch
            {
                ConceptionType.CreatedInCode => new SceneReference(guid),
                ConceptionType.DeserializedFromJson => TestUtils.DeserializeFromJson(TestUtils.GetRawJson(guid)),
                ConceptionType.DeserializedFromXml => TestUtils.DeserializeFromXml(TestUtils.GetRawXml(guid)),
                ConceptionType.DeserializedFromBinary => TestUtils.DeserializeFromBinaryBase64(TestUtils.GetRawBinaryBase64(guid)),
                ConceptionType.UnitySerialized => throw new Exception("This branch is already handled above with an if statement."),
                _ => throw new ArgumentOutOfRangeException(nameof(conceptionType), conceptionType, null),
            };

            Assert.AreEqual(
                StringComparer.OrdinalIgnoreCase.GetHashCode(guid),
                sr.GetHashCode()
            );
        }
    }
}
