using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UdonLab.QuickUIElement
{
    public class UIElementMethod
    {
        public static readonly string s_StyleSheetPath = "Assets/Udon Lab/Toolkit/UIElements/StyleSheets/QuickUIElement.uss";
        public static void InsertStyleSheet(ref VisualElement root)
        {
            root.styleSheets.Add(EditorGUIUtility.Load(s_StyleSheetPath) as StyleSheet);
        }
        public static Label CreateLabel(string text, string className)
        {
            Label label = new Label(text);
            label.AddToClassList(className);
            return label;
        }
        public static Label CreateHead(string head)
        {
            return CreateLabel(head, "head");
        }
        public static Label CreateTitle(string title)
        {
            return CreateLabel(title, "title");
        }
        public static Label CreateTips(string tips)
        {
            return CreateLabel(tips, "tips");
        }
        public static PropertyFieldKit CreatePropertyField(UnityEngine.Object target, string bindingPath, string label, bool forceOverrideLabel = false)
        {
            if (target == null) return null;
            var serializedObject = new SerializedObject(target);
            var pfk = new PropertyFieldKit
            {
                propertyField = new PropertyField(serializedObject.FindProperty(bindingPath))
                {
                    name = bindingPath,
                    label = label,
                }
            };
            pfk.propertyField.Bind(serializedObject);
            var field = target.GetType().GetField(bindingPath, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            var property = target.GetType().GetProperty(bindingPath, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (field != null || property != null)
            {
                var headerAttribute = Attribute.GetCustomAttribute(field, typeof(HeaderAttribute)) as HeaderAttribute
                ?? Attribute.GetCustomAttribute(property, typeof(HeaderAttribute)) as HeaderAttribute
                ?? null;
                if (headerAttribute != null) pfk.label = CreateTitle(forceOverrideLabel ? label : headerAttribute.header);
            }
            return pfk;
        }
        public static ObjectFieldKit CreateObjectField(UnityEngine.Object target, string bindingPath, string label, bool forceOverrideLabel = false)
        {
            if (target == null) return null;
            var serializedObject = new SerializedObject(target);
            var ofk = new ObjectFieldKit
            {
                propertyField = new ObjectField()
                {
                    name = bindingPath,
                    label = label,
                }
            };
            var field = target.GetType().GetField(bindingPath, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            var property = target.GetType().GetProperty(bindingPath, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (field != null || property != null)
            {
                ofk.propertyField.objectType = field != null
                ? field.FieldType
                : property.PropertyType;
                var headerAttribute = Attribute.GetCustomAttribute(field, typeof(HeaderAttribute)) as HeaderAttribute
                ?? Attribute.GetCustomAttribute(property, typeof(HeaderAttribute)) as HeaderAttribute
                ?? null;
                if (headerAttribute != null) ofk.label = CreateTitle(forceOverrideLabel ? label : headerAttribute.header);
            }
            ofk.propertyField.value = serializedObject.FindProperty(bindingPath).objectReferenceValue;
            // ofk.propertyField.BindProperty(serializedObject.FindProperty(bindingPath));
            ofk.propertyField.RegisterCallback<ChangeEvent<UnityEngine.Object>>(evt =>
            {
                var prop = serializedObject.FindProperty(bindingPath);
                if (prop != null)
                {
                    prop.objectReferenceValue = evt.newValue;
                    serializedObject.ApplyModifiedProperties();
                }
            });
            return ofk;
        }
        public static VisualElementKit CreateVisualElementHandler(UnityEngine.Object target, string bindingPath, string label, bool isPropertyField = false, bool forceOverrideLabel = false)
        {
            if (target == null) return null;
            var cehk = new VisualElementKit();
            if (!isPropertyField)
            {
                // 判断 bindingPath 是否为 UnityEngine.Object 类型
                var field = target.GetType().GetField(bindingPath, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                var property = target.GetType().GetProperty(bindingPath, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (field != null)
                {
                    if (field.FieldType.IsSubclassOf(typeof(UnityEngine.Object)))
                    {
                        var ofk = CreateObjectField(target, bindingPath, label, forceOverrideLabel);
                        cehk.label = ofk.label;
                        cehk.propertyField = ofk.propertyField;
                    }
                    else
                    {
                        var pfk = CreatePropertyField(target, bindingPath, label, forceOverrideLabel);
                        cehk.label = pfk.label;
                        cehk.propertyField = pfk.propertyField;
                    }
                }
                else if (property != null)
                {
                    if (property.PropertyType.IsSubclassOf(typeof(UnityEngine.Object)))
                    {
                        var ofk = CreateObjectField(target, bindingPath, label, forceOverrideLabel);
                        cehk.label = ofk.label;
                        cehk.propertyField = ofk.propertyField;
                    }
                    else
                    {
                        var pfk = CreatePropertyField(target, bindingPath, label, forceOverrideLabel);
                        cehk.label = pfk.label;
                        cehk.propertyField = pfk.propertyField;
                    }
                }
            }
            else
            {
                var pfk = CreatePropertyField(target, bindingPath, label, forceOverrideLabel);
                cehk.label = pfk.label;
                cehk.propertyField = pfk.propertyField;
            }
            return cehk;
        }
        public static VisualElementKit CreateTField<T>(string name, T value, EventCallback<ChangeEvent<T>> onChange) where T : IConvertible
        {
            // 判断 T 类型
            var cehk = new VisualElementKit();
            switch (typeof(T).Name)
            {
                case "Int32":
                    var intField = new IntegerField(name)
                    {
                        value = Convert.ToInt32(value)
                    };
                    intField.RegisterCallback(onChange);
                    cehk.propertyField = intField;
                    break;
                case "Single":
                    var floatField = new FloatField(name)
                    {
                        value = Convert.ToSingle(value)
                    };
                    floatField.RegisterCallback(onChange);
                    cehk.propertyField = floatField;
                    break;
                case "Int64":
                    var longField = new LongField(name)
                    {
                        value = Convert.ToInt64(value)
                    };
                    longField.RegisterCallback(onChange);
                    cehk.propertyField = longField;
                    break;
                case "Boolean":
                    var toggle = new Toggle(name)
                    {
                        value = Convert.ToBoolean(value)
                    };
                    toggle.RegisterCallback(onChange);
                    cehk.propertyField = toggle;
                    break;
                case "String":
                    var textField = new TextField(name)
                    {
                        value = Convert.ToString(value)
                    };
                    textField.RegisterCallback(onChange);
                    cehk.propertyField = textField;
                    break;
                default:
                    break;
            }
            return cehk;
        }
        public static VisualElementKit CreateUnityTField<T>(string name, T value, EventCallback<ChangeEvent<T>> onChange) where T : class
        {
            // 判断 T 类型
            var cehk = new VisualElementKit();
            switch (typeof(T).Name)
            {
                case "Color":
                    var colorField = new ColorField(name)
                    {
                        value = (Color)(object)value
                    };
                    colorField.RegisterCallback(onChange);
                    cehk.propertyField = colorField;
                    break;
                case "Vector2":
                    var vector2Field = new Vector2Field(name)
                    {
                        value = (Vector2)(object)value
                    };
                    vector2Field.RegisterCallback(onChange);
                    cehk.propertyField = vector2Field;
                    break;
                case "Vector3":
                    var vector3Field = new Vector3Field(name)
                    {
                        value = (Vector3)(object)value
                    };
                    vector3Field.RegisterCallback(onChange);
                    cehk.propertyField = vector3Field;
                    break;
                case "Vector4":
                    var vector4Field = new Vector4Field(name)
                    {
                        value = (Vector4)(object)value
                    };
                    vector4Field.RegisterCallback(onChange);
                    cehk.propertyField = vector4Field;
                    break;
                case "Rect":
                    var rectField = new RectField(name)
                    {
                        value = (Rect)(object)value
                    };
                    rectField.RegisterCallback(onChange);
                    cehk.propertyField = rectField;
                    break;
                case "Bounds":
                    var boundsField = new BoundsField(name)
                    {
                        value = (Bounds)(object)value
                    };
                    boundsField.RegisterCallback(onChange);
                    cehk.propertyField = boundsField;
                    break;
                case "Vector2Int":
                    var vector2IntField = new Vector2IntField(name)
                    {
                        value = (Vector2Int)(object)value
                    };
                    vector2IntField.RegisterCallback(onChange);
                    cehk.propertyField = vector2IntField;
                    break;
                case "Vector3Int":
                    var vector3IntField = new Vector3IntField(name)
                    {
                        value = (Vector3Int)(object)value
                    };
                    vector3IntField.RegisterCallback(onChange);
                    cehk.propertyField = vector3IntField;
                    break;
                case "RectInt":
                    var rectIntField = new RectIntField(name)
                    {
                        value = (RectInt)(object)value
                    };
                    rectIntField.RegisterCallback(onChange);
                    cehk.propertyField = rectIntField;
                    break;
                case "BoundsInt":
                    var boundsIntField = new BoundsIntField(name)
                    {
                        value = (BoundsInt)(object)value
                    };
                    boundsIntField.RegisterCallback(onChange);
                    cehk.propertyField = boundsIntField;
                    break;
                case "AnimationCurve":
                    var animationCurveField = new CurveField(name)
                    {
                        value = (AnimationCurve)(object)value
                    };
                    animationCurveField.RegisterCallback(onChange);
                    cehk.propertyField = animationCurveField;
                    break;
                case "Gradient":
                    var gradientField = new GradientField(name)
                    {
                        value = (Gradient)(object)value
                    };
                    gradientField.RegisterCallback(onChange);
                    cehk.propertyField = gradientField;
                    break;
                default:
                    var objectField = new ObjectField(name)
                    {
                        value = value as UnityEngine.Object,
                        objectType = typeof(T)
                    };
                    objectField.RegisterCallback(onChange);
                    cehk.propertyField = objectField;
                    break;
            }
            return cehk;
        }
        public static string[] GetMethods(UnityEngine.Object target, BindingFlags bindingFlags)
        {
            if (target == null) return new string[0];
            var methods = target.GetType().GetMethods(bindingFlags);
            string[] methodNames = new string[methods.Length];
            for (int i = 0; i < methods.Length; i++)
            {
                methodNames[i] = methods[i].Name;
            }
            return methodNames;
        }
        public static string[] GetFields(UnityEngine.Object target, BindingFlags bindingFlags)
        {
            if (target == null) return new string[0];
            var fields = target.GetType().GetFields(bindingFlags);
            string[] fieldNames = new string[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                fieldNames[i] = fields[i].Name;
            }
            return fieldNames;
        }
        public static string[] GetProperties(UnityEngine.Object target, BindingFlags bindingFlags)
        {
            if (target == null) return new string[0];
            var properties = target.GetType().GetProperties(bindingFlags);
            string[] propertyNames = new string[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                propertyNames[i] = properties[i].Name;
            }
            return propertyNames;
        }
        public class PropertyFieldKit
        {
            public Label label = null;
            public PropertyField propertyField;
        }
        public class ObjectFieldKit
        {
            public Label label = null;
            public ObjectField propertyField;
        }
        public class VisualElementKit
        {
            public Label label = null;
            public VisualElement propertyField;
        }
    }
}