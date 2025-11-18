using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.SceneManagement
{
    [RequireComponent(typeof(CanvasGroup))]
    public class SceneFadeEffect : MonoBehaviour
    {
        [SerializeField, Range(0, 10)] private float _speed = 1f;

        private CanvasGroup _canvasGroup;
        private sbyte _fadeDirection;

        public string ScenePath { private get; set; }

        private void Awake() => _canvasGroup = GetComponent<CanvasGroup>();

        private void Start()
        {
            bool isLoadingScene = string.IsNullOrEmpty(ScenePath);
            _canvasGroup.alpha = isLoadingScene ? 0f : 1f;
            _fadeDirection = (sbyte)(isLoadingScene ? 1 : -1);
        }
        private void Update()
        {
            _canvasGroup.alpha += Time.unscaledDeltaTime * _speed * _fadeDirection;

            switch(_canvasGroup.alpha)
            {
                case 1f: SceneManager.LoadSceneAsync(ScenePath); break;
                case 0f: Destroy(gameObject); break;
            };
        }
    }
}