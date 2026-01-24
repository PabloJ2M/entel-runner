using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Movement
{
    [RequireComponent(typeof(Rigidbody2D), typeof(AnimatorEvents))]
    public class Jump : MonoBehaviour
    {
        [SerializeField] private InputActionReference _jumpInput;

        [Header("Params")]
        [SerializeField] private float _jumpForce = 24f;
        [SerializeField] private float _cutJumpVelocity = 3f;
        [SerializeField] private float _fallMultiplier = 4f;

        private Rigidbody2D _rb;
        private AnimatorEvents _animator;
        private bool _isGrounded;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<AnimatorEvents>();
        }
        private void OnEnable()
        {
            _jumpInput.action.Enable();
            _jumpInput.action.performed += OnJump;
        }
        private void OnDisable()
        {
            _jumpInput.action.performed -= OnJump;
            _jumpInput.action.Disable();
        }

        private void FixedUpdate()
        {
            _animator?.SetGravity(_rb.linearVelocityY);
            if (!_jumpInput.action.IsPressed() && _rb.linearVelocity.y > _cutJumpVelocity)
            {
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x,_cutJumpVelocity);
            }

            if (_rb.linearVelocity.y < 0)
            {
                _rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (_fallMultiplier - 1) * Time.fixedDeltaTime;
            }
        }

        private void OnJump(InputAction.CallbackContext ctx)
        {
            if (!_isGrounded) return;
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x,_jumpForce);

            _isGrounded = false;
            _animator?.SetGroundCheck(_isGrounded);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _isGrounded = true;
            _animator?.SetGroundCheck(_isGrounded);
        }
    }
}
