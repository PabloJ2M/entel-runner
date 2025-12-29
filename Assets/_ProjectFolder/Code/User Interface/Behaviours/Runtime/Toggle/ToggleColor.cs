namespace UnityEngine.UI
{
    public class ToggleColor : ToggleBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Color _onColor, _offColor;

        protected override void OnUpdateValue(bool isOn)
        {
            if (!_icon) return;
            _icon.color = isOn ? _onColor : _offColor;
        }
    }
}