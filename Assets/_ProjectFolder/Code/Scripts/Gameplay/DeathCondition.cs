using UnityEngine;
using UnityEngine.Animations;
using Unity.Cinemachine;

namespace Gameplay
{
    public class DeathCondition : MonoBehaviour
    {
        [SerializeField] private CinemachineShake _cameraEffect;
        [SerializeField] private TweenCanvasGroup _hitEffect;
        [SerializeField] private string _tag = "Finish";

        [SerializeField] private Behaviour[] _components;
        private AnimatorEvents _animator;
        private bool _isDeath;

        private void Awake() => _animator = GetComponent<AnimatorEvents>();
        private void OnTriggerEnter2D(Collider2D collision) => Trigger(collision);
        private void OnCollisionEnter2D(Collision2D collision) => Trigger(collision.collider);

        private void Trigger(Collider2D collider)
        {
            if (collider.CompareTag(_tag))
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
            GameplayManager.Instance.Disable();
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