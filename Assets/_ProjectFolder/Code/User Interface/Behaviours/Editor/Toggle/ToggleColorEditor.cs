using UnityEditor;
using UnityEditor.UI;

namespace UnityEngine.UI
{
    [CustomEditor(typeof(ToggleColor)), CanEditMultipleObjects]
    public class ToggleColorEditor : ToggleEditor
    {
        private SerializedProperty _icon;
        private SerializedProperty _onColor, _offColor;

        protected override void OnEnable()
        {
            base.OnEnable();
            _icon = serializedObject.FindProperty("_icon");
            _onColor = serializedObject.FindProperty("_onColor");
            _offColor = serializedObject.FindProperty("_offColor");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Icon Toggle", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_icon);
            EditorGUILayout.PropertyField(_onColor);
            EditorGUILayout.PropertyField(_offColor);

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space();
            base.OnInspectorGUI();
        }
    }
}