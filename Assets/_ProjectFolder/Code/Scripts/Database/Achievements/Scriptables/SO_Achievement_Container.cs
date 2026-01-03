using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Achievements
{
    using Services.CloudSave;

    [CreateAssetMenu(fileName = "achievement container", menuName = "system/achievements/achievement container", order = 0)]
    public class SO_Achievement_Container : ScriptableObject, ICloudSaveGameData
    {
        [SerializeField] private SO_Achievement[] _achievements;
        private Dictionary<string, SO_Achievement> _cache;

        private class JsonData { public List<string> missions_list; }

        public string ItemsListToJson()
        {
            JsonData data = new();
            foreach (var item in _achievements) data.missions_list.Add(item.ID);
            return JsonUtility.ToJson(data);
        }
        public void ResetAchievements()
        {
            foreach (var item in _achievements) item.ClearData();
        }
        public SO_Achievement Get(string id)
        {
            _cache ??= _achievements.ToDictionary(i => i.ID);
            return _cache.TryGetValue(id, out var item) ? item : null;
        }
    }
}