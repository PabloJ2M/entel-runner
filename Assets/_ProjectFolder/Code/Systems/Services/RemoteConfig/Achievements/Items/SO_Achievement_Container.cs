using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Achievements
{
    [CreateAssetMenu(fileName = "achievement container", menuName = "system/achievements/achievement container", order = 0)]
    public class SO_Achievement_Container : ScriptableObject
    {
        [SerializeField] private SO_Achievement[] _achievements;

        public IEnumerable<SO_Achievement> FindAchievements(IEnumerable<string> ids) => _achievements.Where(x => ids.Contains(x.ID));
        public void ResetAchievements() { foreach (var item in _achievements) item.ClearData(); }
    }
}