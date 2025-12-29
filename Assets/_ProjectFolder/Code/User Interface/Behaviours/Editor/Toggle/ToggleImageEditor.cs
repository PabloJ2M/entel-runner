using UnityEditor;
using UnityEditor.UI;

namespace UnityEngine.UI
{
    [CustomEditor(typeof(ToggleImage)), CanEditMultipleObjects]
    public class ToggleImageEditor : ToggleEditor
    {
        private SerializedProperty _icon;
        private SerializedProperty _onSprite, _offSprite;

        protected override void OnEnable()
        {
            base.OnEnable();
            _icon = serializedObject.FindProperty("_icon");
            _onSprite = serializedObject.FindProperty("_onSprite");
            _offSprite = serializedObject.FindProperty("_offSprite");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Icon Toggle", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_icon);
            EditorGUILayout.PropertyField(_onSprite);
            EditorGUILayout.PropertyField(_offSprite);
            
            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space();
            base.OnInspectorGUI();
        }
    }
}