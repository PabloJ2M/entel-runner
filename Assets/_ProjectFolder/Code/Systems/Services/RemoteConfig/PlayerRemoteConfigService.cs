using System;
using UnityEngine;

namespace Unity.Services.RemoteConfig
{
    public class PlayerRemoteConfigService : UnityServiceBehaviour
    {
        protected override string _localDataID => "remote_config";
        private DateTime _lastUpdate;

        public event Action<string> onRemoteConfigUpdated;
        public event Action onRemoteConfigCompleted;

        private struct appAttributes { }
        private struct userAttributes { }

        protected override void Awake()
        {
            base.Awake();
            string savedDate = string.Empty;

            LoadLocalData(ref savedDate);
            DateTime.TryParse(savedDate, out _lastUpdate);
        }
        protected override void OnSignInCompleted()
        {
            if (IsUpToDate()) return;

            Debug.Log("Sign-in completed -> Fetch Remote Config");

            if (IsUpToDate())
            {
                Debug.Log("Remote Config already up to date");
                return;
            }

            RemoteConfigService.Instance.FetchCompleted -= OnFetchData;
            RemoteConfigService.Instance.FetchCompleted += OnFetchData;
            RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
        }
        protected override void OnSignOutCompleted()
        {
            _lastUpdate = DateTime.MinValue;
            RemoteConfigService.Instance.FetchCompleted -= OnFetchData;
        }

        private void OnFetchData(ConfigResponse response)
        {
            Debug.Log($"RemoteConfig status: {response.status}");

            if (response.status != ConfigRequestStatus.Success)
            {
                Debug.LogWarning("Remote Config fetch failed");
                return;
            }

            string[] keys = RemoteConfigService.Instance.appConfig.GetKeys();

            foreach (string key in keys)
                onRemoteConfigUpdated?.Invoke(key);

            onRemoteConfigCompleted?.Invoke();
            _lastUpdate = DateTime.UtcNow.Date;
            SaveLocalData(_lastUpdate.ToString());
        }

        private bool IsUpToDate()
        {
            Debug.Log($"last update: {_lastUpdate.Date}");
            Debug.Log($"today: {DateTime.UtcNow.Date}");
            return _lastUpdate != DateTime.MinValue && _lastUpdate.Date == DateTime.UtcNow.Date;
        }

        public string GetJson(string key) => RemoteConfigService.Instance.appConfig.GetJson(key, string.Empty);
        public string GetString(string key) => RemoteConfigService.Instance.appConfig.GetString(key);
        public int GetInteger(string key) => RemoteConfigService.Instance.appConfig.GetInt(key);
        public long GetLong(string key) => RemoteConfigService.Instance.appConfig.GetLong(key);
        public float GetFloat(string key) => RemoteConfigService.Instance.appConfig.GetFloat(key);
        public bool GetBoolean(string key) => RemoteConfigService.Instance.appConfig.GetBool(key);
    }
}