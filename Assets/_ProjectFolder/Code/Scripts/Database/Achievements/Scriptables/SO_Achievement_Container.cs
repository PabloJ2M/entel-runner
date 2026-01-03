using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Achievements
{
    [CreateAssetMenu(fileName = "achievement container", menuName = "system/achievements/achievement container", order = 0)]
    public class SO_Achievement_Container : ScriptableObject
    {
        [SerializeField] private SO_Achievement[] _achievements;
        private Dictionary<string, SO_Achievement> _cache;

        public SO_Achievement Get(string id)
        {
            _cache ??= _achievements.ToDictionary(i => i.ID);
            return _cache.TryGetValue(id, out var item) ? item : null;
        }
        public void ResetAchievements()
        {
            foreach (var item in _achievements)
                item.ClearData();
        }
    }
}