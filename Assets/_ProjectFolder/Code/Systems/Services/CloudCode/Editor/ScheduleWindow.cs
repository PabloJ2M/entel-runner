using System;
using System.Text;
using System.Threading.Tasks;
using Unity.Services.Authentication.PlayerAccounts;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Unity.Services.CloudCode
{
    public class ScheduleWindow : EditorWindow
    {
        private const string SERVICE_URL = "https://services.api.unity.com/cloud-code/v1/projects/";
        private const string PROJECT_ID = "00cea21b-b045-40ec-a6a9-21a4586964d3";
        private const string ENVIRONMENT_ID = "production";

        private const string KEY_ID = "905a1693-7b02-48d1-bd01-c3a804d478ae";
        private const string SECRET_KEY = "KwMAMR8xKdl3KM0_AFuDD4WddPfEbXe7";
        private string AccessToken => Convert.ToBase64String(Encoding.UTF8.GetBytes($"{KEY_ID}:{SECRET_KEY}"));

        private CloudSchedule _schedule;

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
            _schedule.scheduleId = EditorGUILayout.TextField("Schedule ID", _schedule.scheduleId);
            _schedule.cloudCodeFunction = EditorGUILayout.TextField("Cloud Code Function", _schedule.cloudCodeFunction);
            _schedule.cronExpression = EditorGUILayout.TextField("Cron", _schedule.cronExpression);

            GUILayout.Label("Payload (JSON)");
            _schedule.payloadJson = EditorGUILayout.TextArea(_schedule.payloadJson, GUILayout.Height(80));

            EditorUtility.SetDirty(_schedule);
        }
        private async void UploadSchedule(CloudSchedule schedule)
        {
            string url = $"{SERVICE_URL}{PROJECT_ID}/environments/{ENVIRONMENT_ID}/schedules/{schedule.scheduleId}";

            var body = new
            {
                name = schedule.scheduleId,
                cron = schedule.cronExpression,
                functionName = schedule.cloudCodeFunction,
                parameters = JsonUtility.FromJson<object>(schedule.payloadJson)
            };

            string json = JsonUtility.ToJson(body);

            using UnityWebRequest request = new UnityWebRequest(url, "PUT");
            await WebRequest.SendRequest(request, $"Basic {AccessToken}", json);
        }
        private async void DeleteSchedule(CloudSchedule schedule)
        {
            await Task.Yield();
        }
    }
}