using System.Collections.Generic;
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
            EditorGUILayout.LabelField("Params");

            for (int i = 0; i < draft.payload.@params.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();

                var entry = draft.payload.@params[i];
                entry.key = EditorGUILayout.TextField(entry.key);
                entry.type = (ParamType)EditorGUILayout.EnumPopup(entry.type);
                entry.value = EditorGUILayout.TextField(entry.value);

                if (GUILayout.Button("X", GUILayout.Width(20))) {
                    draft.payload.@params.RemoveAt(i);
                    break;
                }

                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("+ Add Param"))
                draft.payload.@params.Add(new());
        }

        public static IDictionary<string, object> BuildParams(this IList<ParamEntry> @params)
        {
            var dictionary = new Dictionary<string, object>();

            foreach (var param in @params) {
                if (!string.IsNullOrWhiteSpace(param.key))
                    dictionary[param.key] = ParseValue(param);
            }

            return dictionary;
        }
        public static IList<ParamEntry> ToParamEntries(this IDictionary<string, object> dictionary)
        {
            var list = new List<ParamEntry>();

            foreach (var kv in dictionary)
                list.Add(new() { key = kv.Key, type = DetectType(kv.Value), value = kv.Value?.ToString() ?? "" });

            return list;
        }

        private static object ParseValue(ParamEntry p)
        {
            return p.type switch {
                ParamType.Int => int.TryParse(p.value, out var i) ? i : 0,
                ParamType.Float => float.TryParse(p.value, out var f) ? f : 0f,
                ParamType.Bool => bool.TryParse(p.value, out var b) && b,
                _ => p.value
            };
        }
        private static ParamType DetectType(object value)
        {
            return value switch {
                int => ParamType.Int,
                long => ParamType.Int,
                float => ParamType.Float,
                double => ParamType.Float,
                bool => ParamType.Bool,
                _ => ParamType.String
            };
        }
    }
}