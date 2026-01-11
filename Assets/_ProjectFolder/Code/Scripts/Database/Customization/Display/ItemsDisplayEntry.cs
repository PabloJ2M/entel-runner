using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Unity.Customization
{
    using Pool;

    public abstract class ItemsDisplayEntry : PoolObjectBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _name;
        protected SO_Item _item;

        protected virtual void Awake() => GetComponent<Button>().onClick.AddListener(OnClickHandler);

        public virtual void Init(SO_Item item)
        {
            _item = item;
            _icon?.SetSprite(_item.Preview);
            _name?.SetText(_item.LabelName);
        }
        protected abstract void OnClickHandler();
    }
}