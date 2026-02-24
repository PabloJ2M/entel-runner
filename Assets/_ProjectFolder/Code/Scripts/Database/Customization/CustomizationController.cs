using System;
using UnityEngine;

namespace Unity.Customization
{
    using Services.CloudSave;
    using Services.CloudSave.Models;

    public class CustomizationController : SaveLocalBehaviour
    {
        [SerializeField] private CustomizationData _customization;

        protected override string _localDataID => "customization";
        private PlayerCloudSaveService _cloudSave = null;

        public CustomizationData Local => _customization;
        public Action onCustomizationUpdated;

        private void Awake() => _cloudSave = GetComponentInParent<PlayerCloudSaveService>();
        private void Start()
        {
            LoadLocalData(ref _customization);

            if (_customization.unlocked.Count > 0)
                onCustomizationUpdated?.Invoke();
        }
        private void OnEnable()
        {
            _cloudSave.onCloudSaveUpdated += OnCloudSaveUpdated;
            _cloudSave.onCloudSaveClear += OnCloudSaveClear;
        }
        private void OnDisable()
        {
            _cloudSave.onCloudSaveUpdated -= OnCloudSaveUpdated;
            _cloudSave.onCloudSaveClear -= OnCloudSaveClear;
        }

        private void OnCloudSaveUpdated(string key, Item item)
        {
            if (key != _localDataID) return;
            var data = item.Value.GetAs<CustomizationData>();

            _customization = data;
            onCustomizationUpdated?.Invoke();
            SaveLocalData(_customization);
        }
        private void OnCloudSaveClear()
        {
            _customization.selectedLibrary = string.Empty;
            _customization.equipped.Clear();
            _customization.unlocked.Clear();
            DeleteLocalData();

            onCustomizationUpdated?.Invoke();
        }

        [ContextMenu("Save Data")]
        public async void SaveData()
        {
            SaveDataLocal();
            await _cloudSave.SaveObjectData(_localDataID, _customization);
        }
        public void SaveDataLocal() => SaveLocalData(_customization);
    }
}