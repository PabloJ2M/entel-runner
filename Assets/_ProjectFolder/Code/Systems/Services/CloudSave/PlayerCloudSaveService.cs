using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Unity.Services.CloudSave
{
    using Models;
    using UnityEngine;

    public class PlayerCloudSaveService : UnityServiceBehaviour
    {
        protected override string _localDataID => "inventory";
        private bool _hasLoadedData;

        public event Action<string, Item> onCloudSaveUpdated;
        public event Action onCloudSaveClear;

        protected override void Awake()
        {
            base.Awake();
            LoadLocalData(ref _hasLoadedData);
        }
        protected override void OnSignOutCompleted()
        {
            _hasLoadedData = false;
            SaveLocalData(_hasLoadedData);
            onCloudSaveClear?.Invoke();
        }
        protected override async void OnSignInCompleted()
        {
            if (!_hasLoadedData)
                await LoadObjectData();
        }

        public async Awaitable LoadObjectData()
        {
            await LoadPlayerData().CloudCodeResponse();

            async Task LoadPlayerData()
            {
                var result = await CloudSaveService.Instance.Data.Player.LoadAllAsync();
                foreach (var item in result) onCloudSaveUpdated?.Invoke(item.Key, item.Value);

                _hasLoadedData = true;
                SaveLocalData(_hasLoadedData);
            }
        }
        public async Awaitable SaveObjectData(string key, object @cloud)
        {
            var payload = new Dictionary<string, object> { { key, @cloud } };
            await CloudSaveService.Instance?.Data?.Player?.SaveAsync(payload).CloudCodeResponse();
        }
    }
}