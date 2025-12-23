using UnityEditor;
using UnityEngine;

namespace System.Collections.Generic
{
    [CustomPropertyDrawer(typeof(SerializableDictionary<,>.SerializableKeyValuePair))]
    public class SerializableDictionaryDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var keyProp = property.FindPropertyRelative("Key");
            var valueProp = property.FindPropertyRelative("Value");

            float spacing = 4f;
            float half = (position.width - spacing) * 0.5f;

            Rect keyRect = new Rect(position.x, position.y, half, position.height);
            Rect valueRect = new Rect(position.x + half + spacing, position.y, half, position.height);

            EditorGUI.PropertyField(keyRect, keyProp, GUIContent.none, true);
            EditorGUI.PropertyField(valueRect, valueProp, GUIContent.none, true);

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var keyProp = property.FindPropertyRelative("Key");
            var valueProp = property.FindPropertyRelative("Value");

            return Mathf.Max(
                EditorGUI.GetPropertyHeight(keyProp, true),
                EditorGUI.GetPropertyHeight(valueProp, true)
            );
        }
    }
}