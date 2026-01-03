namespace Unity.Achievements
{
    using Services;
    using Services.Economy;
    using Pool;

    public class AchievementUI : PoolObjectSingle<AchievementUI_Entry>
    {
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
            _database.onAchievementsUpdated -= OnBuildAchievements;
        }

        private void OnBuildAchievements()
        {
            if (_database.DailyAchievements.Count == 0) return;
            ClearPoolInstance();

            foreach (var mission in _database.DailyAchievements)
            {
                var entry = Pool.Get() as AchievementUI_Entry;
                entry.Init(mission);
            }
        }
        public void ClaimReward(AchievementRevenue revenue)
        {
            _economy.AddBalanceID(revenue.balance, revenue.amount);
        }
    }
}