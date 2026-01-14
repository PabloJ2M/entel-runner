using UnityEngine;
using UnityEngine.UI;

namespace Unity.Customization
{
    public class QualityTab : EnumToggleTab<ItemQuality>
    {
        [SerializeField] private GameObject _icon;

        protected void Start()
        {
            for (int i = 0; i < (int)_value; i++)
                Instantiate(_icon, transform);
        }
        protected override void OnClick(bool isOn)
        {
            if (isOn)
                _group.UpdateType(_value);
            else
                _group.CheckToggleStatus();
        }
    }
}