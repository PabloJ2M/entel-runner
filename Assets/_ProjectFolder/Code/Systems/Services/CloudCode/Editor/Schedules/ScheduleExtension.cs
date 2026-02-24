using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Unity.Services.CloudCode
{
    public static class ScheduleExtension
    {
        private static readonly string ScheduleEndpoint = "https://services.api.unity.com/scheduler/v1";
        private static readonly string TriggerEndpoint = "https://services.api.unity.com/triggers/v1";

        private static string ScheduleURL => ServicesEditor.BaseURL(ScheduleEndpoint, "configs");
        private static string TriggerURL => ServicesEditor.BaseURL(TriggerEndpoint, "configs");

        [Serializable] private class ResponseWrapper { public CloudSchedule[] configs; }
        public static async Awaitable<CloudSchedule[]> GetScheduleListAsync()
        {
            using UnityWebRequest request = new(ScheduleURL, RequestType.GET.ToString());
            string json = await WebRequest.SendRequest(request, ServicesEditor.AccessToken);

            if (string.IsNullOrEmpty(json)) return Array.Empty<CloudSchedule>();

            var wrapper = JsonConvert.DeserializeObject<ResponseWrapper>(json);
            return wrapper.configs;
        }

        public static async Awaitable CreateAync(EditableDraft draft)
        {
            string json = JsonConvert.SerializeObject(draft.ToJson(), Formatting.Indented);

            using UnityWebRequest request = new(ScheduleURL, RequestType.POST.ToString());
            await WebRequest.SendRequest(request, ServicesEditor.AccessToken, json);
        }
        public static async Awaitable UpdateAync(string id, EditableDraft draft)
        {
            string json = JsonUtility.ToJson(new CloudSchedule(id, draft, 1));
            using UnityWebRequest request = new($"{ScheduleURL}/{id}", RequestType.PUT.ToString());
            await WebRequest.SendRequest(request, ServicesEditor.AccessToken, json);
        }
        public static async Awaitable DeleteAync(string id)
        {
            using UnityWebRequest request = UnityWebRequest.Delete($"{ScheduleURL}/{id}");
            await WebRequest.SendRequest(request, ServicesEditor.AccessToken);
        }

        public static async Awaitable AsignTrigger(EditableDraft cloud)
        {
            string json = JsonUtility.ToJson(new TriggerSchedule(cloud));
            using UnityWebRequest request = new(TriggerURL, RequestType.POST.ToString());
            await WebRequest.SendRequest(request, ServicesEditor.AccessToken, json);
        }
    }
}