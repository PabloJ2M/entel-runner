using System;
using System.Text;

namespace Unity.Services
{
    public static class ServicesEditor
    {
        private const string PROJECT_ID = "00cea21b-b045-40ec-a6a9-21a4586964d3";
        private const string ENVIRONMENT_ID = "7ebbedf3-8bf5-4de9-9916-be24a68f0aad";

        private const string KEY_ID = "905a1693-7b02-48d1-bd01-c3a804d478ae";
        private const string SECRET_KEY = "KwMAMR8xKdl3KM0_AFuDD4WddPfEbXe7";

        public static string BaseURL(string url, string path) => $"{url}/projects/{PROJECT_ID}/environments/{ENVIRONMENT_ID}/{path}";
        public static string AccessToken => $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{KEY_ID}:{SECRET_KEY}"))}";
    }
}