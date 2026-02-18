using UnityEngine;
using UnityEngine.Animations;
using Unity.Cinemachine;

namespace Gameplay
{
    public class DeathCondition : MonoBehaviour
    {
        [SerializeField] private CinemachineShake _cameraEffect;
        [SerializeField] private TweenCanvasGroup _hitEffect;

        [SerializeField] private Behaviour[] _components;
        private AnimatorEvents _animator;
        private bool _isDeath;
        
        private const string _tag = "Finish";

        private void Awake() => _animator = GetComponent<AnimatorEvents>();
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(_tag))
                Disable();
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.collider.CompareTag(_tag)) return;
            if (Vector2.Dot(collision.contacts[0].normal, Vector2.left) > 0.75f)
                Disable();
        }

        public void Enable()
        {
            _isDeath = false;
            foreach (var component in _components) component.enabled = true;
        }
        public void Disable()
        {
            if (_isDeath) return;
            foreach (var component in _components) component.enabled = false;

            Invoke(nameof(NormalColor), 0.2f);
            GameplayManager.Instance.Stop();
            Time.timeScale = 0.4f;

            _animator?.TriggerDeath();
            _cameraEffect?.Shake();
            _hitEffect?.FadeIn();
            _isDeath = true;
        }
        private void NormalColor()
        {
            Time.timeScale = 1f;
        }
    }
}