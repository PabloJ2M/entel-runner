namespace UnityEngine.UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Image _healthBar, _effectBar;
        [SerializeField] private Health _reference;

        private void Update() => _effectBar.fillAmount = Mathf.MoveTowards(_effectBar.fillAmount, _healthBar.fillAmount, Time.deltaTime);
        private void OnEnable() => _reference.onHealthUpdated += UpdateHealth;
        private void OnDisable() => _reference.onHealthUpdated -= UpdateHealth;
        private void UpdateHealth(float value) => _healthBar.fillAmount = value;
    }
}