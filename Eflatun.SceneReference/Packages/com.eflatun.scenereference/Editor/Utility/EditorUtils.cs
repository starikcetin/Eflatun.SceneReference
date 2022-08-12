using System;
using System.IO;
using System.Reflection;
using UnityEditor;

namespace Eflatun.SceneReference.Editor.Utility
{
    /// <summary>
    /// Editor utilities used by Eflatun.SceneReference.
    /// </summary>
    internal static class EditorUtils
    {
        private const BindingFlags AllBindingFlags = (BindingFlags)(-1);

        /// <summary>
        /// Returns true if the given <paramref name="path"/> ends with the file extension ".unity".
        /// </summary>
        public static bool IsSceneAssetPath(this string path) => Path.GetExtension(path) == ".unity";

        public static TAttr[] GetAttributes<TAttr>(this SerializedProperty prop, bool inherit)
            where TAttr : Attribute
        {
            if (prop == null)
            {
                throw new ArgumentException("Could not find type from given prop", nameof(prop));
            }

            var propType = prop.serializedObject.targetObject.GetType();

            foreach (var pathSegment in prop.propertyPath.Split('.'))
            {
                var fieldInfo = propType.GetField(pathSegment, AllBindingFlags);
                if (fieldInfo != null)
                {
                    return (TAttr[])fieldInfo.GetCustomAttributes<TAttr>(inherit);
                }

                var propertyInfo = propType.GetProperty(pathSegment, AllBindingFlags);
                if (propertyInfo != null)
                {
                    return (TAttr[])propertyInfo.GetCustomAttributes<TAttr>(inherit);
                }
            }

            throw new ArgumentException("Could not find type from given prop", nameof(prop));
        }
    }
}
