using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Achievements
{
    using Services.RemoteConfig;

    public class AchievementController : RemoteConfigListener
    {
        protected override string _localDataID => "achievements";

        [SerializeField] private AchievementRemoteGroup _achievements = new();
        private Dictionary<ConfigType, AchievementField> _fields = new();

        public Dictionary<ConfigType, IReadOnlyCollection<SO_Achievement>> Achievements { get; private set; } = new();
        public event Action onAchievementsUpdated;

        private void Start()
        {
            LoadLocalData(ref _achievements);
            if (_achievements.groups.Count != 0)
                OnRemoteConfigCompleted();
        }

        public void AddListener(AchievementField field, ConfigType type) => _fields.Add(type, field);
        public void RemoveListener(ConfigType type) => _fields.Remove(type);

        protected override void OnRemoteConfigUpdated(string key, DateTime serverTime)
        {
            if (!Enum.TryParse(key, out ConfigType type)) return;
            if (!_fields.ContainsKey(type)) return;

            if (_achievements.groups.TryGetValue(key, out var group))
            {
                DateTime.TryParse(group.date, out var lastUpdate);
                _fields[type].ResetAchievements(lastUpdate, serverTime);
            }

            _achievements.groups[key] = JsonUtility.FromJson<AchievementRemote>(_remoteConfig.GetJson(key));
            SaveLocalData(_achievements);
        }
        protected override void OnRemoteConfigCompleted()
        {
            ParseConfigData();
            onAchievementsUpdated?.Invoke();
        }
        protected override void ParseConfigData()
        {
            foreach (var groups in _achievements.groups) {
                if (Enum.TryParse(groups.Key, out ConfigType type))
                    Achievements[type] = _fields[type].Parse(groups.Value.missions);
            }
        }
    }
}