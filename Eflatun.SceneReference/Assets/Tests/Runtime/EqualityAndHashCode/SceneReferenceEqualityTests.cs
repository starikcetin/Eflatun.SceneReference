using System;
using System.Collections;
using Eflatun.SceneReference.Tests.Runtime.Subjects;
using Eflatun.SceneReference.Tests.Runtime.Utils;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Eflatun.SceneReference.Tests.Runtime.EqualityAndHashCode
{
    public class SceneReferenceEqualityTests
    {
        [UnitySetUp]
        public IEnumerator Setup() => TestSubjectContainer.CacheIfNotAlready();

        [Test]
        public void Valid_SameRef(
            [Values] SceneType sceneType,
            [Values] ConceptionType conceptionType
        ) => AssertEquality(
            true,
            CreateValid(sceneType, conceptionType)
        );

        [Test]
        public void Invalid_SameRef(
            [Values] ConceptionType conceptionType,
            [Values] InvalidReason invalidReason
        ) => AssertEquality(
            true,
            CreateInvalid(conceptionType, invalidReason)
        );

        [Test]
        public void Valid_Valid(
            [Values] SceneType sceneTypeA,
            [Values] ConceptionType conceptionTypeA,
            [Values] SceneType sceneTypeB,
            [Values] ConceptionType conceptionTypeB
        ) => AssertEquality(
            sceneTypeA == sceneTypeB,
            CreateValid(sceneTypeA, conceptionTypeA),
            CreateValid(sceneTypeB, conceptionTypeB)
        );

        [Test]
        public void Valid_Invalid(
            [Values] SceneType sceneTypeA,
            [Values] ConceptionType conceptionTypeA,
            [Values] ConceptionType conceptionTypeB,
            [Values] InvalidReason invalidReasonB
        ) => AssertEquality(
            false,
            CreateValid(sceneTypeA, conceptionTypeA),
            CreateInvalid(conceptionTypeB, invalidReasonB)
        );

        [Test]
        public void Invalid_Invalid(
            [Values] ConceptionType conceptionTypeA,
            [Values] InvalidReason invalidReasonA,
            [Values] ConceptionType conceptionTypeB,
            [Values] InvalidReason invalidReasonB
        ) => AssertEquality(
            invalidReasonA == invalidReasonB,
            CreateInvalid(conceptionTypeA, invalidReasonA),
            CreateInvalid(conceptionTypeB, invalidReasonB)
        );

        [Test]
        public void Valid_DifferentGuidCasing_AreEqual(
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

            AssertEquality(true, srLower, srUpper);
        }

        private SceneReference CreateValid(SceneType sceneType, ConceptionType conceptionType)
        {
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

            return conceptionType switch
            {
                ConceptionType.CreatedInCode => new SceneReference(guid),
                ConceptionType.DeserializedViaNewtonsoftJson => TestUtils.DeserializeViaNewtonsoftJson(TestUtils.GetExpectedOutputOfNewtonsoftJson(guid)),
                ConceptionType.DeserializedViaSystemXml => TestUtils.DeserializeViaSystemXml(TestUtils.GetExpectedOutputOfSystemXml(guid)),
                ConceptionType.DeserializedViaBinaryFormatter => TestUtils.DeserializeFromBase64ViaBinaryFormatter(TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(guid)),
                ConceptionType.UnitySerialized => TestSubjectContainer.GetSceneReference(sceneType),
                _ => throw new ArgumentOutOfRangeException(nameof(conceptionType), conceptionType, null),
            };
        }

        private SceneReference CreateInvalid(ConceptionType conceptionType, InvalidReason invalidReason)
        {
            if (conceptionType == ConceptionType.UnitySerialized && invalidReason == InvalidReason.InvalidGuid)
            {
                Assert.Ignore("It would take a very dedicated person doing very weird things to get a Unity serialized SceneReference with an invalid guid.");
            }

            if (conceptionType == ConceptionType.CreatedInCode && (invalidReason == InvalidReason.InvalidGuid || invalidReason == InvalidReason.NotExisting || invalidReason == InvalidReason.NotSceneAsset))
            {
                Assert.Ignore("It is not possible to create a SceneReference in code with a guid that is not a valid scene asset because the constructor validates the guid and throws if it is not for a valid scene asset.");
            }

            if (invalidReason == InvalidReason.Null)
            {
                return null;
            }

            if (invalidReason == InvalidReason.Empty)
            {
                return new SceneReference();
            }

            if (conceptionType == ConceptionType.UnitySerialized)
            {
                return invalidReason switch
                {
                    InvalidReason.Null => throw new Exception("This branch is already handled above with an if statement."),
                    InvalidReason.Empty => throw new Exception("This branch is already handled above with an if statement."),
                    InvalidReason.InvalidGuid => throw new Exception("This branch is already handled above with an if statement."),
                    InvalidReason.NotExisting => TestSubjectContainer.NotExisting.Field,
                    InvalidReason.NotSceneAsset => TestSubjectContainer.NotSceneAsset.Field,
                    _ => throw new ArgumentOutOfRangeException(nameof(invalidReason), invalidReason, null)
                };
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

            return conceptionType switch
            {
                ConceptionType.CreatedInCode => new SceneReference(guid),
                ConceptionType.DeserializedViaNewtonsoftJson => TestUtils.DeserializeViaNewtonsoftJson(TestUtils.GetExpectedOutputOfNewtonsoftJson(guid)),
                ConceptionType.DeserializedViaSystemXml => TestUtils.DeserializeViaSystemXml(TestUtils.GetExpectedOutputOfSystemXml(guid)),
                ConceptionType.DeserializedViaBinaryFormatter => TestUtils.DeserializeFromBase64ViaBinaryFormatter(TestUtils.GetAsBase64ExpectedOutputOfBinaryFormatter(guid)),
                ConceptionType.UnitySerialized => throw new Exception("This branch is already handled above with an if statement."),
                _ => throw new ArgumentOutOfRangeException(nameof(conceptionType), conceptionType, null),
            };
        }

        private static void AssertEquality(bool shouldBeEqual, SceneReference a, SceneReference b)
        {
            Assert.AreEqual(shouldBeEqual, a == b);
            Assert.AreEqual(shouldBeEqual, b == a);

            Assert.AreEqual(!shouldBeEqual, a != b);
            Assert.AreEqual(!shouldBeEqual, b != a);

            Assert.AreEqual(shouldBeEqual, Equals(a, b));
            Assert.AreEqual(shouldBeEqual, Equals(b, a));

            if (a is {})
            {
                Assert.AreEqual(shouldBeEqual, a.Equals(b));
            }

            if (b is {})
            {
                Assert.AreEqual(shouldBeEqual, b.Equals(a));
            }
        }

        private static void AssertEquality(bool shouldBeEqual, SceneReference aAndB)
            => AssertEquality(shouldBeEqual, aAndB, aAndB);
    }
}
