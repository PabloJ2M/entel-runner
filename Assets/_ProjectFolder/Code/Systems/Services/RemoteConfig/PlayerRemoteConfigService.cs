using System;

namespace Unity.Services.RemoteConfig
{
    using CloudCode;

    public class PlayerRemoteConfigService : UnityServiceBehaviour
    {
        protected override string _localDataID => "remote_config";
        private PlayerCloudCodeService _cloudCode;
        private DateTime _lastUpdate;

        public event Action<string, DateTime> onRemoteConfigUpdated;
        public event Action onRemoteConfigCompleted;

        private struct appAttributes { }
        private struct userAttributes { }

        protected override void Awake()
        {
            base.Awake();
            string savedDate = string.Empty;
            LoadLocalData(ref savedDate);

            DateTime.TryParse(savedDate, out _lastUpdate);
            _cloudCode = GetComponent<PlayerCloudCodeService>();
        }
        protected override void OnSignInCompleted()
        {
            if (IsUpToDate()) return;

            RemoteConfigService.Instance.FetchCompleted += OnFetchData;
            RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
        }
        protected override void OnSignOutCompleted()
        {
            _lastUpdate = DateTime.MinValue;
            RemoteConfigService.Instance.FetchCompleted -= OnFetchData;
        }

        private async void OnFetchData(ConfigResponse response)
        {
            await _cloudCode.GetServerTime();
            if (!_cloudCode.ServerTimeUTC.HasResponse) return;

            string[] keys = RemoteConfigService.Instance.appConfig.GetKeys();
            var serverUTC = _cloudCode.ServerTimeUTC.Time;

            foreach (string key in keys)
                onRemoteConfigUpdated?.Invoke(key, serverUTC);

            onRemoteConfigCompleted?.Invoke();
            _lastUpdate = DateTime.UtcNow.Date;
            SaveLocalData(_lastUpdate.ToString());
        }

        private bool IsUpToDate()
        {
            if (_lastUpdate == null) return false;
            return _lastUpdate.Date == DateTime.UtcNow.Date;
        }

        public string GetJson(string key) => RemoteConfigService.Instance.appConfig.GetJson(key, string.Empty);
        public string GetString(string key) => RemoteConfigService.Instance.appConfig.GetString(key);
        public int GetInteger(string key) => RemoteConfigService.Instance.appConfig.GetInt(key);
        public long GetLong(string key) => RemoteConfigService.Instance.appConfig.GetLong(key);
        public float GetFloat(string key) => RemoteConfigService.Instance.appConfig.GetFloat(key);
        public bool GetBoolean(string key) => RemoteConfigService.Instance.appConfig.GetBool(key);
    }
}