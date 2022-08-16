using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Returns attributes of type <typeparamref name="TAttribute"/> on <paramref name="serializedProperty"/>.
        /// Returns the attributes on the innermost field only.
        /// </summary>
        public static TAttribute[] GetAttributes<TAttribute>(this SerializedProperty serializedProperty, bool inherit)
            where TAttribute : Attribute
        {
            var pathSegments = serializedProperty.propertyPath.Split('.');
            var outermostType = serializedProperty.serializedObject.targetObject.GetType();

            var fieldInfos = new List<FieldInfo>();
            var itType = outermostType;
            foreach (var pathSegment in pathSegments)
            {
                // We don't need to check for properties, Unity doesn't serialize them.
                var fieldInfo = itType.GetField(pathSegment, AllBindingFlags);
                if (fieldInfo == null)
                {
                    break;
                }

                fieldInfos.Add(fieldInfo);
                itType = fieldInfo.FieldType;
            }

            // Reverse for, look inside-out. This ensures we get the innermost attribute.
            for (var i = fieldInfos.Count - 1; i >= 0; i--)
            {
                var fieldInfo = fieldInfos[i];
                if (fieldInfo.GetCustomAttributes<TAttribute>(inherit) is TAttribute[] attributes)
                {
                    return attributes;
                }
            }

            return Array.Empty<TAttribute>();
        }
    }
}
