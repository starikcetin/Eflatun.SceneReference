using System.Collections;
using Eflatun.SceneReference.Tests.Runtime.Subjects;
using Eflatun.SceneReference.Tests.Runtime.Utils;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Eflatun.SceneReference.Tests.Runtime
{
    public class SceneReferenceUnitySerializationTests
    {
        [UnitySetUp]
        public IEnumerator Setup() => TestSubjectContainer.CacheIfNotAlready();

        [Test]
        public void ProvidesExpectedState_EnabledScene()
        {
            TestUtils.AssertEnabledSceneState(TestSubjectContainer.EnabledScene.Field);
            TestUtils.AssertEnabledSceneState(TestSubjectContainer.EnabledScene.Prop);

            foreach (var sr in TestSubjectContainer.EnabledScene.FieldArray)
            {
                TestUtils.AssertEnabledSceneState(sr);
            }

            foreach (var sr in TestSubjectContainer.EnabledScene.PropArray)
            {
                TestUtils.AssertEnabledSceneState(sr);
            }

            foreach (var sr in TestSubjectContainer.EnabledScene.FieldList)
            {
                TestUtils.AssertEnabledSceneState(sr);
            }

            foreach (var sr in TestSubjectContainer.EnabledScene.PropList)
            {
                TestUtils.AssertEnabledSceneState(sr);
            }
        }

        [Test]
        public void ProvidesExpectedState_DisabledScene()
        {
            TestUtils.AssertDisabledSceneState(TestSubjectContainer.DisabledScene.Field);
            TestUtils.AssertDisabledSceneState(TestSubjectContainer.DisabledScene.Prop);

            foreach (var sr in TestSubjectContainer.DisabledScene.FieldArray)
            {
                TestUtils.AssertDisabledSceneState(sr);
            }

            foreach (var sr in TestSubjectContainer.DisabledScene.PropArray)
            {
                TestUtils.AssertDisabledSceneState(sr);
            }

            foreach (var sr in TestSubjectContainer.DisabledScene.FieldList)
            {
                TestUtils.AssertDisabledSceneState(sr);
            }

            foreach (var sr in TestSubjectContainer.DisabledScene.PropList)
            {
                TestUtils.AssertDisabledSceneState(sr);
            }
        }

        [Test]
        public void ProvidesExpectedState_NotInBuildScene()
        {
            TestUtils.AssertNotInBuildSceneState(TestSubjectContainer.NotInBuildScene.Field);
            TestUtils.AssertNotInBuildSceneState(TestSubjectContainer.NotInBuildScene.Prop);

            foreach (var sr in TestSubjectContainer.NotInBuildScene.FieldArray)
            {
                TestUtils.AssertNotInBuildSceneState(sr);
            }

            foreach (var sr in TestSubjectContainer.NotInBuildScene.PropArray)
            {
                TestUtils.AssertNotInBuildSceneState(sr);
            }

            foreach (var sr in TestSubjectContainer.NotInBuildScene.FieldList)
            {
                TestUtils.AssertNotInBuildSceneState(sr);
            }

            foreach (var sr in TestSubjectContainer.NotInBuildScene.PropList)
            {
                TestUtils.AssertNotInBuildSceneState(sr);
            }
        }

        [Test]
        public void ProvidesExpectedState_Empty()
        {
            TestUtils.AssertEmptyState(TestSubjectContainer.Empty.Field);
            TestUtils.AssertEmptyState(TestSubjectContainer.Empty.Prop);

            foreach (var sr in TestSubjectContainer.Empty.FieldArray)
            {
                TestUtils.AssertEmptyState(sr);
            }

            foreach (var sr in TestSubjectContainer.Empty.PropArray)
            {
                TestUtils.AssertEmptyState(sr);
            }

            foreach (var sr in TestSubjectContainer.Empty.FieldList)
            {
                TestUtils.AssertEmptyState(sr);
            }

            foreach (var sr in TestSubjectContainer.Empty.PropList)
            {
                TestUtils.AssertEmptyState(sr);
            }
        }

        [Test]
        public void ProvidesExpectedState_DeletedScene()
        {
            TestUtils.AssertDeletedSceneState(TestSubjectContainer.DeletedScene.Field);
            TestUtils.AssertDeletedSceneState(TestSubjectContainer.DeletedScene.Prop);

            foreach (var sr in TestSubjectContainer.DeletedScene.FieldArray)
            {
                TestUtils.AssertDeletedSceneState(sr);
            }

            foreach (var sr in TestSubjectContainer.DeletedScene.PropArray)
            {
                TestUtils.AssertDeletedSceneState(sr);
            }

            foreach (var sr in TestSubjectContainer.DeletedScene.FieldList)
            {
                TestUtils.AssertDeletedSceneState(sr);
            }

            foreach (var sr in TestSubjectContainer.DeletedScene.PropList)
            {
                TestUtils.AssertDeletedSceneState(sr);
            }
        }

        [Test]
        public void ProvidesExpectedState_NotExisting()
        {
            TestUtils.AssertNotExistingState(TestSubjectContainer.NotExisting.Field);
            TestUtils.AssertNotExistingState(TestSubjectContainer.NotExisting.Prop);

            foreach (var sr in TestSubjectContainer.NotExisting.FieldArray)
            {
                TestUtils.AssertNotExistingState(sr);
            }

            foreach (var sr in TestSubjectContainer.NotExisting.PropArray)
            {
                TestUtils.AssertNotExistingState(sr);
            }

            foreach (var sr in TestSubjectContainer.NotExisting.FieldList)
            {
                TestUtils.AssertNotExistingState(sr);
            }

            foreach (var sr in TestSubjectContainer.NotExisting.PropList)
            {
                TestUtils.AssertNotExistingState(sr);
            }
        }

        [Test]
        public void ProvidesExpectedState_NotSceneAsset()
        {
            TestUtils.AssertNotSceneAssetState(TestSubjectContainer.NotSceneAsset.Field);
            TestUtils.AssertNotSceneAssetState(TestSubjectContainer.NotSceneAsset.Prop);

            foreach (var sr in TestSubjectContainer.NotSceneAsset.FieldArray)
            {
                TestUtils.AssertNotSceneAssetState(sr);
            }

            foreach (var sr in TestSubjectContainer.NotSceneAsset.PropArray)
            {
                TestUtils.AssertNotSceneAssetState(sr);
            }

            foreach (var sr in TestSubjectContainer.NotSceneAsset.FieldList)
            {
                TestUtils.AssertNotSceneAssetState(sr);
            }

            foreach (var sr in TestSubjectContainer.NotSceneAsset.PropList)
            {
                TestUtils.AssertNotSceneAssetState(sr);
            }
        }

        [Test]
        public void ProvidesExpectedState_Addressable1Scene()
        {
            TestUtils.AssertAddressable1SceneState(TestSubjectContainer.Addressable1Scene.Field);
            TestUtils.AssertAddressable1SceneState(TestSubjectContainer.Addressable1Scene.Prop);

            foreach (var sr in TestSubjectContainer.Addressable1Scene.FieldArray)
            {
                TestUtils.AssertAddressable1SceneState(sr);
            }

            foreach (var sr in TestSubjectContainer.Addressable1Scene.PropArray)
            {
                TestUtils.AssertAddressable1SceneState(sr);
            }

            foreach (var sr in TestSubjectContainer.Addressable1Scene.FieldList)
            {
                TestUtils.AssertAddressable1SceneState(sr);
            }

            foreach (var sr in TestSubjectContainer.Addressable1Scene.PropList)
            {
                TestUtils.AssertAddressable1SceneState(sr);
            }
        }

        [Test]
        public void ProvidesExpectedState_Addressable2Scene()
        {
            TestUtils.AssertAddressable2SceneState(TestSubjectContainer.Addressable2Scene.Field);
            TestUtils.AssertAddressable2SceneState(TestSubjectContainer.Addressable2Scene.Prop);

            foreach (var sr in TestSubjectContainer.Addressable2Scene.FieldArray)
            {
                TestUtils.AssertAddressable2SceneState(sr);
            }

            foreach (var sr in TestSubjectContainer.Addressable2Scene.PropArray)
            {
                TestUtils.AssertAddressable2SceneState(sr);
            }

            foreach (var sr in TestSubjectContainer.Addressable2Scene.FieldList)
            {
                TestUtils.AssertAddressable2SceneState(sr);
            }

            foreach (var sr in TestSubjectContainer.Addressable2Scene.PropList)
            {
                TestUtils.AssertAddressable2SceneState(sr);
            }
        }

        [Test]
        public void ProvidesExpectedState_AddressableDuplicateAddressAScene()
        {
            TestUtils.AssertAddressableDuplicateAddressASceneState(TestSubjectContainer.AddressableDuplicateAddressAScene.Field);
            TestUtils.AssertAddressableDuplicateAddressASceneState(TestSubjectContainer.AddressableDuplicateAddressAScene.Prop);

            foreach (var sr in TestSubjectContainer.AddressableDuplicateAddressAScene.FieldArray)
            {
                TestUtils.AssertAddressableDuplicateAddressASceneState(sr);
            }

            foreach (var sr in TestSubjectContainer.AddressableDuplicateAddressAScene.PropArray)
            {
                TestUtils.AssertAddressableDuplicateAddressASceneState(sr);
            }

            foreach (var sr in TestSubjectContainer.AddressableDuplicateAddressAScene.FieldList)
            {
                TestUtils.AssertAddressableDuplicateAddressASceneState(sr);
            }

            foreach (var sr in TestSubjectContainer.AddressableDuplicateAddressAScene.PropList)
            {
                TestUtils.AssertAddressableDuplicateAddressASceneState(sr);
            }
        }

        [Test]
        public void ProvidesExpectedState_AddressableDuplicateAddressBScene()
        {
            TestUtils.AssertAddressableDuplicateAddressBSceneState(TestSubjectContainer.AddressableDuplicateAddressBScene.Field);
            TestUtils.AssertAddressableDuplicateAddressBSceneState(TestSubjectContainer.AddressableDuplicateAddressBScene.Prop);

            foreach (var sr in TestSubjectContainer.AddressableDuplicateAddressBScene.FieldArray)
            {
                TestUtils.AssertAddressableDuplicateAddressBSceneState(sr);
            }

            foreach (var sr in TestSubjectContainer.AddressableDuplicateAddressBScene.PropArray)
            {
                TestUtils.AssertAddressableDuplicateAddressBSceneState(sr);
            }

            foreach (var sr in TestSubjectContainer.AddressableDuplicateAddressBScene.FieldList)
            {
                TestUtils.AssertAddressableDuplicateAddressBSceneState(sr);
            }

            foreach (var sr in TestSubjectContainer.AddressableDuplicateAddressBScene.PropList)
            {
                TestUtils.AssertAddressableDuplicateAddressBSceneState(sr);
            }
        }
    }
}
