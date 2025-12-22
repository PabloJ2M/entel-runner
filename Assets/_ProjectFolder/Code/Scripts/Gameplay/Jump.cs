using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Jump : MonoBehaviour
    {
        [SerializeField] private InputActionReference _input;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _force;
        private Rigidbody2D _rgbd;

        private bool _isGrounded/*, _airJump*/;

        private void Awake() => _rgbd = GetComponent<Rigidbody2D>();
        private void Start() => _input.action.performed += OnJump;
        private void OnDestroy() => _input.action.performed -= OnJump;
        private void OnEnable() => _input.action.Enable();
        private void OnDisable() => _input.action.Disable();
        private void OnCollisionEnter2D(Collision2D collision)
        {
            /*_airJump = */_isGrounded = true;
            _animator?.SetBool("IsGrounded", true);
        }

        private void OnJump(InputAction.CallbackContext ctx)
        {
            if ((!_isGrounded /*&& !_airJump*/) || !ctx.action.IsPressed()) return;
            //if (!_isGrounded) _airJump = false;

            _animator?.SetBool("IsGrounded", false);
            _rgbd.linearVelocityY = _force;
            _isGrounded = false;
        }
    }
}