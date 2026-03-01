using System;
using System.Collections;
using Eflatun.SceneReference.Tests.Runtime.EqualityAndHashCode;
using Eflatun.SceneReference.Tests.Runtime.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Eflatun.SceneReference.Tests.Runtime.Subjects
{
    public class TestSubjectContainer : MonoBehaviour
    {
        [SerializeField] private TestSubject enabledScene;
        public static TestSubject EnabledScene { get; private set; }

        [SerializeField] private TestSubject disabledScene;
        public static TestSubject DisabledScene { get; private set; }

        [SerializeField] private TestSubject notInBuildScene;
        public static TestSubject NotInBuildScene { get; private set; }

        [SerializeField] private TestSubject empty;
        public static TestSubject Empty { get; private set; }

        [SerializeField] private TestSubject deletedScene;
        public static TestSubject DeletedScene { get; private set; }

        [SerializeField] private TestSubject notExisting;
        public static TestSubject NotExisting { get; private set; }

        [SerializeField] private TestSubject notSceneAsset;
        public static TestSubject NotSceneAsset { get; private set; }

        [SerializeField] private TestSubject addressable1Scene;
        public static TestSubject Addressable1Scene { get; private set; }

        [SerializeField] private TestSubject addressable2Scene;
        public static TestSubject Addressable2Scene { get; private set; }

        [SerializeField] private TestSubject addressableDuplicateAddressAScene;
        public static TestSubject AddressableDuplicateAddressAScene { get; private set; }

        [SerializeField] private TestSubject addressableDuplicateAddressBScene;
        public static TestSubject AddressableDuplicateAddressBScene { get; private set; }

        private static bool _didCache;

        public static IEnumerator CacheIfNotAlready()
        {
            if (_didCache)
            {
                yield break;
            }

            _didCache = true;

            yield return SceneManager.LoadSceneAsync(TestUtils.TestSubjectContainerScenePath, LoadSceneMode.Additive);
            var scene = SceneManager.GetSceneByPath(TestUtils.TestSubjectContainerScenePath);

            if (!scene.IsValid())
            {
                throw new Exception($"Couldn't load scene {TestUtils.TestSubjectContainerScenePath}");
            }

            var containers = FindObjectsOfType<TestSubjectContainer>();

            if (containers.Length < 1)
            {
                throw new Exception($"Couldn't find any {nameof(TestSubjectContainer)} in scene {TestUtils.TestSubjectContainerScenePath}");
            }

            if (containers.Length > 1)
            {
                throw new Exception($"Found more than one {nameof(TestSubjectContainer)} in scene {TestUtils.TestSubjectContainerScenePath}");
            }

            var container = containers[0];

            EnabledScene = container.enabledScene;
            DisabledScene = container.disabledScene;
            NotInBuildScene = container.notInBuildScene;
            Empty = container.empty;
            DeletedScene = container.deletedScene;
            NotExisting = container.notExisting;
            NotSceneAsset = container.notSceneAsset;
            Addressable1Scene = container.addressable1Scene;
            Addressable2Scene = container.addressable2Scene;
            AddressableDuplicateAddressAScene = container.addressableDuplicateAddressAScene;
            AddressableDuplicateAddressBScene = container.addressableDuplicateAddressBScene;

            yield return SceneManager.UnloadSceneAsync(scene);
        }

        public static SceneReference GetSceneReference(SceneType sceneType) => sceneType switch
        {
            SceneType.NotInBuild => NotInBuildScene.Field,
            SceneType.Disabled => DisabledScene.Field,
            SceneType.Enabled => EnabledScene.Field,
            SceneType.Addressable1 => Addressable1Scene.Field,
            SceneType.Addressable2 => Addressable2Scene.Field,
            SceneType.AddressableDuplicateAddressA => AddressableDuplicateAddressAScene.Field,
            SceneType.AddressableDuplicateAddressB => AddressableDuplicateAddressBScene.Field,
            _ => throw new ArgumentOutOfRangeException(nameof(sceneType), sceneType, null),
        };
    }
}
