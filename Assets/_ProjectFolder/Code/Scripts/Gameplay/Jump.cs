using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Movement
{
    [RequireComponent(typeof(Rigidbody2D), typeof(AnimatorEvents))]
    public class Jump : MonoBehaviour
    {
        [SerializeField] private InputActionReference _pressInput;
        [SerializeField] private float _inputForgiveness = 0.5f;

        [Header("Atributes")]
        [SerializeField] private int _maxJumps = 2;
        [SerializeField] private float _jumpForce = 15f;
        [SerializeField] private float _lowJumpMultiplier = 2f;

        private Rigidbody2D _rigidbody;
        private AnimatorEvents _animator;

        private int _currentJumps;
        private bool _isGrounded, _isPressing, _jumpBuffered;
        private float _limitVelocity;

        public bool IsGrounded => _isGrounded;
        public Action onJump;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<AnimatorEvents>();
        }
        private void Start() => _limitVelocity = Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        private void OnEnable() => _pressInput.action.performed += OnJump;
        private void OnDisable()
        {
            CancelInvoke();
            _isPressing = false;
            _pressInput.action.performed -= OnJump;
        }

        private void FixedUpdate()
        {
            _animator?.SetGravity(_rigidbody.linearVelocityY);

            if (!_isPressing && _rigidbody.linearVelocityY > 0)
                _rigidbody.linearVelocityY += _limitVelocity;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            _isGrounded = true;
            _currentJumps = 0;
            _animator?.SetGroundCheck(_isGrounded);

            if (_jumpBuffered)
                JumpImpulse();
        }
        private void OnCollisionExit2D(Collision2D collision) => _isGrounded = false;

        private void OnJump(InputAction.CallbackContext ctx)
        {
            if (ctx.action.WasPressedThisFrame())
            {
                _isPressing = true;

                if (_isGrounded || _currentJumps < _maxJumps)
                {
                    JumpImpulse();
                }
                else
                {
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

        private void ResetJumpBuffer() => _jumpBuffered = false;
        private void JumpImpulse()
        {
            CancelInvoke();

            onJump?.Invoke();
            _currentJumps++;
            _jumpBuffered = _isGrounded = false;
            _rigidbody.linearVelocityY = _jumpForce;
            _animator?.SetGroundCheck(_isGrounded);
        }
    }
}