using UnityEngine;
using TMPro;

namespace Unity.Services.Economy
{
    public class BalanceUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textUI;
        [SerializeField] private string _balanceID = "COIN";

        private PlayerEconomyService _economy;

        private void Awake() => _economy = UnityServiceInit.Instance.GetComponent<PlayerEconomyService>();
        private void OnEnable() => _economy.onBalanceUpdated += OnUpdateUI;
        private void OnDisable() => _economy.onBalanceUpdated -= OnUpdateUI;

        private void OnUpdateUI(string id, long amount)
        {
            if (id != _balanceID) return;
            _textUI.SetText(amount.ToString());
        }
    }
}