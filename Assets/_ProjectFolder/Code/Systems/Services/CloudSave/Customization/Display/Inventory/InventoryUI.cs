using System.Linq;
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

            var unlocked = _playerData.Customization.unlocked;
            var group = _groups[category].OrderByDescending(x => x.Value.Cost);
            foreach (var item in group)
            {
                if (item.Value.Cost != 0 && !unlocked.Contains(item.Key)) continue;
                var entry = Pool.Get() as ItemsDisplayEntry;
                entry.Init(item.Value);
            }
        }

        public void SelectItem(SO_Item item)
        {
            var equipped = _playerData.Customization.equipped;

            if (!equipped.ContainsKey(_libraryID)) equipped.Add(_libraryID, new());
            if (!equipped[_libraryID].ContainsKey(item.Category)) equipped[_libraryID].Add(item.Category, item.ID);

            //if (equipped[_libraryID][item.Category] == item.ID) return;
            equipped[_libraryID][item.Category] = item.ID;
            _library.UpdatePreview();
        }
    }
}