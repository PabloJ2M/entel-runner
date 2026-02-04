using System;
using System.Linq;
using System.Collections;
using UnityEngine;

namespace Unity.Customization.Store
{
    using Services;
    using Services.Economy;

    public class StoreUI : ItemsDisplayBehaviour
    {
        [SerializeField] private TaskConfirmation _triggerAction;
        private PlayerEconomyService _economy;

        protected override void OnEnable()
        {
            base.OnEnable();
            _economy = UnityServiceInit.Instance?.GetComponent<PlayerEconomyService>();
            _economy?.ForceUpdateBalance(BalanceType.COIN);
        }
        protected override void OnUpdateGroup(ItemGroup group)
        {
            _items = _itemList?.GetItemsByGroup(group.ToString());
            base.OnUpdateGroup(group);
        }
        protected override void DisplayItems()
        {
            var items = GetItemsFiltered().OrderByDescending(item => item.Type).ThenByDescending(item => item.Cost);
            var unlocked = _customization.Local.unlocked;

            ClearPoolInstance();

            foreach (var item in items)
            {
                if (item.Cost == 0) continue;
                var entry = Pool.Get() as StoreUI_Entry;
                entry.Init(item, unlocked.ExistPath(item.Reference.ID, item.Group, item.ID));
            }
        }

        public IEnumerator BuyItem(SO_Item item, Action<bool> result)
        {
            if (_economy.GetBalance(item.Balance) < item.Cost) yield break;
            yield return _triggerAction.DisplayTask(() => UnlockedItem(item));
        }
        private void UnlockedItem(SO_Item item)
        {
            _economy.RemoveBalanceID(item.Balance, item.Cost);
            _customization.Local.unlocked.CreatePath(item.Reference.ID, item.Group, item.ID);
            _customization.SaveDataLocal();
            DisplayItems();
        }

        public void SaveBalances() => _economy?.SaveAllBalances();
    }
}