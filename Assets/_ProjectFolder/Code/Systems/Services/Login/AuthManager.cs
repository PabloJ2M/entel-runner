using System;
using UnityEngine;

namespace Unity.Services.Authentication
{
    public class AuthManager : MonoBehaviour
    {
        private UnityServiceInit _service;
        public event Action onSignInCompleted;

        private void Awake() => _service = GetComponent<UnityServiceInit>();
        private void OnEnable() => _service.onServiceInitialized += HandleSignIn;
        private void OnDisable() => _service.onServiceInitialized -= HandleSignIn;

        private async void HandleSignIn()
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            onSignInCompleted?.Invoke();
        }
    }
}