using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace Unity.Services.Economy
{
    public class PlayerEconomyService : PlayerServiceBehaviour
    {
        [SerializeField] private SerializedDictionary<string, long> _balances;
        
        public override string DataID => "currency";

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
                    _balances[balance.CurrencyId] = balance.Balance;
            }
        }
        protected override void OnSignOutCompleted()
        {
            foreach (var item in _balances)
                _balances[item.Key] = 0;
        }

        public long GetBalance(string id) => _balances.ContainsKey(id) ? _balances[id] : 0;
        public async void AddBalanceID(string id, uint amount)
        {
            _balances[id] += amount;
            SaveLocalData(_balances);
            await EconomyService.Instance.PlayerBalances.IncrementBalanceAsync(id, (int)amount).EconomyResponse();
        }
        public async void RemoveBalanceID(string id, uint amount)
        {
            _balances[id] -= amount;
            SaveLocalData(_balances);
            await EconomyService.Instance.PlayerBalances.DecrementBalanceAsync(id, (int)amount).EconomyResponse();
        }
    }
}