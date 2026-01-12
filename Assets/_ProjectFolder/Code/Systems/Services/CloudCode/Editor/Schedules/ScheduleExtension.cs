using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Unity.Services.CloudCode
{
    public static class ScheduleExtension
    {
        private static string ServiceURL => $"https://services.api.unity.com/scheduler/v1";
        private static string URL => ServicesEditor.BaseURL(ServiceURL, "configs");
        //private static string SchedulesURL => ServicesEditor.BaseURL(ServiceURL, "schedules");

        [Serializable] private class ResponseWrapper { public CloudSchedule[] configs; }
        public static async Task<CloudSchedule[]> GetScheduleListAsync()
        {
            string json = string.Empty;

            using UnityWebRequest request = new(URL, RequestType.GET.ToString());
            await WebRequest.SendRequest(request, ServicesEditor.AccessToken, (string result) => json = result);

            if (string.IsNullOrEmpty(json)) return Array.Empty<CloudSchedule>();

            var wrapper = JsonConvert.DeserializeObject<ResponseWrapper>(json);
            return wrapper.configs;
        }
        //public static async Task CheckStatusAsync(string id)
        //{
        //    string json = string.Empty;

        //    using UnityWebRequest request = new(SchedulesURL, RequestType.GET.ToString());
        //    await WebRequest.SendRequest(request, ServicesEditor.AccessToken, (string result) => json = result);

        //    Debug.Log(json);
        //}

        public static async Task CreateAync(EditableDraft draft)
        {
            string json = JsonConvert.SerializeObject(draft.ToJson(), Formatting.Indented);

            using UnityWebRequest request = new(URL, RequestType.POST.ToString());
            await WebRequest.SendRequest(request, ServicesEditor.AccessToken, json);
        }
        public static async Task UpdateAync(string id, EditableDraft draft)
        {
            string json = JsonUtility.ToJson(new CloudSchedule(id, draft, 1));
            using UnityWebRequest request = new($"{URL}/{id}", RequestType.PUT.ToString());
            await WebRequest.SendRequest(request, ServicesEditor.AccessToken, json);
        }
        public static async Task DeleteAync(string id)
        {
            using UnityWebRequest request = UnityWebRequest.Delete($"{URL}/{id}");
            await WebRequest.SendRequest(request, ServicesEditor.AccessToken);
        }
    }
}