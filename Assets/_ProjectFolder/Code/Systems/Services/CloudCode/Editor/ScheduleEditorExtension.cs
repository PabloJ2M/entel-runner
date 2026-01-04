using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Unity.Services.CloudCode
{
    public static class ScheduleEditorExtension
    {
        private static readonly string[] _types = { "recurring", "one-time" };

        public static void ScheduleGUI(this EditableDraft draft, ref int selected)
        {
            GUILayout.BeginHorizontal();
            draft.schedule = EditorGUILayout.TextField("Schedule", draft.schedule);

            selected = EditorGUILayout.Popup(selected, _types, GUILayout.Width(100));
            draft.type = _types[selected];
            GUILayout.EndHorizontal();
        }
        public static void ScheduleParamsGUI(this EditableDraft draft)
        {
            if (GUILayout.Button("Add Param")) draft.payload.@params.Add($"param {draft.payload.@params.Count}", null);
            var list = draft.payload.@params.Keys.ToList();

            foreach (var key in list)
            {
                GUILayout.BeginHorizontal();

                GUILayout.Label(key, GUILayout.Width(100));
                draft.payload.@params[key] = EditorGUILayout.TextField(draft.payload.@params[key]?.ToString());
                if (GUILayout.Button("X", GUILayout.Width(20))) draft.payload.@params.Remove(key);
                
                GUILayout.EndHorizontal();
            }
        }
    }
}