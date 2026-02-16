using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.SceneManagement
{
    public class SceneController : SingletonBasic<SceneController>
    {
        [SerializeField] private SceneFadeEffect _fadePrefab;

        private HashSet<string> _loadedScenes = new();
        private bool _isLoadingScene;

        private void Start()
        {
            Time.timeScale = 1f;
            Instantiate(_fadePrefab, transform);
        }

        public void AddScene(string scenePath)
        {
            if (!_loadedScenes.Add(scenePath)) return;
            SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
        }
        public void RemoveScene(string scenePath)
        {
            if (!_loadedScenes.Remove(scenePath)) return;
            SceneManager.UnloadSceneAsync(scenePath);
        }
        public void ChangeScene(string scenePath)
        {
            if (_isLoadingScene) return;

            Instantiate(_fadePrefab, transform).ScenePath = scenePath;
            _isLoadingScene = true;
        }
    }
}