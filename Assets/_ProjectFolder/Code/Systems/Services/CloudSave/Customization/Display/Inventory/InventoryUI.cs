using System.Collections.Generic;

namespace Unity.Customization.Inventory
{
    public class InventoryUI : ItemsDisplayBehaviour
    {
        private IDictionary<string, Dictionary<string, SO_Item>> _groups;
        private string _libraryID;

        protected override void OnUpdateLibrary(LibraryReference reference)
        {
            _libraryID = reference.ID;
            _groups = _itemList.GetItemsLibrary(_libraryID);
        }
        protected override void OnUpdateCategory(string category)
        {
            ClearPoolInstance();
            if (_groups == null) return;
            if (!_groups.ContainsKey(category)) return;

            foreach (var item in _groups[category])
            {
                if (!_playerData.Customization.unlocked.Contains(item.Key)) continue;
                var entry = Pool.Get() as ItemsDisplayEntry;
                entry.Init(item.Value);
            }
        }

        public void SelectItem(SO_Item item)
        {
            var data = _playerData.Customization;

            if (data.equipped[_libraryID][item.Category] == item.ID) return;
            data.equipped[_libraryID][item.Category] = item.ID;
            _library.UpdatePreview();
        }
    }
}