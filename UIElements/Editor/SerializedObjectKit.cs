using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UdonLab.EditorUI
{
    public class SerializedObjectKit
    {
        public static T GetSerializedObject<T>(UnityEngine.Object unityObject, string propertyName) where T : UnityEngine.Object
        {
            var serializedObject = new SerializedObject(unityObject);
            var prop = serializedObject.FindProperty(propertyName);
            if (prop != null)
            {
                return prop.objectReferenceValue as T;
            }
            return null;
        }
        public static T GetSerializedObject<T>(SerializedObject serializedObject, string propertyName) where T : UnityEngine.Object
        {
            var prop = serializedObject.FindProperty(propertyName);
            if (prop != null)
            {
                return prop.objectReferenceValue as T;
            }
            return null;
        }
        public static object GetSerializedObject(UnityEngine.Object unityObject, string propertyName)
        {
            var serializedObject = new SerializedObject(unityObject);
            var prop = serializedObject.FindProperty(propertyName);
            if (prop != null)
            {
                switch (prop.propertyType)
                {
                    case SerializedPropertyType.ObjectReference:
                        return prop.objectReferenceValue;
                    case SerializedPropertyType.Integer:
                        return prop.intValue;
                    case SerializedPropertyType.Boolean:
                        return prop.boolValue;
                    case SerializedPropertyType.Float:
                        return prop.floatValue;
                    case SerializedPropertyType.String:
                        return prop.stringValue;
                    case SerializedPropertyType.Color:
                        return prop.colorValue;
                    case SerializedPropertyType.Enum:
                        return prop.enumValueIndex;
                    case SerializedPropertyType.Vector2:
                        return prop.vector2Value;
                    case SerializedPropertyType.Vector3:
                        return prop.vector3Value;
                    case SerializedPropertyType.Vector4:
                        return prop.vector4Value;
                    case SerializedPropertyType.Rect:
                        return prop.rectValue;
                    case SerializedPropertyType.ArraySize:
                        return prop.arraySize;
                    case SerializedPropertyType.Character:
                        return prop.intValue;
                    case SerializedPropertyType.AnimationCurve:
                        return prop.animationCurveValue;
                    case SerializedPropertyType.Bounds:
                        return prop.boundsValue;
                    case SerializedPropertyType.Quaternion:
                        return prop.quaternionValue;
                    case SerializedPropertyType.ExposedReference:
                        return prop.exposedReferenceValue;
                    case SerializedPropertyType.FixedBufferSize:
                        return prop.fixedBufferSize;
                    case SerializedPropertyType.Vector2Int:
                        return prop.vector2IntValue;
                    case SerializedPropertyType.Vector3Int:
                        return prop.vector3IntValue;
                    case SerializedPropertyType.RectInt:
                        return prop.rectIntValue;
                    case SerializedPropertyType.BoundsInt:
                        return prop.boundsIntValue;
                    default:
                        return null;
                }
                // return prop.objectReferenceValue as object;
            }
            return null;
        }
        public static void SetSerializedObject(UnityEngine.Object unityObject, string propertyName, UnityEngine.Object value)
        {
            var serializedObject = new SerializedObject(unityObject);
            var prop = serializedObject.FindProperty(propertyName);
            if (prop != null)
            {
                prop.objectReferenceValue = value;
                serializedObject.ApplyModifiedProperties();
            }
        }
        public static void SetSerializedObject(SerializedObject serializedObject, string propertyName, UnityEngine.Object value)
        {
            var prop = serializedObject.FindProperty(propertyName);
            if (prop != null)
            {
                prop.objectReferenceValue = value;
                serializedObject.ApplyModifiedProperties();
            }
        }
        public static List<T> GetSerializedObjectList<T>(UnityEngine.Object unityObject, string propertyName) where T : UnityEngine.Object
        {
            var serializedObject = new SerializedObject(unityObject);
            var prop = serializedObject.FindProperty(propertyName);
            var list = new List<T>();
            if (prop != null)
            {
                for (int i = 0; i < prop.arraySize; i++)
                {
                    var element = prop.GetArrayElementAtIndex(i);
                    if (element != null)
                    {
                        var value = element.objectReferenceValue as T;
                        if (value != null)
                        {
                            list.Add(value);
                        }
                    }
                }
            }
            return list;
        }
        public static List<T> GetSerializedObjectList<T>(SerializedObject serializedObject, string propertyName) where T : UnityEngine.Object
        {
            var prop = serializedObject.FindProperty(propertyName);
            var list = new List<T>();
            if (prop != null)
            {
                for (int i = 0; i < prop.arraySize; i++)
                {
                    var element = prop.GetArrayElementAtIndex(i);
                    if (element != null)
                    {
                        var value = element.objectReferenceValue as T;
                        if (value != null)
                        {
                            list.Add(value);
                        }
                    }
                }
            }
            return list;
        }
        public static void SetSerializedObjectList<T>(UnityEngine.Object unityObject, string propertyName, List<T> values)
        {
            var serializedObject = new SerializedObject(unityObject);
            var prop = serializedObject.FindProperty(propertyName);
            if (prop != null)
            {
                prop.arraySize = values.Count;
                for (int i = 0; i < values.Count; i++)
                {
                    var element = prop.GetArrayElementAtIndex(i);
                    if (element != null)
                    {
                        element.objectReferenceValue = values[i] as UnityEngine.Object;
                    }
                }
                serializedObject.ApplyModifiedProperties();
            }
        }
        public static void SetSerializedObjectList<T>(SerializedObject serializedObject, string propertyName, List<T> values)
        {
            var prop = serializedObject.FindProperty(propertyName);
            if (prop != null)
            {
                prop.arraySize = values.Count;
                for (int i = 0; i < values.Count; i++)
                {
                    var element = prop.GetArrayElementAtIndex(i);
                    if (element != null)
                    {
                        element.objectReferenceValue = values[i] as UnityEngine.Object;
                    }
                }
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
