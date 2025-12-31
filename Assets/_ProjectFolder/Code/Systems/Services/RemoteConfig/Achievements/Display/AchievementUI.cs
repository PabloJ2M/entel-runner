using UnityEngine;

namespace Unity.Achievements
{
    using Services.RemoteConfig;
    using Pool;

    public class AchievementUI : PoolObjectSingle<AchievementUI_Entry>
    {
        [Header("Controller")]
        [SerializeField] private SO_Achievement_Container _achievements;

        private GlobalRemoteService _remoteConfig;

        protected override void Awake()
        {
            base.Awake();
            _remoteConfig = FindFirstObjectByType<GlobalRemoteService>(FindObjectsInactive.Include);
        }
        private void Start() => OnBuildAchievements(_remoteConfig.RemoteData);
        private void OnEnable() => _remoteConfig.onRemoteConfigUpdated += OnUpdateRemoteConfig;
        private void OnDisable() => _remoteConfig.onRemoteConfigUpdated -= OnUpdateRemoteConfig;

        private void OnUpdateRemoteConfig(RemoteConfigData data)
        {
            _achievements.ResetAchievements();
            OnBuildAchievements(data);
        }
        private void OnBuildAchievements(RemoteConfigData data)
        {
            ClearPoolInstance();
            var items = _achievements.FindAchievements(data.missions);

            foreach (var item in items) {
                var entry = Pool.Get() as AchievementUI_Entry;
                entry.Init(item);
            }
        }
    }
}