using UnityEditor;
using UnityEditor.UI;

namespace UnityEngine.UI
{
    [CustomEditor(typeof(ButtonString))]
    public class ButtonStringEditor : ButtonEditor
    {
        private SerializedProperty _textUI;
        private SerializedProperty _stringIndex;
        private SerializedProperty _textPrefix;

        protected override void OnEnable()
        {
            base.OnEnable();
            _textUI = serializedObject.FindProperty("_textUI");
            _stringIndex = serializedObject.FindProperty("_stringIndex");
            _textPrefix = serializedObject.FindProperty("_textPrefix");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Text Button", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_textUI);
            EditorGUILayout.PropertyField(_stringIndex);
            EditorGUILayout.PropertyField(_textPrefix);

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space();
            base.OnInspectorGUI();
        }
    }
}