using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Unity.Customization.Store
{
    [RequireComponent(typeof(Button))]
    public class StoreUI_Entry : ItemsDisplayEntry
    {
        [Header("Cost Section")]
        [SerializeField] private TextMeshProUGUI _cost;
        [SerializeField] private GameObject _discountMark;
        [SerializeField] private GameObject _lockScreen;

        private StoreUI _manager;
        private bool _hasPurchased;

        protected override void Awake()
        {
            base.Awake();
            _manager = GetComponentInParent<StoreUI>();
        }

        public override void Init(SO_Item item)
        {
            base.Init(item);
            _cost?.SetText(_item.Cost.ToString());
            _discountMark.SetActive(item.HasDiscount);
        }
        public void Init(SO_Item item, bool hasPurchased)
        {
            Init(item);
            _hasPurchased = hasPurchased || _item.Cost == 0;
            _lockScreen.SetActive(_hasPurchased);
        }

        protected override void OnClickHandler()
        {
            if (_hasPurchased) return;
            StartCoroutine(_manager.BuyItem(_item, OnResult));
        }
        private void OnResult(bool result)
        {
            if (!result) return;
            _lockScreen.SetActive(true);
            _hasPurchased = true;
        }
    }
}