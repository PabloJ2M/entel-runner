using System.Linq;
using System.Collections.Generic;

namespace Unity.Customization.Inventory
{
    public class InventoryUI : ItemsDisplayBehaviour
    {
        private IDictionary<string, Dictionary<string, SO_Item>> _groups;

        protected override void OnUpdateLibrary(SO_LibraryReference reference)
        {
            base.OnUpdateLibrary(reference);
            _groups = _itemList.GetItemsLibrary(_libraryID);
        }
        protected override void OnUpdateCategory(string category)
        {
            ClearPoolInstance();

            if (_groups == null) return;
            if (!_groups.ContainsKey(category)) return;

            var unlocked = _customization.Local.unlocked;
            var group = _groups[category].OrderByDescending(x => x.Value.Cost);
            
            foreach (var item in group)
            {
                if (item.Value.Cost != 0 && !unlocked.ExistPath(_libraryID, category, item.Key)) continue;
                var entry = Pool.Get() as ItemsDisplayEntry;
                entry.Init(item.Value);
            }
        }

        public void SelectItem(SO_Item item)
        {
            var equipped = _customization.Local.equipped;
            equipped.CreatePath(_libraryID, item.Category, item.ID);
            _library.UpdatePreview();
        }
    }
}