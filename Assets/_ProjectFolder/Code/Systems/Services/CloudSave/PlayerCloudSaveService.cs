using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Unity.Services.CloudSave
{
    using Models;

    public class PlayerCloudSaveService : PlayerServiceBehaviour
    {
        protected override string _dataID => "inventory";
        private bool _hasLoadedData;

        public Action<string, Item> onCloudSaveUpdated;
        public Action onCloudSaveClear;

        protected override void Awake()
        {
            base.Awake();
            LoadLocalData(ref _hasLoadedData);
        }
        protected override void OnSignOutCompleted()
        {
            SaveLocalData(_hasLoadedData = false);
            onCloudSaveClear?.Invoke();
        }
        protected override async void OnSignInCompleted()
        {
            if (!_hasLoadedData)
                await LoadObjectData();
        }

        public async Task LoadObjectData()
        {
            await LoadPlayerData().CloudCodeResponse();

            async Task LoadPlayerData()
            {
                var result = await CloudSaveService.Instance.Data.Player.LoadAllAsync();
                foreach (var item in result) onCloudSaveUpdated?.Invoke(item.Key, item.Value);
                SaveLocalData(_hasLoadedData = true);
            }
        }
        public async Task SaveObjectData(string key, object @cloud)
        {
            var payload = new Dictionary<string, object> { { key, @cloud } };
            await CloudSaveService.Instance.Data.Player.SaveAsync(payload).CloudCodeResponse();
        }
    }
}