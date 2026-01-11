using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Achievements
{
    using Services.CloudSave;

    [CreateAssetMenu(fileName = "achievement list", menuName = "system/achievements/achievement list", order = 0)]
    public class SO_Achievement_List : ScriptableObject, ICloudSaveGameData
    {
        [SerializeField] private SO_Achievement[] _achievements;
        private Dictionary<string, SO_Achievement> _cache;

        public string ItemsListToJson()
        {
            IJsonData data = new AchievementCloud(_achievements);
            return data.Json();
        }

        public void ResetAchievements()
        {
            foreach (var item in _achievements)
                item.ClearData();
        }
        public SO_Achievement Get(string id)
        {
            _cache ??= _achievements.ToDictionary(i => i.ID);
            return _cache.TryGetValue(id, out var item) ? item : null;
        }
    }
}