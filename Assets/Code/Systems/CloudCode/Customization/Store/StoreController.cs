using UnityEngine;
using UnityEngine.UI;

namespace Unity.Customization.Store
{
    using Pool;

    [RequireComponent(typeof(ScrollRect))]
    public class StoreController : PoolObjectSingle<StoreItem>
    {
        [Header("Controller")]
        [SerializeField] private SpriteResolverController _character;
        [SerializeField] private SO_ItemList _itemList;

        protected override void Reset() => _parent = GetComponent<ScrollRect>().content;
        private void Start()
        {
            var categories = _itemList.GetItems(_character.Categories);

            foreach (var item in categories)
            {
                var entry = Pool.Get() as StoreItem;
                entry.Init(item, false);
            }
        }

        public void BuyItem(SO_Item item)
        {

        }
        public void SelectItem(SO_Item item)
        {
            _character.SetLabel(item.Category, item.Label);
        }
    }
}