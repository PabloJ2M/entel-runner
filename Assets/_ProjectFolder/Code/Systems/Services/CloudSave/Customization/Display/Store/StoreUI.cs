using UnityEngine;

namespace Unity.Customization.Store
{
    using Services.Economy;

    public class StoreUI : ItemsDisplayBehaviour
    {
        private PlayerEconomyService _economy;

        protected override void Awake()
        {
            base.Awake();
            _economy = FindFirstObjectByType<PlayerEconomyService>(FindObjectsInactive.Include);
        }
        protected override void OnUpdateLibrary(LibraryReference reference)
        {
            base.OnUpdateLibrary(reference);

            var items = _itemList.GetItems(reference, _tabs.Category);
            var unlocked = _playerData.Customization.unlocked;

            foreach (var item in items)
            {
                if (item.Cost == 0) continue;
                var entry = Pool.Get() as StoreUIEntry;
                entry.Init(item, unlocked.Contains(item.ID));
            }
        }

        public bool BuyItem(SO_Item item)
        {
            if (_economy.GetBalance("COIN") < item.Cost) return false;
            _economy.RemoveBalanceID("COIN", item.Cost);

            _playerData.Customization.unlocked.Add(item.ID);
            _library.UpdateLibraryReference();
            return true;
        }
    }
}