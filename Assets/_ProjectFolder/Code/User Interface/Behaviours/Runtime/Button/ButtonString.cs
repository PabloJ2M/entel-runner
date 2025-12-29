using TMPro;

namespace UnityEngine.UI
{
    public class ButtonString : Button
    {
        [SerializeField] private TextMeshProUGUI _textUI;
        [SerializeField] private int _stringIndex;
        [SerializeField] private string _textPrefix;

        private string _defaultText;

        protected override void Awake() { base.Awake(); _defaultText = _textUI?.text; }
        protected override void Start() { base.Start(); UpdateText(); }

        protected void UpdateText() => _textUI?.SetText(string.Format(_defaultText, _textPrefix));
        public void SetText(string value)
        {
            _defaultText = value.Insert(Mathf.Clamp(_stringIndex, 0, value.Length), " {0} ");
            UpdateText();
        }
    }
}