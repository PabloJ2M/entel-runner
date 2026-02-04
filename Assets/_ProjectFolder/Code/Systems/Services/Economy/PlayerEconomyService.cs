using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace Unity.Services.Economy
{
    public class PlayerEconomyService : UnityServiceBehaviour
    {
        [SerializeField] private SerializedDictionary<BalanceType, long> _balances;
        public Action<BalanceType, long> onBalanceUpdated;

        protected override string _localDataID => "currency";

        protected override void Awake()
        {
            base.Awake();
            LoadLocalData(ref _balances);
        }
        protected override async void OnSignInCompleted()
        {
            if (_balances.Count != 0) return;

            await LoadEconomyData().EconomyResponse();
            async Task LoadEconomyData()
            {
                await EconomyService.Instance.Configuration.SyncConfigurationAsync();
                var result = await EconomyService.Instance.PlayerBalances.GetBalancesAsync();

                _balances.Clear();
                foreach (var balance in result.Balances)
                {
                    if (!Enum.TryParse<BalanceType>(balance.CurrencyId, out var type)) continue;
                    _balances[type] = balance.Balance;
                    ForceUpdateBalance(type);
                }
            }
        }
        protected override void OnSignOutCompleted()
        {
            foreach (var item in _balances)
                _balances[item.Key] = 0;
        }

        public long GetBalance(BalanceType type) => _balances.ContainsKey(type) ? _balances[type] : 0;
        public void ForceUpdateBalance(BalanceType type) => onBalanceUpdated?.Invoke(type, GetBalance(type));

        public void AddBalanceID(BalanceType type, uint amount) => ModifyBalanceID(type, amount);
        public void RemoveBalanceID(BalanceType type, uint amount) => ModifyBalanceID(type, -amount);
        private void ModifyBalanceID(BalanceType type, long amount)
        {
            if (!_balances.ContainsKey(type)) return;

            _balances[type] += amount;
            onBalanceUpdated?.Invoke(type, _balances[type]);
            SaveLocalData(_balances);
        }

        public async void SaveBalanceID(BalanceType type) => await EconomyService.Instance?.PlayerBalances?
            .SetBalanceAsync(type.ToString(), GetBalance(type))
            .EconomyResponse();
        
        [ContextMenu("Save All Balances")]
        public void SaveAllBalances()
        {
            foreach (var item in _balances)
                SaveBalanceID(item.Key);
        }
    }
}