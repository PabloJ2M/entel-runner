using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorEvents : MonoBehaviour
    {
        private Animator _animator;

        private const string _speed = "Speed", _gravity = "Gravity";
        private const string _isGrounded = "IsGrounded";
        private const string _death = "Death";

        private void Awake() => _animator = GetComponent<Animator>();
        private void Start() => SetSpeed(1f);

        public void SetSpeed(float amount) => SetFloat(_speed, amount);
        public void SetGravity(float amount) => SetFloat(_gravity, amount);
        public void SetGroundCheck(bool value) => _animator?.SetBool(_isGrounded, value);
        public void TriggerDeath()
        {
            _animator?.SetTrigger(_death);
            SetSpeed(0f);
        }

        private void SetFloat(string name, float value) => _animator?.SetFloat(name, value);
    }
}