using System;

namespace Unity.Services.RemoteConfig
{
    public abstract class RemoteConfigListener : SaveLocalBehaviour
    {
        protected PlayerRemoteConfigService _remoteConfig;

        protected virtual void Awake() => _remoteConfig = GetComponentInParent<PlayerRemoteConfigService>();
        protected virtual void OnEnable()
        {
            _remoteConfig.onRemoteConfigUpdated += OnRemoteConfigUpdated;
            _remoteConfig.onRemoteConfigCompleted += OnRemoteConfigCompleted;
        }
        protected virtual void OnDisable()
        {
            _remoteConfig.onRemoteConfigUpdated -= OnRemoteConfigUpdated;
            _remoteConfig.onRemoteConfigCompleted -= OnRemoteConfigCompleted;
        }

        protected abstract void OnRemoteConfigUpdated(string key);
        protected abstract void OnRemoteConfigCompleted();
        protected abstract void ParseConfigData();
    }
}