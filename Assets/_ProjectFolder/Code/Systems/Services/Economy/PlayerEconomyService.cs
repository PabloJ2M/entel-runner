using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace Unity.Services.Economy
{
    public class PlayerEconomyService : PlayerServiceBehaviour
    {
        [SerializeField] private SerializedDictionary<BalanceType, long> _balances;
        public Action<BalanceType, long> onBalanceUpdated;

        protected override string _dataID => "currency";

        protected override void Awake()
        {
            base.Awake();
            LoadLocalData(ref _balances);
        }
        protected override async void OnSignInCompleted()
        {
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
        public async void AddBalanceID(BalanceType type, uint amount)
        {
            ModifyBalanceID(type, amount);
            await EconomyService.Instance.PlayerBalances.IncrementBalanceAsync(type.ToString(), (int)amount).EconomyResponse();
        }
        public async void RemoveBalanceID(BalanceType type, uint amount)
        {
            ModifyBalanceID(type, -amount);
            await EconomyService.Instance.PlayerBalances.DecrementBalanceAsync(type.ToString(), (int)amount).EconomyResponse();
        }

        private void ModifyBalanceID(BalanceType type, long amount)
        {
            if (!_balances.ContainsKey(type)) return;

            _balances[type] += amount;
            onBalanceUpdated?.Invoke(type, _balances[type]);
            SaveLocalData(_balances);
        }
    }
}