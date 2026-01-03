using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Achievements
{
    using Services.RemoteConfig;

    public class AchievementController : MonoBehaviour
    {
        [SerializeField] private SO_Achievement_Container _listReference;
        [SerializeField] private List<SO_Achievement> _dailyAchievements = new();

        private PlayerRemoteConfigService _remoteConfig;

        public List<SO_Achievement> DailyAchievements => _dailyAchievements;
        public Action onAchievementsUpdated;

        private void Awake() => _remoteConfig = GetComponentInParent<PlayerRemoteConfigService>();
        private void Start() => LoadData(_remoteConfig.RemoteData);
        private void OnEnable() => _remoteConfig.onRemoteConfigUpdated += OnRemoteConfigUpdated;
        private void OnDisable() => _remoteConfig.onRemoteConfigUpdated -= OnRemoteConfigUpdated;

        private void OnRemoteConfigUpdated(RemoteConfigData data)
        {
            if (_remoteConfig.IsUpToDate()) return;

            _listReference.ResetAchievements();
            _dailyAchievements.Clear();
            LoadData(data);
        }
        private void LoadData(RemoteConfigData data)
        {
            if (data.missions == null) return;

            foreach (var mission in data.missions)
            {
                var item = _listReference.Get(mission);
                if (item != null) _dailyAchievements.Add(item);
            }

            onAchievementsUpdated?.Invoke();
        }
    }
}