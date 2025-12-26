using UnityEngine;
using UnityEngine.UI;

namespace Unity.Customization.Store
{
    using Pool;
    using Services;
    using Services.Economy;
    using Services.CloudSave;

    [RequireComponent(typeof(ScrollRect))]
    public class StoreController : PoolObjectSingle<StoreItem>
    {
        [Header("Controller")]
        [SerializeField] private SpriteResolverController _character;
        [SerializeField] private SO_ItemList _itemList;

        private PlayerEconomyService _economy;
        private PlayerDataService _playerData;

        protected override void Awake()
        {
            base.Awake();
            var manager = UnityServiceInit.Instance;
            _economy = manager.GetComponent<PlayerEconomyService>();
            _playerData = manager.GetComponent<PlayerDataService>();
        }
        protected override void Reset() => _parent = GetComponent<ScrollRect>().content;
        private void OnEnable() => _playerData.onDataUpdated += Start;
        private void OnDisable() => _playerData.onDataUpdated -= Start;

        private void Start()
        {
            var categories = _itemList.GetItems(_character.Categories);
            Clear();

            foreach (var item in categories)
            {
                var entry = Pool.Get() as StoreItem;
                entry.Init(item, _playerData ? _playerData.Customization.unlocked.Contains(item.ID) : false);
            }
        }
        protected override void OnGet(PoolObjectBehaviour @object)
        {
            base.OnGet(@object);
            @object.Transform.SetAsLastSibling();
        }

        public bool BuyItem(SO_Item item)
        {
            if (_economy.GetBalance("COIN") < item.Cost) return false;
            _playerData.Customization.unlocked.Add(item.ID);
            _economy.RemoveBalanceID("COIN", item.Cost);
            return true;
        }
        public void SelectItem(SO_Item item)
        {
            _character.SetLabel(item.Category, item.Label);
            _playerData.Customization.equipped[item.Category] = item.ID;
        }

        [ContextMenu("Save")]
        public async void SaveData()
        {
            var cloud = new CustomizationDataCloud(_playerData.Customization);
            await _playerData.SaveObjectData(_playerData.DataID, cloud, _playerData.Customization);
        }
    }
}