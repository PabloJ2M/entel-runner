using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Unity.Customization.Store
{
    using Pool;

    [RequireComponent(typeof(Button))]
    public class StoreItem : PoolObjectBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _name;

        [Header("Cost Section")]
        [SerializeField] private GameObject _costContainer;
        [SerializeField] private TextMeshProUGUI _cost;

        private SO_Item _item;
        private bool _hasPurchased;

        private void Awake() => GetComponent<Button>().onClick.AddListener(OnClickHandler);

        public void Init(SO_Item item, bool hasPurchased)
        {
            _item = item;
            _hasPurchased = hasPurchased || _item.Price == 0;
            
            _icon?.SetSprite(_item.Sprite);
            _name?.SetText(_item.Label);

            _costContainer.SetActive(!_hasPurchased);
            if (_costContainer.activeSelf) _cost?.SetText(_item.Price.ToString());
        }
        private void OnClickHandler()
        {
            var controller = GetComponentInParent<StoreController>();

            if (_hasPurchased) { controller.SelectItem(_item); return; }
            
            _hasPurchased = true;
            _costContainer.SetActive(false);
            controller.BuyItem(_item);
        }
    }
}