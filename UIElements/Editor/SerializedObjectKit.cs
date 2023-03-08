using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UdonLab.EditorUI
{
    public class SerializedObjectKit
    {
        public static T GetSerializedObject<T>(UnityEngine.Object unityObject, string propertyName) where T : class
        {
            var serializedObject = new SerializedObject(unityObject);
            return GetSerializedObject<T>(serializedObject, propertyName);
        }
        public static T GetSerializedObject<T>(SerializedObject serializedObject, string propertyName) where T : class
        {
            var prop = serializedObject.FindProperty(propertyName);
            if (prop != null)
            {
                if (prop.propertyType == SerializedPropertyType.ObjectReference)
                {
                    return prop.objectReferenceValue as T;
                }
                switch (prop.propertyType)
                {
                    case SerializedPropertyType.Integer:
                        return prop.intValue as T;
                    case SerializedPropertyType.Boolean:
                        return prop.boolValue as T;
                    case SerializedPropertyType.Float:
                        return prop.floatValue as T;
                    case SerializedPropertyType.String:
                        return prop.stringValue as T;
                    case SerializedPropertyType.Color:
                        return prop.colorValue as T;
                    case SerializedPropertyType.Enum:
                        return prop.enumValueIndex as T;
                    case SerializedPropertyType.Vector2:
                        return prop.vector2Value as T;
                    case SerializedPropertyType.Vector3:
                        return prop.vector3Value as T;
                    case SerializedPropertyType.Vector4:
                        return prop.vector4Value as T;
                    case SerializedPropertyType.Rect:
                        return prop.rectValue as T;
                    case SerializedPropertyType.Character:
                        return prop.intValue as T;
                    case SerializedPropertyType.AnimationCurve:
                        return prop.animationCurveValue as T;
                    case SerializedPropertyType.Bounds:
                        return prop.boundsValue as T;
                    case SerializedPropertyType.Quaternion:
                        return prop.quaternionValue as T;
                    case SerializedPropertyType.ExposedReference:
                        return prop.exposedReferenceValue as T;
                    case SerializedPropertyType.FixedBufferSize:
                        return prop.fixedBufferSize as T;
                    case SerializedPropertyType.Vector2Int:
                        return prop.vector2IntValue as T;
                    case SerializedPropertyType.Vector3Int:
                        return prop.vector3IntValue as T;
                    case SerializedPropertyType.RectInt:
                        return prop.rectIntValue as T;
                    case SerializedPropertyType.BoundsInt:
                        return prop.boundsIntValue as T;
                    default:
                        break;
                }
            }
            return default(T);
        }
        public static object GetSerializedObject(UnityEngine.Object unityObject, string propertyName)
        {
            var serializedObject = new SerializedObject(unityObject);
            return GetSerializedObject(serializedObject, propertyName);
        }
        public static object GetSerializedObject(SerializedObject serializedObject, string propertyName)
        {
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
        public static void SetSerializedObject(UnityEngine.Object unityObject, string propertyName, object value)
        {
            var serializedObject = new SerializedObject(unityObject);
            var prop = serializedObject.FindProperty(propertyName);
            if (prop != null)
            {
                switch (prop.propertyType)
                {
                    case SerializedPropertyType.ObjectReference:
                        prop.objectReferenceValue = value as UnityEngine.Object;
                        break;
                    case SerializedPropertyType.Integer:
                        prop.intValue = (int)value;
                        break;
                    case SerializedPropertyType.Boolean:
                        prop.boolValue = (bool)value;
                        break;
                    case SerializedPropertyType.Float:
                        prop.floatValue = (float)value;
                        break;
                    case SerializedPropertyType.String:
                        prop.stringValue = (string)value;
                        break;
                    case SerializedPropertyType.Color:
                        prop.colorValue = (Color)value;
                        break;
                    case SerializedPropertyType.Enum:
                        prop.enumValueIndex = (int)value;
                        break;
                    case SerializedPropertyType.Vector2:
                        prop.vector2Value = (Vector2)value;
                        break;
                    case SerializedPropertyType.Vector3:
                        prop.vector3Value = (Vector3)value;
                        break;
                    case SerializedPropertyType.Vector4:
                        prop.vector4Value = (Vector4)value;
                        break;
                    case SerializedPropertyType.Rect:
                        prop.rectValue = (Rect)value;
                        break;
                    case SerializedPropertyType.ArraySize:
                        prop.arraySize = (int)value;
                        break;
                    case SerializedPropertyType.Character:
                        prop.intValue = (int)value;
                        break;
                    case SerializedPropertyType.AnimationCurve:
                        prop.animationCurveValue = (AnimationCurve)value;
                        break;
                    case SerializedPropertyType.Bounds:
                        prop.boundsValue = (Bounds)value;
                        break;
                    case SerializedPropertyType.Quaternion:
                        prop.quaternionValue = (Quaternion)value;
                        break;
                    case SerializedPropertyType.ExposedReference:
                        prop.exposedReferenceValue = (UnityEngine.Object)value;
                        break;
                    case SerializedPropertyType.Vector2Int:
                        prop.vector2IntValue = (Vector2Int)value;
                        break;
                    case SerializedPropertyType.Vector3Int:
                        prop.vector3IntValue = (Vector3Int)value;
                        break;
                    case SerializedPropertyType.RectInt:
                        prop.rectIntValue = (RectInt)value;
                        break;
                    case SerializedPropertyType.BoundsInt:
                        prop.boundsIntValue = (BoundsInt)value;
                        break;
                    default:
                        break;
                }
                serializedObject.ApplyModifiedProperties();
            }
        }
        public static void SetSerializedObject(SerializedObject serializedObject, string propertyName, object value)
        {
            var prop = serializedObject.FindProperty(propertyName);
            if (prop != null)
            {
                switch (prop.propertyType)
                {
                    case SerializedPropertyType.ObjectReference:
                        prop.objectReferenceValue = value as UnityEngine.Object;
                        break;
                    case SerializedPropertyType.Integer:
                        prop.intValue = (int)value;
                        break;
                    case SerializedPropertyType.Boolean:
                        prop.boolValue = (bool)value;
                        break;
                    case SerializedPropertyType.Float:
                        prop.floatValue = (float)value;
                        break;
                    case SerializedPropertyType.String:
                        prop.stringValue = (string)value;
                        break;
                    case SerializedPropertyType.Color:
                        prop.colorValue = (Color)value;
                        break;
                    case SerializedPropertyType.Enum:
                        prop.enumValueIndex = (int)value;
                        break;
                    case SerializedPropertyType.Vector2:
                        prop.vector2Value = (Vector2)value;
                        break;
                    case SerializedPropertyType.Vector3:
                        prop.vector3Value = (Vector3)value;
                        break;
                    case SerializedPropertyType.Vector4:
                        prop.vector4Value = (Vector4)value;
                        break;
                    case SerializedPropertyType.Rect:
                        prop.rectValue = (Rect)value;
                        break;
                    case SerializedPropertyType.ArraySize:
                        prop.arraySize = (int)value;
                        break;
                    case SerializedPropertyType.Character:
                        prop.intValue = (int)value;
                        break;
                    case SerializedPropertyType.AnimationCurve:
                        prop.animationCurveValue = (AnimationCurve)value;
                        break;
                    case SerializedPropertyType.Bounds:
                        prop.boundsValue = (Bounds)value;
                        break;
                    case SerializedPropertyType.Quaternion:
                        prop.quaternionValue = (Quaternion)value;
                        break;
                    case SerializedPropertyType.ExposedReference:
                        prop.exposedReferenceValue = (UnityEngine.Object)value;
                        break;
                    case SerializedPropertyType.Vector2Int:
                        prop.vector2IntValue = (Vector2Int)value;
                        break;
                    case SerializedPropertyType.Vector3Int:
                        prop.vector3IntValue = (Vector3Int)value;
                        break;
                    case SerializedPropertyType.RectInt:
                        prop.rectIntValue = (RectInt)value;
                        break;
                    case SerializedPropertyType.BoundsInt:
                        prop.boundsIntValue = (BoundsInt)value;
                        break;
                    default:
                        break;
                }
                serializedObject.ApplyModifiedProperties();
            }
        }
        static object _GetSerializedObjectList(SerializedProperty prop)
        {
            var list = new List<object>();
            if (prop != null)
            {
                for (int i = 0; i < prop.arraySize; i++)
                {
                    var element = prop.GetArrayElementAtIndex(i);
                    if (element != null)
                    {
                        if (element.propertyType == SerializedPropertyType.ArraySize)
                        {
                            list.Add(_GetSerializedObjectList(element));
                        }
                        else if (element.propertyType == SerializedPropertyType.ObjectReference)
                        {
                            list.Add(element.objectReferenceValue);
                        }
                        else switch (element.propertyType)
                            {
                                case SerializedPropertyType.ObjectReference:
                                    list.Add(element.objectReferenceValue);
                                    break;
                                case SerializedPropertyType.Integer:
                                    list.Add(element.intValue);
                                    break;
                                case SerializedPropertyType.Boolean:
                                    list.Add(element.boolValue);
                                    break;
                                case SerializedPropertyType.Float:
                                    list.Add(element.floatValue);
                                    break;
                                case SerializedPropertyType.String:
                                    list.Add(element.stringValue);
                                    break;
                                case SerializedPropertyType.Color:
                                    list.Add(element.colorValue);
                                    break;
                                case SerializedPropertyType.Enum:
                                    list.Add(element.enumValueIndex);
                                    break;
                                case SerializedPropertyType.Vector2:
                                    list.Add(element.vector2Value);
                                    break;
                                case SerializedPropertyType.Vector3:
                                    list.Add(element.vector3Value);
                                    break;
                                case SerializedPropertyType.Vector4:
                                    list.Add(element.vector4Value);
                                    break;
                                case SerializedPropertyType.Rect:
                                    list.Add(element.rectValue);
                                    break;
                                case SerializedPropertyType.Character:
                                    list.Add(element.intValue);
                                    break;
                                case SerializedPropertyType.AnimationCurve:
                                    list.Add(element.animationCurveValue);
                                    break;
                                case SerializedPropertyType.Bounds:
                                    list.Add(element.boundsValue);
                                    break;
                                case SerializedPropertyType.Quaternion:
                                    list.Add(element.quaternionValue);
                                    break;
                                case SerializedPropertyType.ExposedReference:
                                    list.Add(element.exposedReferenceValue);
                                    break;
                                case SerializedPropertyType.Vector2Int:
                                    list.Add(element.vector2IntValue);
                                    break;
                                case SerializedPropertyType.Vector3Int:
                                    list.Add(element.vector3IntValue);
                                    break;
                                case SerializedPropertyType.RectInt:
                                    list.Add(element.rectIntValue);
                                    break;
                                case SerializedPropertyType.BoundsInt:
                                    list.Add(element.boundsIntValue);
                                    break;
                                default:
                                    break;
                            }
                    }
                }
            }
            return list.ToArray();
        }
        public static List<T> GetSerializedObjectList<T>(UnityEngine.Object unityObject, string propertyName, bool allowNull = false) where T : class
        {
            var serializedObject = new SerializedObject(unityObject);
            return GetSerializedObjectList<T>(serializedObject, propertyName, allowNull);
        }
        public static List<T> GetSerializedObjectList<T>(SerializedObject serializedObject, string propertyName, bool allowNull = false) where T : class
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
                        T value = default(T);
                        if (element.propertyType == SerializedPropertyType.ObjectReference)
                        {
                            value = element.objectReferenceValue as T;
                            if (value != null || allowNull)
                            {
                                list.Add(value);
                            }
                        }
                        else
                        {
                            switch (element.propertyType)
                            {
                                case SerializedPropertyType.Integer:
                                    {
                                        value = element.intValue as T;
                                    }
                                    break;
                                case SerializedPropertyType.Boolean:
                                    {
                                        value = element.boolValue as T;
                                    }
                                    break;
                                case SerializedPropertyType.Float:
                                    {
                                        value = element.floatValue as T;
                                    }
                                    break;
                                case SerializedPropertyType.String:
                                    {
                                        value = element.stringValue as T;
                                    }
                                    break;
                                case SerializedPropertyType.Color:
                                    {
                                        value = element.colorValue as T;
                                    }
                                    break;
                                case SerializedPropertyType.Enum:
                                    {
                                        value = element.enumValueIndex as T;
                                    }
                                    break;
                                case SerializedPropertyType.Vector2:
                                    {
                                        value = element.vector2Value as T;
                                    }
                                    break;
                                case SerializedPropertyType.Vector3:
                                    {
                                        value = element.vector3Value as T;
                                    }
                                    break;
                                case SerializedPropertyType.Vector4:
                                    {
                                        value = element.vector4Value as T;
                                    }
                                    break;
                                case SerializedPropertyType.Rect:
                                    {
                                        value = element.rectValue as T;
                                    }
                                    break;
                                case SerializedPropertyType.Character:
                                    {
                                        value = element.intValue as T;
                                    }
                                    break;
                                case SerializedPropertyType.AnimationCurve:
                                    {
                                        value = element.animationCurveValue as T;
                                    }
                                    break;
                                case SerializedPropertyType.Bounds:
                                    {
                                        value = element.boundsValue as T;
                                    }
                                    break;
                                case SerializedPropertyType.Quaternion:
                                    {
                                        value = element.quaternionValue as T;
                                    }
                                    break;
                                case SerializedPropertyType.ExposedReference:
                                    {
                                        value = element.exposedReferenceValue as T;
                                    }
                                    break;
                                case SerializedPropertyType.FixedBufferSize:
                                    {
                                        value = element.fixedBufferSize as T;
                                    }
                                    break;
                                case SerializedPropertyType.Vector2Int:
                                    {
                                        value = element.vector2IntValue as T;
                                    }
                                    break;
                                case SerializedPropertyType.Vector3Int:
                                    {
                                        value = element.vector3IntValue as T;
                                    }
                                    break;
                                case SerializedPropertyType.RectInt:
                                    {
                                        value = element.rectIntValue as T;
                                    }
                                    break;
                                case SerializedPropertyType.BoundsInt:
                                    {
                                        value = element.boundsIntValue as T;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (value != null || allowNull)
                        {
                            list.Add(value);
                        }
                    }
                }
            }
            return list;
        }
        public static List<object> GetSerializedObjectList(UnityEngine.Object unityObject, string propertyName, bool allowNull = false)
        {
            var serializedObject = new SerializedObject(unityObject);
            var prop = serializedObject.FindProperty(propertyName);
            var list = new List<object>();
            if (prop != null)
            {
                for (int i = 0; i < prop.arraySize; i++)
                {
                    var element = prop.GetArrayElementAtIndex(i);
                    if (element != null)
                    {
                        if (element.propertyType == SerializedPropertyType.ArraySize)
                        {
                            list.Add(_GetSerializedObjectList(element));
                        }
                        else if (element.propertyType == SerializedPropertyType.ObjectReference)
                        {
                            list.Add(element.objectReferenceValue);
                        }
                        else switch (element.propertyType)
                            {
                                case SerializedPropertyType.Integer:
                                    {
                                        var value = element.intValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Boolean:
                                    {
                                        var value = element.boolValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Float:
                                    {
                                        var value = element.floatValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.String:
                                    {
                                        var value = element.stringValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Color:
                                    {
                                        var value = element.colorValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Enum:
                                    {
                                        var value = element.enumValueIndex as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Vector2:
                                    {
                                        var value = element.vector2Value as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Vector3:
                                    {
                                        var value = element.vector3Value as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Vector4:
                                    {
                                        var value = element.vector4Value as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Rect:
                                    {
                                        var value = element.rectValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Character:
                                    {
                                        var value = element.intValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.AnimationCurve:
                                    {
                                        var value = element.animationCurveValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Bounds:
                                    {
                                        var value = element.boundsValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Quaternion:
                                    {
                                        var value = element.quaternionValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.ExposedReference:
                                    {
                                        var value = element.exposedReferenceValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.FixedBufferSize:
                                    {
                                        var value = element.fixedBufferSize as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Vector2Int:
                                    {
                                        var value = element.vector2IntValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Vector3Int:
                                    {
                                        var value = element.vector3IntValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.RectInt:
                                    {
                                        var value = element.rectIntValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.BoundsInt:
                                    {
                                        var value = element.boundsIntValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                    }
                }
            }
            return list;
        }
        public static List<object> GetSerializedObjectList(SerializedObject serializedObject, string propertyName, bool allowNull = false)
        {
            var prop = serializedObject.FindProperty(propertyName);
            var list = new List<object>();
            if (prop != null)
            {
                for (int i = 0; i < prop.arraySize; i++)
                {
                    var element = prop.GetArrayElementAtIndex(i);
                    if (element != null)
                    {
                        if (element.propertyType == SerializedPropertyType.ArraySize)
                        {
                            list.Add(_GetSerializedObjectList(element));
                        }
                        else if (element.propertyType == SerializedPropertyType.ObjectReference)
                        {
                            list.Add(element.objectReferenceValue);
                        }
                        else switch (element.propertyType)
                            {
                                case SerializedPropertyType.Integer:
                                    {
                                        var value = element.intValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Boolean:
                                    {
                                        var value = element.boolValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Float:
                                    {
                                        var value = element.floatValue as object;
                                        // Debug.Log($"Float: {value}");
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.String:
                                    {
                                        var value = element.stringValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Color:
                                    {
                                        var value = element.colorValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Enum:
                                    {
                                        var value = element.enumValueIndex as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Vector2:
                                    {
                                        var value = element.vector2Value as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Vector3:
                                    {
                                        var value = element.vector3Value as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Vector4:
                                    {
                                        var value = element.vector4Value as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Rect:
                                    {
                                        var value = element.rectValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Character:
                                    {
                                        var value = element.intValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.AnimationCurve:
                                    {
                                        var value = element.animationCurveValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Bounds:
                                    {
                                        var value = element.boundsValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Quaternion:
                                    {
                                        var value = element.quaternionValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.ExposedReference:
                                    {
                                        var value = element.exposedReferenceValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.FixedBufferSize:
                                    {
                                        var value = element.fixedBufferSize as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Vector2Int:
                                    {
                                        var value = element.vector2IntValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.Vector3Int:
                                    {
                                        var value = element.vector3IntValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.RectInt:
                                    {
                                        var value = element.rectIntValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                case SerializedPropertyType.BoundsInt:
                                    {
                                        var value = element.boundsIntValue as object;
                                        if (value != null || allowNull)
                                        {
                                            list.Add(value);
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                    }
                }
            }
            // Debug.Log($"GetSerializedList {propertyName} {list.Count}");
            return list;
        }
        static void _SetSerializedObjectList(SerializedProperty element, object value)
        {
            // Debug.Log($"SetSerializedObjectList {element.name} {value}");
            if (element.propertyType == SerializedPropertyType.ArraySize)
            {
                var list = value as object[];
                if (list != null)
                {
                    element.arraySize = list.Length;
                    for (int i = 0; i < list.Length; i++)
                    {
                        var subElement = element.GetArrayElementAtIndex(i);
                        if (subElement != null)
                        {
                            if (subElement.propertyType == SerializedPropertyType.ArraySize)
                            {
                                _SetSerializedObjectList(subElement, list[i]);
                            }
                            else if (subElement.propertyType == SerializedPropertyType.ObjectReference)
                            {
                                subElement.objectReferenceValue = list[i] as UnityEngine.Object;
                            }
                            else switch (subElement.propertyType)
                                {
                                    case SerializedPropertyType.Integer:
                                        subElement.intValue = (int)list[i];
                                        break;
                                    case SerializedPropertyType.Boolean:
                                        subElement.boolValue = (bool)list[i];
                                        break;
                                    case SerializedPropertyType.Float:
                                        subElement.floatValue = (float)list[i];
                                        break;
                                    case SerializedPropertyType.String:
                                        subElement.stringValue = (string)list[i];
                                        break;
                                    case SerializedPropertyType.Color:
                                        subElement.colorValue = (Color)list[i];
                                        break;
                                    case SerializedPropertyType.ObjectReference:
                                        subElement.objectReferenceValue = list[i] as UnityEngine.Object;
                                        break;
                                    case SerializedPropertyType.LayerMask:
                                        subElement.intValue = (int)list[i];
                                        break;
                                    case SerializedPropertyType.Enum:
                                        subElement.enumValueIndex = (int)list[i];
                                        break;
                                    case SerializedPropertyType.Vector2:
                                        subElement.vector2Value = (Vector2)list[i];
                                        break;
                                    case SerializedPropertyType.Vector3:
                                        subElement.vector3Value = (Vector3)list[i];
                                        break;
                                    case SerializedPropertyType.Vector4:
                                        subElement.vector4Value = (Vector4)list[i];
                                        break;
                                    case SerializedPropertyType.Rect:
                                        subElement.rectValue = (Rect)list[i];
                                        break;
                                    case SerializedPropertyType.Character:
                                        subElement.intValue = (int)list[i];
                                        break;
                                    case SerializedPropertyType.AnimationCurve:
                                        subElement.animationCurveValue = (AnimationCurve)list[i];
                                        break;
                                    case SerializedPropertyType.Bounds:
                                        subElement.boundsValue = (Bounds)list[i];
                                        break;
                                    case SerializedPropertyType.Quaternion:
                                        subElement.quaternionValue = (Quaternion)list[i];
                                        break;
                                    case SerializedPropertyType.ExposedReference:
                                        subElement.exposedReferenceValue = list[i] as UnityEngine.Object;
                                        break;
                                    case SerializedPropertyType.Vector2Int:
                                        subElement.vector2IntValue = (Vector2Int)list[i];
                                        break;
                                    case SerializedPropertyType.Vector3Int:
                                        subElement.vector3IntValue = (Vector3Int)list[i];
                                        break;
                                    case SerializedPropertyType.RectInt:
                                        subElement.rectIntValue = (RectInt)list[i];
                                        break;
                                }
                        }
                    }
                }
            }
            else if (element.propertyType == SerializedPropertyType.ObjectReference)
            {
                element.objectReferenceValue = value as UnityEngine.Object;
            }
            else switch (element.propertyType)
                {
                    case SerializedPropertyType.Integer:
                        element.intValue = (int)value;
                        break;
                    case SerializedPropertyType.Boolean:
                        element.boolValue = (bool)value;
                        break;
                    case SerializedPropertyType.Float:
                        // Debug.Log($"SetSerializedObjectList {element.name} {value}");
                        element.floatValue = (float)value;
                        break;
                    case SerializedPropertyType.String:
                        element.stringValue = (string)value;
                        break;
                    case SerializedPropertyType.Color:
                        element.colorValue = (Color)value;
                        break;
                    case SerializedPropertyType.Enum:
                        element.enumValueIndex = (int)value;
                        break;
                    case SerializedPropertyType.Vector2:
                        element.vector2Value = (Vector2)value;
                        break;
                    case SerializedPropertyType.Vector3:
                        element.vector3Value = (Vector3)value;
                        break;
                    case SerializedPropertyType.Vector4:
                        element.vector4Value = (Vector4)value;
                        break;
                    case SerializedPropertyType.Rect:
                        element.rectValue = (Rect)value;
                        break;
                    case SerializedPropertyType.Character:
                        element.intValue = (int)value;
                        break;
                    case SerializedPropertyType.AnimationCurve:
                        element.animationCurveValue = (AnimationCurve)value;
                        break;
                    case SerializedPropertyType.Bounds:
                        element.boundsValue = (Bounds)value;
                        break;
                    case SerializedPropertyType.Quaternion:
                        element.quaternionValue = (Quaternion)value;
                        break;
                    case SerializedPropertyType.ExposedReference:
                        element.exposedReferenceValue = value as UnityEngine.Object;
                        break;
                    case SerializedPropertyType.Vector2Int:
                        element.vector2IntValue = (Vector2Int)value;
                        break;
                    case SerializedPropertyType.Vector3Int:
                        element.vector3IntValue = (Vector3Int)value;
                        break;
                    case SerializedPropertyType.RectInt:
                        element.rectIntValue = (RectInt)value;
                        break;
                    case SerializedPropertyType.BoundsInt:
                        element.boundsIntValue = (BoundsInt)value;
                        break;
                    default:
                        break;
                }
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
                        _SetSerializedObjectList(element, values[i]);
                        // element.objectReferenceValue = values[i] as UnityEngine.Object;
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
                        _SetSerializedObjectList(element, values[i]);
                        // element.objectReferenceValue = values[i] as UnityEngine.Object;
                    }
                }
                serializedObject.ApplyModifiedProperties();
            }
        }
        // public static void SetSerializedObjectList(UnityEngine.Object unityObject, string propertyName, object values)
        // {
        //     var serializedObject = new SerializedObject(unityObject);
        //     var prop = serializedObject.FindProperty(propertyName);
        //     if (prop != null)
        //     {
        //         var list = values as List<object>;
        //         if (list != null)
        //         {
        //             prop.arraySize = list.Count;
        //             for (int i = 0; i < list.Count; i++)
        //             {
        //                 var element = prop.GetArrayElementAtIndex(i);
        //                 if (element != null)
        //                 {
        //                     _SetSerializedObjectList(element, list[i]);
        //                 }
        //             }
        //             serializedObject.ApplyModifiedProperties();
        //         }
        //     }
        // }
        // public static void SetSerializedObjectList(SerializedObject serializedObject, string propertyName, object values)
        // {

        //     var prop = serializedObject.FindProperty(propertyName);
        //     if (prop != null)
        //     {
        //         var list = values as List<object>;
        //         Debug.Log($"SetSerializedObjectList {prop.name} {list?.Count}");
        //         if (list != null)
        //         {
        //             prop.arraySize = list.Count;
        //             for (int i = 0; i < list.Count; i++)
        //             {
        //                 var element = prop.GetArrayElementAtIndex(i);
        //                 if (element != null)
        //                 {
        //                     _SetSerializedObjectList(element, list[i]);
        //                 }
        //             }
        //             serializedObject.ApplyModifiedProperties();
        //         }
        //     }
        // }
    }
}
