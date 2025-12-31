using System.Linq;

namespace Unity.Customization.Store
{
    using Services;
    using Services.Economy;

    public class StoreUI : ItemsDisplayBehaviour
    {
        private PlayerEconomyService _economy;
        private const string _balanceID = "COIN";

        protected override void OnEnable()
        {
            base.OnEnable();
            _economy = UnityServiceInit.Instance.GetComponent<PlayerEconomyService>();
            _economy?.ForceUpdateBalance(_balanceID);
        }
        protected override void OnUpdateLibrary(LibraryReference reference) { }
        protected override void OnUpdateCategory(string category)
        {
            var items = _itemList.GetItems(category).OrderByDescending(item => item.Cost);
            var unlocked = _playerData.Customization.unlocked;
            
            ClearPoolInstance();
            if (items.Count() == 0) return;

            foreach (var item in items)
            {
                if (item.Cost == 0) continue;
                var entry = Pool.Get() as StoreUI_Entry;
                entry.Init(item, unlocked.Contains(item.ID));
            }
        }

        public bool BuyItem(SO_Item item)
        {
            if (_economy.GetBalance(_balanceID) < item.Cost) return false;

            _economy.RemoveBalanceID(_balanceID, item.Cost);
            _playerData.Customization.unlocked.Add(item.ID);
            return true;
        }
    }
}