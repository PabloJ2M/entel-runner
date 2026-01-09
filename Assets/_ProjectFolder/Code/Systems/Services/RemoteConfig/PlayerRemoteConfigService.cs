using System;
using UnityEngine;

namespace Unity.Services.RemoteConfig
{
    public class PlayerRemoteConfigService : PlayerServiceBehaviour
    {
        protected override string _dataID => "daily_updates";
        private RemoteConfigData _remoteData = new();

        public RemoteConfigData RemoteData => _remoteData;
        public Action<RemoteConfigData> onRemoteConfigUpdated;

        private struct userAttributes { }
        private struct appAttributes { }

        protected override void Awake()
        {
            base.Awake();
            LoadLocalData(ref _remoteData);
        }
        protected override void OnSignInCompleted()
        {
            if (IsUpToDate()) return;
            RemoteConfigService.Instance.FetchCompleted += OnFetchData;
            RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
        }
        protected override void OnSignOutCompleted()
        {
            RemoteConfigService.Instance.FetchCompleted -= OnFetchData;
        }

        private void OnFetchData(ConfigResponse response)
        {
            string[] keys = RemoteConfigService.Instance.appConfig.GetKeys();
            string json = RemoteConfigService.Instance.appConfig.GetJson(_dataID, string.Empty);
            if (string.IsNullOrEmpty(json)) return;
            
            _remoteData = JsonUtility.FromJson<RemoteConfigData>(json);
            _remoteData.date = DateTime.UtcNow.Date.ToString();

            onRemoteConfigUpdated?.Invoke(_remoteData);
            SaveLocalData(_remoteData);
        }
        public bool IsUpToDate()
        {
            if (string.IsNullOrEmpty(_remoteData.date)) return false;
            if (DateTime.TryParse(_remoteData.date, out DateTime result)) return result.Date == DateTime.UtcNow;
            return false;
        }
    }
}