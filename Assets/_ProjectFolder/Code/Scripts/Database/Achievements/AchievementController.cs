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

        protected override void Awake()
        {
            base.Awake();
            LoadLocalData(ref _achievements);
        }
        private async void Start()
        {
            await Awaitable.EndOfFrameAsync();
            if (_achievements.groups.Count != 0) OnRemoteConfigCompleted();
        }

        public void AddListener(AchievementField field, ConfigType type) => _fields.Add(type, field);
        public void RemoveListener(ConfigType type) => _fields.Remove(type);

        protected override void OnRemoteConfigUpdated(string key)
        {
            if (!Enum.TryParse(key, out ConfigType type)) return;
            if (!_fields.ContainsKey(type)) return;

            var newData = JsonUtility.FromJson<AchievementRemote>(_remoteConfig.GetJson(key));

            if (_achievements.groups.ContainsKey(key))
                _fields[type].ResetAchievements(_achievements.groups[key].date, newData.date);

            _achievements.groups[key] = newData;
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