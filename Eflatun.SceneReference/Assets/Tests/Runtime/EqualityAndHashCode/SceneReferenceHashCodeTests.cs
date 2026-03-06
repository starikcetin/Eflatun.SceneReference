using System;
using System.Collections;
using Eflatun.SceneReference.Tests.Runtime.Subjects;
using Eflatun.SceneReference.Tests.Runtime.Utils;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Eflatun.SceneReference.Tests.Runtime.EqualityAndHashCode
{
    public class SceneReferenceHashCodeTests
    {
        [UnitySetUp]
        public IEnumerator Setup() => TestSubjectContainer.CacheIfNotAlready();

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
                ConceptionType.DeserializedViaNewtonsoftJson => TestUtils.DeserializeViaNewtonsoftJson(TestUtils.GetExpectedOutputOfNewtonsoftJson(guid)),
                ConceptionType.DeserializedViaSystemXml => TestUtils.DeserializeViaSystemXml(TestUtils.GetExpectedOutputOfSystemXml(guid)),
                ConceptionType.DeserializedViaBinaryFormatter => TestUtils.DeserializeFromBase64ViaBinaryFormatter(TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(guid)),
                ConceptionType.UnitySerialized => TestSubjectContainer.GetSceneReference(sceneType),
                _ => throw new ArgumentOutOfRangeException(nameof(conceptionType), conceptionType, null),
            };

            Assert.AreEqual(
                StringComparer.OrdinalIgnoreCase.GetHashCode(guid),
                sr.GetHashCode()
            );
        }

        [Test]
        public void Valid_DifferentGuidCasing_SameHashCode(
            [Values] SceneType sceneType
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

            var srLower = new SceneReference(guid);
            var srUpper = new SceneReference(guid.ToUpperInvariant());

            Assert.AreEqual(srLower.GetHashCode(), srUpper.GetHashCode());
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
                            TestSubjectContainer.NotExisting.Field.GetHashCode()
                        );

                        return;
                    }

                    case InvalidReason.NotSceneAsset:
                    {
                        Assert.AreEqual(
                            StringComparer.OrdinalIgnoreCase.GetHashCode(TestUtils.NotSceneAssetGuid),
                            TestSubjectContainer.NotSceneAsset.Field.GetHashCode()
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
                ConceptionType.DeserializedViaNewtonsoftJson => TestUtils.DeserializeViaNewtonsoftJson(TestUtils.GetExpectedOutputOfNewtonsoftJson(guid)),
                ConceptionType.DeserializedViaSystemXml => TestUtils.DeserializeViaSystemXml(TestUtils.GetExpectedOutputOfSystemXml(guid)),
                ConceptionType.DeserializedViaBinaryFormatter => TestUtils.DeserializeFromBase64ViaBinaryFormatter(TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(guid)),
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
