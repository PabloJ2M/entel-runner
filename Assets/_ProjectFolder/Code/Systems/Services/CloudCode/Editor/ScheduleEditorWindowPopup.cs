using UnityEditor;
using UnityEngine;

namespace Unity.Services.CloudCode
{
    public class ScheduleEditorWindowPopup : EditorWindow
    {
        private EditableDraft draft;
        private int _selectedType;

        public static void Open(EditableDraft d)
        {
            var window = CreateInstance<ScheduleEditorWindowPopup>();
            window.titleContent = new GUIContent(d.eventName);
            window.draft = d;
            window.ShowUtility();
        }
        private void OnGUI()
        {
            draft.payload.functionName = GUILayout.TextField(draft.payload.functionName);
            draft.ScheduleGUI(ref _selectedType);

            GUILayout.Label("Payload Params (JSON)");
            draft.ScheduleParamsGUI();
        }
    }
}