using System.Text;

namespace UnityEngine.Networking
{
    using Result = UnityWebRequest.Result;
    public enum RequestType { POST, GET, PUT }

    public static class WebRequest
    {
        public static async Awaitable<string> SendRequest(UnityWebRequest request, string auth, string json)
        {
            if (!string.IsNullOrEmpty(json)) request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            return await SendRequest(request, auth);
        }
        public static async Awaitable<string> SendRequest(UnityWebRequest request, string auth)
        {
            if (!string.IsNullOrEmpty(auth)) request.SetRequestHeader("Authorization", auth);
            return await SendRequest(request);
        }
        public static async Awaitable<string> SendRequest(UnityWebRequest request)
        {
            request.SetRequestHeader("Content-Type", "application/json");
            request.downloadHandler = new DownloadHandlerBuffer();
            await request.SendWebRequest();

            if (request.result == Result.Success) return request.downloadHandler.text;
            else Debug.LogError(request.error);
            return string.Empty;
        }
    }
}