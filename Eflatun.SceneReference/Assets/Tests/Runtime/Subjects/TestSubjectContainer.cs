using System;
using System.Collections;
using System.Linq;
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

        private enum CacheState
        {
            NotStarted,
            InProgress,
            Finished,
        }

        private static CacheState _cacheState = CacheState.NotStarted;

        public static IEnumerator CacheIfNotAlready()
        {
            var maxWaitDuration = TimeSpan.FromMinutes(2);
            var waitEndUtcTime = DateTime.UtcNow + maxWaitDuration;

            while (_cacheState == CacheState.InProgress)
            {
                if (DateTime.UtcNow > waitEndUtcTime)
                {
                    throw new TimeoutException($"Caching the {nameof(TestSubjectContainer)} took longer than {maxWaitDuration}. This likely means that the caching process got stuck.");
                }

                yield return null;
            }

            if (_cacheState == CacheState.Finished)
            {
                yield break;
            }

            _cacheState = CacheState.InProgress;

            yield return SceneManager.LoadSceneAsync(TestUtils.TestSubjectContainerScenePath, LoadSceneMode.Additive);

            var container = SceneManager.GetSceneByPath(TestUtils.TestSubjectContainerScenePath)
                .GetRootGameObjects()
                .SelectMany(go => go.GetComponentsInChildren<TestSubjectContainer>())
                .Single();

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

            yield return SceneManager.UnloadSceneAsync(TestUtils.TestSubjectContainerScenePath);

            _cacheState = CacheState.Finished;
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
