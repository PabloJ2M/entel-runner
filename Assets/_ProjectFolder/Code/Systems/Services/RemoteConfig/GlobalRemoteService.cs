using UnityEngine;

namespace Unity.Services.RemoteConfig
{
    public struct userAttributes { }
    public struct appAttributes { }

    public class GlobalRemoteService : PlayerServiceBehaviour
    {
        [SerializeField] private RemoteDataOverride _remoteData;

        public override string DataID => "daily_updates";

        public RemoteDataOverride RemoteData => _remoteData;

        protected override void OnSignInCompleted()
        {
            RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
            RemoteConfigService.Instance.FetchCompleted += OnFetchData;
        }
        protected override void OnSignOutCompleted()
        {
            
        }

        private void OnFetchData(ConfigResponse response)
        {
            string json = RemoteConfigService.Instance.appConfig.GetJson(DataID, string.Empty);
            if (string.IsNullOrEmpty(json)) return;
            
            _remoteData = JsonUtility.FromJson<RemoteDataOverride>(json);
        }
    }
}