using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Unity.Services.CloudSave
{
    public class CloudSaveEditorWindow : EditorWindow
    {
        private ScriptableObject _gameDataSO;
        private ICloudSaveGameData _gameData;

        private string _customItemId;

        private CloudCustomKey[] _availableKeys = new CloudCustomKey[0];
        private string[] _availableKeysList = new string[0];
        private int _selectedKeyIndex = -1;

        private bool _isLoading;

        [MenuItem("Services/Cloud Save/Global Save")]
        public static void Open() => GetWindow<CloudSaveEditorWindow>("Global CloudSave");

        private void OnGUI()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Game Data Source", EditorStyles.boldLabel);
            _gameDataSO = EditorGUILayout.ObjectField("ScriptableObject", _gameDataSO, typeof(ScriptableObject), false) as ScriptableObject;

            if (_gameDataSO is not ICloudSaveGameData gameData) {
                EditorGUILayout.HelpBox("Only support ICloudSaveGameData Objects", MessageType.Warning);
                return;
            }

            _gameData = gameData;
            EditorGUILayout.Space();

            using (new EditorGUI.DisabledScope(_gameData == null)) {
                _customItemId = EditorGUILayout.TextField("Custom Item ID", _customItemId);

                if (string.IsNullOrEmpty(_customItemId)) {
                    EditorGUILayout.HelpBox("Custom Item ID must not be empty", MessageType.Warning);
                    return;
                }

                EditorGUILayout.Space();
                if (GUILayout.Button("Refresh Item Keys")) _ = RefreshKeys();
            }

            EditorGUILayout.Space();
            DrawKeysSection();

            EditorGUILayout.Space();
            DrawApplyButton();
        }

        private void DrawKeysSection()
        {
            EditorGUILayout.LabelField("Existing Item Keys", EditorStyles.boldLabel);

            if (_isLoading) {
                EditorGUILayout.LabelField("Loading...");
                return;
            }

            if (_availableKeys.Length == 0) {
                EditorGUILayout.HelpBox("No available keys in Custom Item ID", MessageType.Info);
                return;
            }

            _selectedKeyIndex = EditorGUILayout.Popup("Target Key", _selectedKeyIndex, _availableKeysList);
        }
        private void DrawApplyButton()
        {
            if (_selectedKeyIndex < 0 || _gameData == null) return;
            if (GUILayout.Button("Overwrite Game Data"))
                _ = ApplySelectedKey();
        }

        private async Task RefreshKeys()
        {
            _isLoading = true;
            Repaint();

            try {
                _availableKeys = await CloudSaveExtension.GetAllKeysAsync(_customItemId);
                _availableKeysList = _availableKeys.KeyList();
                _selectedKeyIndex = _availableKeysList.Any() ? 0 : -1;
            }
            catch (Exception e) {
                Debug.LogError(e);
            }

            _isLoading = false;
            Repaint();
        }
        private async Task ApplySelectedKey()
        {
            var customKey = _availableKeys[_selectedKeyIndex];
            var json = _gameData.ItemsListToJson();

            if (!EditorUtility.DisplayDialog("Confirm Overwrite", $"Are you sure you want to overwrite:\n\n{customKey.key}?", "yes", "cancel"))
                return;

            try {
                await CloudSaveExtension.SetAsync(_customItemId, customKey, json);
                Debug.Log($"CloudSave udpated: {customKey.key}");
            }
            catch (Exception e) {
                Debug.LogError(e);
            }
        }
    }
}