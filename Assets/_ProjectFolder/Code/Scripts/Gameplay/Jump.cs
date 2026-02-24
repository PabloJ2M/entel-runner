using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Movement
{
    [RequireComponent(typeof(Rigidbody2D), typeof(AnimatorEvents))]
    public class Jump : MonoBehaviour
    {
        [SerializeField] private InputActionReference _pressInput, _deltaInput;
        [SerializeField] private float _inputForgiveness = 0.5f, _swipeThreshold = 50f;
        [SerializeField] private float _swipeLockAfterJump = 0.08f;

        [Header("Atributes")]
        [SerializeField] private float _jumpForce = 15f;
        [SerializeField] private float _swipeForce = 25f;
        [SerializeField] private float /*_fallMultiplier = 2.5f,*/ _lowJumpMultiplier = 2f;

        private Rigidbody2D _rigidbody;
        private AnimatorEvents _animator;

        private bool _isGrounded, _isPressing, _jumpBuffered;
        private float /*_fallVelocity, */_limitVelocity, _swipeLockTimer;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<AnimatorEvents>();
        }
        private void Start()
        {
            //_fallVelocity = Physics2D.gravity.y * (_fallMultiplier - 1) * Time.fixedDeltaTime;
            _limitVelocity = Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
        private void OnEnable()
        {
            _pressInput.action.performed += OnJump;
            _deltaInput.action.performed += OnSwipe;
        }
        private void OnDisable()
        {
            CancelInvoke();
            _isPressing = false;
            _pressInput.action.performed -= OnJump;
            _deltaInput.action.performed -= OnSwipe;
        }

        private void FixedUpdate()
        {
            _animator?.SetGravity(_rigidbody.linearVelocityY);

            if (_swipeLockTimer > 0) _swipeLockTimer -= Time.fixedDeltaTime;

            //if (_rigidbody.linearVelocityY < 0) _rigidbody.linearVelocityY += _fallVelocity;
            /*else */
            if (!_isPressing && _rigidbody.linearVelocityY > 0) _rigidbody.linearVelocityY += _limitVelocity;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            _isGrounded = true;
            _animator?.SetGroundCheck(_isGrounded);
            if (_jumpBuffered) JumpImpulse();
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            _isGrounded = false;
        }

        private void OnJump(InputAction.CallbackContext ctx)
        {
            if (ctx.action.WasPressedThisFrame())
            {
                _isPressing = true;
                if (_isGrounded) JumpImpulse();
                else {
                    _jumpBuffered = true;
                    Invoke(nameof(ResetJumpBuffer), _inputForgiveness);
                }
            }
            else if (ctx.action.WasReleasedThisFrame())
            {
                CancelInvoke(nameof(ResetJumpBuffer));
                _jumpBuffered = _isPressing = false;
            }
        }
        private void OnSwipe(InputAction.CallbackContext ctx)
        {
            if (_isGrounded || _swipeLockTimer > 0) return;

            if (ctx.ReadValue<Vector2>().y < -_swipeThreshold)
                _rigidbody.linearVelocityY = -_swipeForce;
        }

        private void ResetJumpBuffer() => _jumpBuffered = false;
        //private void ResetPressThreshold() => _isPressing = false;
        private void JumpImpulse()
        {
            CancelInvoke();

            _jumpBuffered = _isGrounded = false;
            _swipeLockTimer = _swipeLockAfterJump;
            _rigidbody.linearVelocityY = _jumpForce;
            _animator?.SetGroundCheck(_isGrounded);
        }
    }
}