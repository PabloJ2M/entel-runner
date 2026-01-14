namespace Unity.Achievements
{
    using Services;
    using Services.Economy;
    using Services.RemoteConfig;
    using Pool;

    public class AchievementUI : PoolObjectSingle<AchievementUI_Entry>
    {
        private PlayerEconomyService _economy;
        private AchievementController _database;

        private ConfigType _selected = ConfigType.daily_missions;

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

        public void UpdateSelected(ConfigType type)
        {
            if (!_database) return;

            _selected = type;
            OnBuildAchievements();
        }
        private void OnBuildAchievements()
        {
            if (_database.Achievements.Count == 0) return;
            ClearPoolInstance();

            foreach (var achievement in _database.Achievements[_selected]) {
                var entry = Pool.Get() as AchievementUI_Entry;
                entry.Init(achievement);
            }
        }

        public void ClaimReward(AchievementRevenue revenue) =>
            _economy.AddBalanceID(revenue.balance, revenue.amount);
    }
}