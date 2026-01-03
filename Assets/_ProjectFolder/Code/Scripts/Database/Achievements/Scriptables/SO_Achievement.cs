using UnityEngine;

namespace Unity.Achievements
{
    [CreateAssetMenu(fileName = "achievement", menuName = "system/achievements/achievement", order = 1)]
    public class SO_Achievement : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private AchievementType _type;
        [Space]
        [SerializeField] private string _name;
        [SerializeField, TextArea(1, 10)] private string _description;

        [SerializeField] private AchievementRevenue _revenue;
        [SerializeField] private AchievementStatus _status;

        public AchievementType Type => _type;
        public string ID => _id;
        public string Name => _name;
        public string Description => _description;
        public AchievementStatus Status => _status;
        public AchievementRevenue Revenue => _revenue;

        protected string _achievementID => $"achievement_{_id}";

        public void LoadProgress() => _status = _status.LoadJson(_achievementID);
        public void SaveProgress() => _status.SaveJson(_achievementID);
        public void ClearData()  => _status.ClearJson(_achievementID);

        public void Add(int amount)
        {
            _status.Add(amount);
            SaveProgress();
        }
    }
}