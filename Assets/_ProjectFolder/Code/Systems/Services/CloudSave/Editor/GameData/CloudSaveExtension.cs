using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Unity.Services.CloudSave
{
    public static class CloudSaveExtension
    {
        private static string ServiceURL => "https://services.api.unity.com/cloud-save/v1/data";
        private static string URL => ServicesEditor.BaseURL(ServiceURL, "custom");

        [Serializable] private class ResponseWrapper { public CloudCustomKey[] results; }
        public static async Awaitable<CloudCustomKey[]> GetAllKeysAsync(string id)
        {
            using UnityWebRequest request = new($"{URL}/{id}/items", RequestType.GET.ToString());
            string json = await WebRequest.SendRequest(request, ServicesEditor.AccessToken);

            if (string.IsNullOrEmpty(json)) return Array.Empty<CloudCustomKey>();

            var wrapper = JsonConvert.DeserializeObject<ResponseWrapper>(json);
            return wrapper.results;
        }

        public static async Awaitable SetAsync(string id, CloudCustomKey data, string value)
        {
            string json = JsonConvert.SerializeObject(new CloudCustomKey(data.key, value, data.writeLock), Formatting.Indented);
            
            using UnityWebRequest request = new($"{URL}/{id}/items", RequestType.POST.ToString());
            await WebRequest.SendRequest(request, ServicesEditor.AccessToken, json);
        }

        public static string[] KeyList(this CloudCustomKey[] keys)
        {
            string[] temp = new string[keys.Length];
            for (int i = 0; i < keys.Length; i++) temp[i] = keys[i].key;
            return temp;
        }
    }
}