using UnityEngine;
using TMPro;

namespace Unity.Services.Economy
{
    public class BalanceUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textUI;
        [SerializeField] private BalanceType _balanceID = BalanceType.COIN;

        private PlayerEconomyService _economy;

        private void Awake() => _economy = UnityServiceInit.Instance?.GetComponent<PlayerEconomyService>();
        private void OnEnable() => _economy.onBalanceUpdated += OnUpdateUI;
        private void OnDisable() => _economy.onBalanceUpdated -= OnUpdateUI;

        private void OnUpdateUI(BalanceType type, long amount)
        {
            if (type != _balanceID) return;
            _textUI.SetText(amount.ToString());
        }
    }
}