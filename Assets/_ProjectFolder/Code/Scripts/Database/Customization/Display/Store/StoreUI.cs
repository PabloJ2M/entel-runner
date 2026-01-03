using System.Linq;
using System.Collections.Generic;

namespace Unity.Customization.Store
{
    using Services;
    using Services.Economy;

    public class StoreUI : ItemsDisplayBehaviour
    {
        private PlayerEconomyService _economy;
        private const BalanceType _balance = BalanceType.COIN;

        protected override void OnEnable()
        {
            base.OnEnable();
            _economy = UnityServiceInit.Instance.GetComponent<PlayerEconomyService>();
            _economy?.ForceUpdateBalance(_balance);
        }
        protected override void OnUpdateCategory(string category)
        {
            var items = _itemList.GetItems(category).OrderByDescending(item => item.Cost);
            var unlocked = _customization.Local.unlocked;

            ClearPoolInstance();
            if (items.Count() == 0) return;

            foreach (var item in items)
            {
                if (item.Cost == 0) continue;
                var entry = Pool.Get() as StoreUI_Entry;
                entry.Init(item, unlocked.ExistPath(item.Reference.ID, category, item.ID));
            }
        }

        public bool BuyItem(SO_Item item)
        {
            if (_economy.GetBalance(_balance) < item.Cost) return false;

            _customization.Local.unlocked.CreatePath(item.Reference.ID, item.Category, item.ID);
            _economy.RemoveBalanceID(_balance, item.Cost);
            return true;
        }
    }
}