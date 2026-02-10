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

        [Header("Discount Section")]
        [SerializeField] private GameObject _discountBlock;
        [SerializeField] private TextMeshProUGUI _discount;

        [Space]
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
            _cost?.SetText($"\U0001F600 {_item.Cost}");

            _discountBlock?.SetActive(item.HasDiscount);
            if (_discountBlock.activeSelf) _discount?.SetText($"{(int)(item.Discount * 100)}%");
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