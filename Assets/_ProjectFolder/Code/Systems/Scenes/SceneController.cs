using UnityEngine;

namespace Unity.SceneManagement
{
    public class SceneController : SingletonBasic<SceneController>
    {
        [SerializeField] private SceneFadeEffect _fadePrefab;

        private bool _isLoadingScene;

        protected override void Awake()
        {
            base.Awake();
            Time.timeScale = 1f;
            Instantiate(_fadePrefab, transform);
        }
        public void ChangeScene(string scenePath)
        {
            if (_isLoadingScene) return;

            Instantiate(_fadePrefab, transform).ScenePath = scenePath;
            _isLoadingScene = true;
        }
    }
}