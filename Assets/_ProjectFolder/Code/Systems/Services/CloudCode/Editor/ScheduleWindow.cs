using System;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace Unity.Services.CloudCode
{
    public class ScheduleWindow : EditorWindow
    {
        private const string SERVICE_URL = "https://services.api.unity.com/scheduler/v1/projects";
        private const string PROJECT_ID = "00cea21b-b045-40ec-a6a9-21a4586964d3";
        private const string ENVIRONMENT_ID = "7ebbedf3-8bf5-4de9-9916-be24a68f0aad";
        private string URL => $"{SERVICE_URL}/{PROJECT_ID}/environments/{ENVIRONMENT_ID}/configs";

        private const string KEY_ID = "905a1693-7b02-48d1-bd01-c3a804d478ae";
        private const string SECRET_KEY = "KwMAMR8xKdl3KM0_AFuDD4WddPfEbXe7";
        private string AccessToken => Convert.ToBase64String(Encoding.UTF8.GetBytes($"{KEY_ID}:{SECRET_KEY}"));

        private static readonly string[] _types = { "recurring", "one-time" };

        private CloudSchedule _schedule;
        private int _selectedType;

        [MenuItem("Services/Cloud Code/Schedules")]
        public static void Open() => GetWindow<ScheduleWindow>("Cloud Schedules");

        private void OnGUI()
        {
            GUILayout.Label("Cloud Code Scheduler", EditorStyles.boldLabel);
            _schedule = (CloudSchedule)EditorGUILayout.ObjectField("Schedule", _schedule, typeof(CloudSchedule), false);

            if (_schedule == null) {
                EditorGUILayout.HelpBox("Selecciona un Schedule Definition", MessageType.Warning);
                return;
            }

            EditorGUILayout.Space();
            DrawScheduleFields();

            EditorGUILayout.Space(10);

            if (GUILayout.Button("Create / Update Schedule")) UploadSchedule(_schedule);
            if (GUILayout.Button("Delete Schedule")) DeleteSchedule(_schedule);
        }

        private void DrawScheduleFields()
        {
            _schedule.name = EditorGUILayout.TextField("Schedule Name", _schedule.name);
            _schedule.eventName = EditorGUILayout.TextField("Cloud Code Function", _schedule.eventName);

            _selectedType = EditorGUILayout.Popup(_selectedType, _types);
            _schedule.type = _types[_selectedType];

            EditorGUILayout.HelpBox("Defines when the planned event should occur", MessageType.Info);
            _schedule.schedule = EditorGUILayout.TextField("Schedule Config", _schedule.schedule);
            _schedule.payloadVersion = (uint)EditorGUILayout.IntField("Payload Version", (int)_schedule.payloadVersion);

            GUILayout.Label("Payload JSON");
            _schedule.payload.functionName = EditorGUILayout.TextField(_schedule.payload.functionName);
            _schedule.payload.@params = EditorGUILayout.TextField(_schedule.payload.@params, GUILayout.Height(80));

            EditorUtility.SetDirty(_schedule);
        }
        private async void UploadSchedule(CloudSchedule schedule)
        {
            var body = new
            {
                name = "test-daily",
                eventName = "test-updates",
                type = "recurring",
                schedule = "0 0 * * *",
                payloadVersion = 1,
                payload = "{\"functionName\":\"daily-updates\",\"params\":{}}"
            };

            string json = JsonConvert.SerializeObject(body, Formatting.Indented);
            using UnityWebRequest request = new UnityWebRequest(URL, RequestType.POST.ToString());
            await WebRequest.SendRequest(request, $"Basic {AccessToken}", json);
        }
        private async void DeleteSchedule(CloudSchedule schedule)
        {
            await Task.Yield();
        }
    }
}