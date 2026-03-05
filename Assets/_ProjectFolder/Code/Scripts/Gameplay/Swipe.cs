using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Movement
{
    public class Swipe : MonoBehaviour
    {
        [SerializeField] private Jump _jump;
        [SerializeField] private InputActionReference _deltaInput;
        [SerializeField] private float _swipeThreshold = 50f;
        [SerializeField] private float _swipeLockAfterJump = 0.08f;

        [Header("Atributes")]
        [SerializeField] private float _swipeForce = 25f;

        private float _swipeLockTimer;
        private Rigidbody2D _rigidbody;

        private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();
        private void OnEnable()
        {
            _deltaInput.action.performed += OnSwipe;
            _jump.onJump += OnImpulse;
        }
        private void OnDisable()
        {
            _deltaInput.action.performed -= OnSwipe;
            _jump.onJump -= OnImpulse;
        }
        private void FixedUpdate()
        {
            if (_swipeLockTimer > 0)
                _swipeLockTimer -= Time.fixedDeltaTime;
        }

        private void OnImpulse()
        {
            _swipeLockTimer = _swipeLockAfterJump;
        }
        private void OnSwipe(InputAction.CallbackContext ctx)
        {
            if (_jump.IsGrounded || _swipeLockTimer > 0) return;

            if (ctx.ReadValue<Vector2>().y < -_swipeThreshold)
                _rigidbody.linearVelocityY = -_swipeForce;
        }
    }
}