using System;
using Eflatun.SceneReference.Utility;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Eflatun.SceneReference
{
    /// <summary>
    /// Provides a strongly-typed reference to a scene. Can be used in both editor and runtime.
    /// </summary>
    [PublicAPI]
    [Serializable]
    public class SceneReference
    {
        // GUID hex of an invalid asset contains all zeros. A GUID hex has 32 chars.
        private const string AllZeroGuidHex = "00000000000000000000000000000000";

        [SerializeField] internal UnityEngine.Object sceneAsset = null;
        [SerializeField] internal string sceneAssetGuidHex = AllZeroGuidHex;

        /// <summary>
        /// GUID of the scene asset in hex format. 
        /// </summary>
        public string AssetGuidHex => sceneAssetGuidHex;

        /// <summary>
        /// Path to the scene asset.
        /// </summary>
        /// <exception cref="EmptySceneReferenceException">Throws if <see cref="HasValue"/> is <c>false</c>.</exception>
        /// <exception cref="InvalidSceneReferenceException">Throws if <see cref="IsInSceneGuidToPathMap"/> is <c>false</c>.</exception>
        public string Path
        {
            get
            {
                if (!HasValue)
                {
                    throw new EmptySceneReferenceException();
                }

                if (!SceneGuidToPathMapProvider.SceneGuidToPathMap.TryGetValue(sceneAssetGuidHex, out var scenePath))
                {
                    throw new InvalidSceneReferenceException();
                }

                return scenePath;
            }
        }

        /// <summary>
        /// Build index of the scene.
        /// </summary>
        /// <exception cref="EmptySceneReferenceException">Throws if <see cref="HasValue"/> is <c>false</c>.</exception>
        /// <exception cref="InvalidSceneReferenceException">Throws if <see cref="IsInSceneGuidToPathMap"/> is <c>false</c>.</exception>
        /// <remarks>
        /// This property will return <c>-1</c> if <see cref="IsInBuildAndEnabled"/> is <c>false</c>.
        /// </remarks>
        public int BuildIndex => SceneUtility.GetBuildIndexByScenePath(Path);

        /// <summary>
        /// Name of the scene asset.
        /// </summary>
        /// <exception cref="EmptySceneReferenceException">Throws if <see cref="HasValue"/> is <c>false</c>.</exception>
        /// <exception cref="InvalidSceneReferenceException">Throws if <see cref="IsInSceneGuidToPathMap"/> is <c>false</c>.</exception>
        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);

        /// <summary>
        /// The <see cref="Scene"/> struct for this scene. Only valid if the scene is currently loaded.
        /// </summary>
        /// <exception cref="EmptySceneReferenceException">Throws if <see cref="HasValue"/> is <c>false</c>.</exception>
        /// <exception cref="InvalidSceneReferenceException">Throws if <see cref="IsInSceneGuidToPathMap"/> is <c>false</c>.</exception>
        /// <remarks>
        /// You can check <see cref="Scene.IsValid"/> to see if the value of this property is valid.<p/>
        /// If <see cref="IsInBuildAndEnabled"/> is <c>false</c>, the scene can never be loaded, and therefore this property can never have a valid value.
        /// </remarks>
        public Scene LoadedScene => SceneManager.GetSceneByPath(Path);

        /// <summary>
        /// Is this <see cref="SceneReference"/> assigned something?
        /// </summary>
        /// <remarks>
        /// Only check this property if you need partial validations, as this property alone does not communicate whether this <see cref="SceneReference"/> is absolutely safe to use.<p/>
        /// If you only need to check if it is safe to use this <see cref="SceneReference"/>, then only check <see cref="IsSafeToUse"/> instead. Checking only <see cref="IsSafeToUse"/> is sufficient for the majority of the use cases.
        /// </remarks>
        /// <seealso cref="IsInSceneGuidToPathMap"/>
        /// <seealso cref="IsInBuildAndEnabled"/>
        /// <seealso cref="IsSafeToUse"/>
        public bool HasValue
        {
            get
            {
                if (!AssetGuidHex.IsValidGuidHex())
                {
                    // internal exceptions should not be documented as part of the public API
                    throw SceneReferenceInternalException.InvalidAssetGuidHex("54783205", AssetGuidHex);
                }

                return AssetGuidHex != AllZeroGuidHex;
            }
        }

        /// <summary>
        /// Does the scene GUID to path map contain the scene?
        /// </summary>
        /// <exception cref="EmptySceneReferenceException">Throws if <see cref="HasValue"/> is <c>false</c>.</exception>
        /// <remarks>
        /// Only check this property if you need partial validations, as this property alone does not communicate whether this <see cref="SceneReference"/> is absolutely safe to use.<p/>
        /// If you only need to check if it is safe to use this <see cref="SceneReference"/>, then only check <see cref="IsSafeToUse"/> instead. Checking only <see cref="IsSafeToUse"/> is sufficient for the majority of the use cases.
        /// </remarks>
        /// <seealso cref="HasValue"/>
        /// <seealso cref="IsInBuildAndEnabled"/>
        /// <seealso cref="IsSafeToUse"/>
        public bool IsInSceneGuidToPathMap
        {
            get
            {
                if (!HasValue)
                {
                    throw new EmptySceneReferenceException();
                }

                return SceneGuidToPathMapProvider.SceneGuidToPathMap.ContainsKey(AssetGuidHex);
            }
        }

        /// <summary>
        /// Is the scene added and enabled in Build Settings?
        /// </summary>
        /// <exception cref="EmptySceneReferenceException">Throws if <see cref="HasValue"/> is <c>false</c>.</exception>
        /// <exception cref="InvalidSceneReferenceException">Throws if <see cref="IsInSceneGuidToPathMap"/> is <c>false</c>.</exception>
        /// <remarks>
        /// Only check this property if you need partial validations, as this property alone does not communicate whether this <see cref="SceneReference"/> is absolutely safe to use.<p/>
        /// If you only need to check if it is safe to use this <see cref="SceneReference"/>, then only check <see cref="IsSafeToUse"/> instead. Checking only <see cref="IsSafeToUse"/> is sufficient for the majority of the use cases.
        /// </remarks>
        /// <seealso cref="HasValue"/>
        /// <seealso cref="IsInSceneGuidToPathMap"/>
        /// <seealso cref="IsSafeToUse"/>
        public bool IsInBuildAndEnabled => BuildIndex != -1;

        /// <summary>
        /// Is this <see cref="SceneReference"/> safe to use?
        /// </summary>
        /// <remarks>
        /// Checking this property alone is sufficient for the majority of the validation use cases, as this property absolutely communicates whether this <see cref="SceneReference"/> is safe to use.<p/>
        /// Checking this property is equivalent to checking all partial validation properties (namely: <see cref="HasValue"/>, <see cref="IsInSceneGuidToPathMap"/>, and <see cref="IsInBuildAndEnabled"/>) in the correct order, but it provides a slightly better performance. If you need those validations partially, you can check them instead of this property. Keep in mind that the use cases that require partial validation are rare and few.
        /// </remarks>
        /// <seealso cref="HasValue"/>
        /// <seealso cref="IsInSceneGuidToPathMap"/>
        /// <seealso cref="IsInBuildAndEnabled"/>
        public bool IsSafeToUse =>
            HasValue
            && SceneGuidToPathMapProvider.SceneGuidToPathMap.TryGetValue(AssetGuidHex, out var path)
            && SceneUtility.GetBuildIndexByScenePath(path) != -1;
    }
}
