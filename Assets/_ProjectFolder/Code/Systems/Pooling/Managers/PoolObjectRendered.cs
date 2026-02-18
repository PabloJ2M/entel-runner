using UnityEngine;

namespace Unity.Pool
{
    using SceneManagement;

    public class PoolObjectRendered : MonoBehaviour
    {
        [SerializeField] private PoolObjectOnSpline _object;
        private bool _isVisible;

        private void Reset() => _object = GetComponentInParent<PoolObjectOnSpline>();
        private void Awake() => _object.onStatusChanged += OnStatusChanged;
        private void OnDestroy() => _object.onStatusChanged -= OnStatusChanged;

        private void OnStatusChanged(bool value) => _isVisible = false;
        private void OnBecameVisible() => _isVisible = true;

        #if !UNITY_EDITOR
        private void OnBecameInvisible()
        {
            if (!_isVisible || SceneController.Instance.IsLoadingScene) return;
            _object.Destroy();
        }
        #endif
    }
}