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
        public static PropertyFieldKit CreatePropertyField(UnityEngine.Object target, string bindingPath, string label)
        {
            if (target == null) return null;
            var serializedObject = new SerializedObject(target);
            var pfk = new PropertyFieldKit();
            pfk.propertyField = new PropertyField(serializedObject.FindProperty(bindingPath))
            {
                name = bindingPath,
                label = label,
            };
            pfk.propertyField.Bind(serializedObject);
            var field = target.GetType().GetField(bindingPath, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            var property = target.GetType().GetProperty(bindingPath, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (field != null || property != null)
            {
                var headerAttribute = field != null
                ? Attribute.GetCustomAttribute(field, typeof(HeaderAttribute)) as HeaderAttribute
                : Attribute.GetCustomAttribute(property, typeof(HeaderAttribute)) as HeaderAttribute;
                if (headerAttribute != null) pfk.label = CreateTitle(headerAttribute.header);
            }
            return pfk;
        }
        public static ObjectFieldKit CreateObjectField(UnityEngine.Object target, string bindingPath, string label)
        {
            if (target == null) return null;
            var serializedObject = new SerializedObject(target);
            var ofk = new ObjectFieldKit();
            ofk.propertyField = new ObjectField()
            {
                name = bindingPath,
                label = label,
            };
            var field = target.GetType().GetField(bindingPath, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            var property = target.GetType().GetProperty(bindingPath, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (field != null || property != null)
            {
                ofk.propertyField.objectType = field != null
                ? field.FieldType
                : property.PropertyType;
                var headerAttribute = field != null
                ? Attribute.GetCustomAttribute(field, typeof(HeaderAttribute)) as HeaderAttribute
                : Attribute.GetCustomAttribute(property, typeof(HeaderAttribute)) as HeaderAttribute;
                if (headerAttribute != null) ofk.label = CreateTitle(headerAttribute.header);
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
        public static VisualElementKit CreateVisualElementHandler(UnityEngine.Object target, string bindingPath, string label, bool isPropertyField = false)
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
                        var ofk = CreateObjectField(target, bindingPath, label);
                        cehk.label = ofk.label;
                        cehk.propertyField = ofk.propertyField;
                    }
                    else
                    {
                        var pfk = CreatePropertyField(target, bindingPath, label);
                        cehk.label = pfk.label;
                        cehk.propertyField = pfk.propertyField;
                    }
                }
                else if (property != null)
                {
                    if (property.PropertyType.IsSubclassOf(typeof(UnityEngine.Object)))
                    {
                        var ofk = CreateObjectField(target, bindingPath, label);
                        cehk.label = ofk.label;
                        cehk.propertyField = ofk.propertyField;
                    }
                    else
                    {
                        var pfk = CreatePropertyField(target, bindingPath, label);
                        cehk.label = pfk.label;
                        cehk.propertyField = pfk.propertyField;
                    }
                }
            }
            else
            {
                var pfk = CreatePropertyField(target, bindingPath, label);
                cehk.label = pfk.label;
                cehk.propertyField = pfk.propertyField;
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