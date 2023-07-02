using System;
using System.IO;
using Eflatun.SceneReference.Exceptions;

namespace Eflatun.SceneReference.Editor.Utility
{
    /// <summary>
    /// Editor utilities used by Eflatun.SceneReference.
    /// </summary>
    internal static class EditorUtils
    {
        /// <summary>
        /// Returns whether the addressables package is installed in the project.
        /// </summary>
        public static bool IsAddressablesPackagePresent =>
#if ESR_ADDRESSABLES
            true
#else // ESR_ADDRESSABLES
            false
#endif // ESR_ADDRESSABLES
        ;

        private static Type _addressablesGroupsWindowType;
        /// <summary>The <see cref="Type"/> of the Addressables Groups editor window.</summary>
        public static Type AddressablesGroupsWindowType
        {
            get
            {
                if (_addressablesGroupsWindowType == null)
                {
                    var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                    foreach (var assembly in assemblies)
                    {
                        var type = assembly.GetType("UnityEditor.AddressableAssets.GUI.AddressableAssetsWindow");
                        if (type != null)
                        {
                            _addressablesGroupsWindowType = type;
                        }
                    }
                }

                if (_addressablesGroupsWindowType == null)
                {
                    throw SceneReferenceInternalException.EditorCode("48302749", "Could not find addressables groups window type.");
                }

                return _addressablesGroupsWindowType;
            }
        }

        /// <summary>
        /// Returns true if the given <paramref name="path"/> ends with the file extension ".unity".
        /// </summary>
        public static bool IsScenePath(this string path) => Path.GetExtension(path) == ".unity";
    }
}
