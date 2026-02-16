using UnityEngine;
using UnityEngine.InputSystem;

namespace Unity.Tutorial
{
    public class TutorialInputAction : TutorialHandlerBehaviour
    {
        [SerializeField] private InputActionReference _input;

        private void OnEnable() => _input.action.performed += PerformeAction;
        private void OnDisable() => _input.action.performed -= PerformeAction;

        private void PerformeAction(InputAction.CallbackContext ctx) => HandleInteraction();
    }
}