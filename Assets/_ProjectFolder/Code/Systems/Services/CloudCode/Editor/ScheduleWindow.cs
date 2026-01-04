using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Unity.Services.CloudCode
{
    public class ScheduleWindow : EditorWindow
    {
        private Dictionary<string, EditableDraft> _drafts = new();
        private CloudSchedule[] _cloudList;
        private Vector2 _scroll;

        [MenuItem("Services/Cloud Code/Schedules")]
        public static void Open() => GetWindow<ScheduleWindow>("Cloud Schedules");

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
            _cloudList = await SchedulerExtension.GetScheduleList();
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

            GUILayout.Label(cloud.payloadVersion.ToString(), GUILayout.Width(25));

            if (GUILayout.Button("Edit", GUILayout.Width(45))) ScheduleEditPopup.Open(cloud, draft);
            if (GUILayout.Button("Save", GUILayout.Width(45))) _ = SchedulerExtension.Update(cloud);
            if (GUILayout.Button("X", GUILayout.Width(25))) _ = SchedulerExtension.Delete(cloud.id);

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
            draft.schedule = EditorGUILayout.TextField("Schedule", draft.schedule);
            //draft.payload.@params = EditorGUILayout.TextArea(draft.payload.@params, GUILayout.Height(60));

            if (GUILayout.Button("Create")) _ = SchedulerExtension.Create(draft);
        }

        //private void DrawScheduleFields()
        //{
        //    _schedule.name = EditorGUILayout.TextField("Schedule Name", _schedule.name);
        //    _schedule.eventName = EditorGUILayout.TextField("Cloud Code Function", _schedule.eventName);

        //    _selectedType = EditorGUILayout.Popup(_selectedType, _types);
        //    _schedule.type = _types[_selectedType];

        //    EditorGUILayout.HelpBox("Defines when the planned event should occur", MessageType.Info);
        //    _schedule.schedule = EditorGUILayout.TextField("Schedule Config", _schedule.schedule);
        //    _schedule.payloadVersion = (uint)EditorGUILayout.IntField("Payload Version", (int)_schedule.payloadVersion);

        //    GUILayout.Label("Payload JSON");
        //    _schedule.payload.functionName = EditorGUILayout.TextField(_schedule.payload.functionName);
        //    _schedule.payload.@params = EditorGUILayout.TextField(_schedule.payload.@params, GUILayout.Height(80));

        //    EditorUtility.SetDirty(_schedule);
        //}
        //private async void UploadSchedule(CloudSchedule schedule)
        //{
        //    var body = new
        //    {
        //        name = "test-daily",
        //        eventName = "test-updates",
        //        type = "recurring",
        //        schedule = "0 0 * * *",
        //        payloadVersion = 1,
        //        payload = "{\"functionName\":\"daily-updates\",\"params\":{}}"
        //    };

        //    string json = JsonConvert.SerializeObject(body, Formatting.Indented);
        //    using UnityWebRequest request = new UnityWebRequest(URL, RequestType.POST.ToString());
        //    await WebRequest.SendRequest(request, $"Basic {AccessToken}", json);
        //}
        //private async void DeleteSchedule(CloudSchedule schedule)
        //{
        //    await Task.Yield();
        //}
    }
}