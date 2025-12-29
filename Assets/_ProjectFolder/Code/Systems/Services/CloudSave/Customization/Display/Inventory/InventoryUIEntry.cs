namespace Unity.Customization.Inventory
{
    public class InventoryUIEntry : ItemsDisplayEntry
    {
        private InventoryUI _manager;

        protected override void Awake()
        {
            base.Awake();
            _manager = GetComponentInParent<InventoryUI>();
        }
        protected override void OnClickHandler() => _manager.SelectItem(_item);
    }
}