using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Jump : MonoBehaviour
    {
        [SerializeField] private InputActionReference _input;
        [SerializeField] private float _force;
        private Rigidbody2D _rgbd;

        private void Awake() => _rgbd = GetComponent<Rigidbody2D>();
        private void Start() => _input.action.performed += OnJump;
        private void OnEnable() => _input.action.Enable();
        private void OnDisable() => _input.action.Disable();

        private void OnJump(InputAction.CallbackContext ctx)
        {
            if (!ctx.action.IsPressed()) return;
            _rgbd.linearVelocityY = _force;
        }
    }
}