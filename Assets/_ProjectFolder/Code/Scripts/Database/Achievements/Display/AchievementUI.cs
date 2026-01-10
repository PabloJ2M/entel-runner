using UnityEngine;

namespace Unity.Achievements
{
    using Services;
    using Services.Economy;
    using Services.RemoteConfig;
    using Pool;

    public class AchievementUI : PoolObjectSingle<AchievementUI_Entry>
    {
        [SerializeField] private ConfigType _display;

        private PlayerEconomyService _economy;
        private AchievementController _database;

        protected override void Awake()
        {
            base.Awake();
            var unityServices = UnityServiceInit.Instance;
            _economy = unityServices.GetComponent<PlayerEconomyService>();
            _database = unityServices.GetComponentInChildren<AchievementController>();
        }
        private void OnEnable()
        {
            OnBuildAchievements();
            _database.onAchievementsUpdated += OnBuildAchievements;
        }
        private void OnDisable()
        {
            ClearPoolInstance();
            _database.onAchievementsUpdated -= OnBuildAchievements;
        }

        private void OnBuildAchievements()
        {
            if (_database.Achievements.Count == 0) return;
            ClearPoolInstance();

            foreach (var achievement in _database.Achievements[_display]) {
                var entry = Pool.Get() as AchievementUI_Entry;
                entry.Init(achievement);
            }
        }

        public void ClaimReward(AchievementRevenue revenue) =>
            _economy.AddBalanceID(revenue.balance, revenue.amount);
    }
}