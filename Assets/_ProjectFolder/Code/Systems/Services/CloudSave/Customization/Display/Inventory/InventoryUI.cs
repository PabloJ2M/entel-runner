namespace Unity.Customization.Inventory
{
    public class InventoryUI : ItemsDisplayBehaviour
    {
        protected override void OnUpdateLibrary(LibraryReference reference)
        {
            base.OnUpdateLibrary(reference);
            var items = _itemList.GetItems(reference, _tabs.Category, _playerData.Customization.unlocked);

            foreach (var item in items)
            {
                var entry = Pool.Get() as ItemsDisplayEntry;
                entry.Init(item);
            }
        }
        public void SelectItem(SO_Item item)
        {
            var data = _playerData.Customization;

            if (data.equipped[_selected.ID][item.Category] == item.ID) return;
            data.equipped[_selected.ID][item.Category] = item.ID;
            _library.UpdateLibraryReference();
        }
    }
}