using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Services.CloudSave
{
    using Customization;

    public class PlayerDataService : PlayerServiceBehaviour
    {
        [SerializeField] private CustomizationData _customization;
        
        public override string DataID => "customization";

        public CustomizationData Customization => _customization;
        public Action onDataUpdated;

        protected override void Awake()
        {
            base.Awake();
            LoadLocalData(ref _customization);
        }
        protected override async void OnSignInCompleted()
        {
            await LoadInventoryData().CloudCodeResponse();

            async Task LoadInventoryData()
            {
                var result = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { DataID });

                if (!result.TryGetValue(DataID, out var entry)) return;
                var cloudData = entry.Value.GetAs<CustomizationDataCloud>();

                if (_customization.Equals(cloudData)) return;
                _customization = new(cloudData);
                onDataUpdated?.Invoke();
            }
        }

        public async Task SaveObjectData(string key, object @cloud, object @local)
        {
            SaveLocalData(@local);
            var payload = new Dictionary<string, object> { { key, @cloud } };
            await CloudSaveService.Instance.Data.Player.SaveAsync(payload).CloudCodeResponse();
        }
    }
}