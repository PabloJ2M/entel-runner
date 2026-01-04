using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Unity.Services.CloudCode
{
    public class ScheduleEditorWindow : EditorWindow
    {
        private Dictionary<string, EditableDraft> _drafts = new();
        private CloudSchedule[] _cloudList;
        private Vector2 _scroll;

        private int _selectedType;

        [MenuItem("Services/Cloud Code/Schedules")]
        public static void Open() => GetWindow<ScheduleEditorWindow>("Cloud Schedules");

        private void OnGUI()
        {
            if (GUILayout.Button("Refresh")) _ = Refresh();
            
            GUILayout.Space(10);
            DrawTableHeader();

            _scroll = GUILayout.BeginScrollView(_scroll);

            if (_cloudList != null)
                foreach (var cloud in _cloudList)
                    DrawRow(cloud);

            GUILayout.EndScrollView();

            GUILayout.Space(10);
            DrawCreateSection();
        }
        private async Task Refresh()
        {
            _cloudList = await ScheduleExtension.GetScheduleList();
            _drafts.Clear();

            foreach (var cloud in _cloudList) _drafts[cloud.id] = new(cloud);
            Repaint();
        }

        private void DrawTableHeader()
        {
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            GUILayout.Label("Event", GUILayout.Width(150));
            GUILayout.Label("Cron / Date", GUILayout.Width(130));
            GUILayout.Label("Fn", GUILayout.Width(100));
            GUILayout.Label("v", GUILayout.Width(25));
            GUILayout.Label("Actions", GUILayout.Width(160));
            GUILayout.EndHorizontal();
        }
        private void DrawRow(CloudSchedule cloud)
        {
            var draft = _drafts[cloud.id];

            GUILayout.BeginHorizontal("box");

            draft.eventName = GUILayout.TextField(draft.eventName, GUILayout.Width(150));
            draft.schedule = GUILayout.TextField(draft.schedule, GUILayout.Width(130));
            draft.payload.functionName = GUILayout.TextField(draft.payload.functionName, GUILayout.Width(100));

            GUILayout.Label(draft.payloadVersion.ToString(), GUILayout.Width(25));

            //if (GUILayout.Button("Edit", GUILayout.Width(45))) ScheduleEditorWindowPopup.Open(draft);
            if (GUILayout.Button("Save", GUILayout.Width(45))) _ = ScheduleExtension.Update(cloud.id, draft);
            if (GUILayout.Button("X", GUILayout.Width(25))) _ = ScheduleExtension.Delete(cloud.id);

            GUILayout.EndHorizontal();
        }
        private void DrawCreateSection()
        {
            GUILayout.Label("Create new", EditorStyles.boldLabel);

            if (!_drafts.ContainsKey("NEW")) _drafts["NEW"] = new EditableDraft();

            var draft = _drafts["NEW"];
            draft.name = EditorGUILayout.TextField("Name", draft.name);
            draft.eventName = EditorGUILayout.TextField("Event", draft.eventName);
            draft.payload.functionName = EditorGUILayout.TextField("Function", draft.payload.functionName);
            draft.ScheduleGUI(ref _selectedType);
            draft.ScheduleParamsGUI();

            if (GUILayout.Button("Create")) _ = ScheduleExtension.Create(draft);
        }
    }
}