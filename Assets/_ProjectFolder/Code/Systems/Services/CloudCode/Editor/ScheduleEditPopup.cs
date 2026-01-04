using UnityEditor;
using UnityEngine;

namespace Unity.Services.CloudCode
{
    public class ScheduleEditPopup : EditorWindow
    {
        private EditableDraft draft;
        private CloudSchedule cloud;

        public static void Open(CloudSchedule c, EditableDraft d)
        {
            var window = CreateInstance<ScheduleEditPopup>();
            window.cloud = c;
            window.draft = d;
            window.titleContent = new GUIContent(c.eventName);
            window.ShowUtility();
        }
        private void OnGUI()
        {
            GUILayout.Label("Payload Params (JSON)");
            //draft.payload.@params = EditorGUILayout.TextArea(draft.payload.@params, GUILayout.Height(200));
        }
    }
}