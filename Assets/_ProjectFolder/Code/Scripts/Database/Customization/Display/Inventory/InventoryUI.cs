using System.Linq;
using System.Collections.Generic;

namespace Unity.Customization.Inventory
{
    public class InventoryUI : ItemsDisplayBehaviour
    {
        private string _libraryID;

        protected override void OnUpdateLibrary(SO_LibraryReference reference)
        {
            _libraryID = reference.ID;
        }
        protected override void OnUpdateCategory(string category)
        {
            _items = _itemList.GetItemsByLibrary(_libraryID, category);
            base.OnUpdateCategory(category);
        }
        protected override void DisplayItems()
        {
            var items = GetItemsFiltered().OrderByDescending(item => item.Type).ThenByDescending(item => item.Cost);
            var unlocked = _customization.Local.unlocked;
            
            ClearPoolInstance();

            foreach (var item in items)
            {
                if (item.Cost != 0 && !unlocked.ExistPath(_libraryID, item.Category, item.ID)) continue;
                var entry = Pool.Get() as ItemsDisplayEntry;
                entry.Init(item);
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