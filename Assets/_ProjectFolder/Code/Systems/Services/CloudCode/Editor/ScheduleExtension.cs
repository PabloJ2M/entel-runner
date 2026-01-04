using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace Unity.Services.CloudCode
{
    public static class ScheduleExtension
    {
        private const string PROJECT_ID = "00cea21b-b045-40ec-a6a9-21a4586964d3";
        private const string ENVIRONMENT_ID = "7ebbedf3-8bf5-4de9-9916-be24a68f0aad";

        private static string BaseUrl => $"https://services.api.unity.com/scheduler/v1/projects/{PROJECT_ID}/environments/{ENVIRONMENT_ID}/configs";

        private const string KEY_ID = "905a1693-7b02-48d1-bd01-c3a804d478ae";
        private const string SECRET_KEY = "KwMAMR8xKdl3KM0_AFuDD4WddPfEbXe7";
        private static string AccessToken => Convert.ToBase64String(Encoding.UTF8.GetBytes($"{KEY_ID}:{SECRET_KEY}"));

        [Serializable] private class ResponseWrapper { public CloudSchedule[] configs; }
        public static async Task<CloudSchedule[]> GetScheduleList()
        {
            using UnityWebRequest request = new(BaseUrl, RequestType.GET.ToString());
            string json = string.Empty;

            await WebRequest.SendRequest(request, $"Basic {AccessToken}", (string result) => json = result);
            if (string.IsNullOrEmpty(json)) return null;

            var wrapper = JsonConvert.DeserializeObject<ResponseWrapper>(json);
            return wrapper.configs;
        }

        public static async Task Create(EditableDraft draft)
        {
            using UnityWebRequest request = new(BaseUrl, RequestType.POST.ToString());
            string json = JsonConvert.SerializeObject(draft.ToJson(), Formatting.Indented);

            await WebRequest.SendRequest(request, $"Basic {AccessToken}", json);
        }
        public static async Task Update(string id, EditableDraft draft)
        {
            using UnityWebRequest request = new($"{BaseUrl}/{id}", RequestType.PUT.ToString());
            string json = JsonUtility.ToJson(new CloudSchedule(id, draft, 1));

            await WebRequest.SendRequest(request, $"Basic {AccessToken}", json);
        }
        public static async Task Delete(string id)
        {
            using UnityWebRequest request = UnityWebRequest.Delete($"{BaseUrl}/{id}");
            await WebRequest.SendRequest(request, $"Basic {AccessToken}");
        }
    }
}