using UnityEngine;

namespace Unity.Achievements
{
    [CreateAssetMenu(fileName = "achievement", menuName = "system/achievements/achievement", order = 1)]
    public class SO_Achievement : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private string _name;
        [SerializeField, TextArea(1, 10)] private string _description;

        [SerializeField] private int _targetValue;
        [SerializeField] private ulong _revenue;
        protected int _progress;

        public string ID => _id;
        public string Name => _name;
        public string Description => _description;
        public bool IsActive { get; set; }

        protected string _achievementID => $"achievement_{_id}";

        public void LoadData()
        {
            if (IsActive && PlayerPrefs.HasKey(_achievementID))
                _progress = PlayerPrefs.GetInt(_achievementID);
        }
        public void SaveData() => PlayerPrefs.SetInt(_achievementID, _progress);
        public void ClearData() => PlayerPrefs.DeleteKey(_achievementID);

        public float Progress() => IsActive ? _progress / _targetValue : 0f;

        public void Add(int amount)
        {
            if (!IsActive) return;
            _progress += amount;
            SaveData();
        }
    }
}