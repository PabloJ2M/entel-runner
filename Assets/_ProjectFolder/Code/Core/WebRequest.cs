using System;
using System.Text;
using System.Threading.Tasks;

namespace UnityEngine.Networking
{
    using Result = UnityWebRequest.Result;
    public enum RequestType { POST, GET, PUT }

    public static class WebRequest
    {
        public static async Task SendRequest(UnityWebRequest request, string auth, string json, Action<string> result = null)
        {
            if (!string.IsNullOrEmpty(json)) request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            await SendRequest(request, auth, result);
        }
        public static async Task SendRequest(UnityWebRequest request, string auth, Action<string> result = null)
        {
            if (!string.IsNullOrEmpty(auth)) request.SetRequestHeader("Authorization", auth);
            await SendRequest(request, result);
        }
        public static async Task SendRequest(UnityWebRequest request, Action<string> result = null)
        {
            request.SetRequestHeader("Content-Type", "application/json");
            request.downloadHandler = new DownloadHandlerBuffer();
            await request.SendWebRequest();

            if (request.result == Result.Success) result?.Invoke(request.downloadHandler.text);
            else Debug.LogError(request.error);
        }
    }
}