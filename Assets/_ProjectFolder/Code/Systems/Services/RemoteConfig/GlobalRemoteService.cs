using System;
using UnityEngine;

namespace Unity.Services.RemoteConfig
{
    public class GlobalRemoteService : PlayerServiceBehaviour
    {
        [SerializeField] private RemoteConfigData _remoteData;

        public override string DataID => "daily_updates";
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
            RemoteConfigService.Instance.FetchCompleted += OnFetchData;
            RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
        }
        protected override void OnSignOutCompleted()
        {
            RemoteConfigService.Instance.FetchCompleted -= OnFetchData;
        }

        private void OnFetchData(ConfigResponse response)
        {
            if (DateTime.TryParse(_remoteData.date, out DateTime result)) {
                if (result.Date == DateTime.UtcNow) return;
            }

            string json = RemoteConfigService.Instance.appConfig.GetJson(DataID, string.Empty);
            if (string.IsNullOrEmpty(json)) return;
            
            _remoteData = JsonUtility.FromJson<RemoteConfigData>(json);
            onRemoteConfigUpdated?.Invoke(_remoteData);
        }
    }
}