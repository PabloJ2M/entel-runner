using System;
using UnityEngine;

namespace Unity.Services.Authentication
{
    using Core;

    public class AuthManager : MonoBehaviour
    {
        private UnityServiceInit _service;

        public event Action onSignInInitialized;
        public event Action onSignInHandler, onSignOutHandler;

        private void Awake() => _service = GetComponent<UnityServiceInit>();
        private void OnEnable() => _service.onServiceInitialized += SignInCallback;
        private void OnDisable()
        {
            _service.onServiceInitialized -= SignInCallback;

            if (UnityServices.State == ServicesInitializationState.Uninitialized) return;
            AuthenticationService.Instance.SignedIn -= HandleSignIn;
            AuthenticationService.Instance.SignedOut -= HandleSignOut;
        }

        private async void SignInCallback()
        {
            AuthenticationService.Instance.SignedIn += HandleSignIn;
            AuthenticationService.Instance.SignedOut += HandleSignOut;

            if (AuthenticationService.Instance.SessionTokenExists)
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            else
                onSignInInitialized?.Invoke();
        }
        private void HandleSignIn() => onSignInHandler?.Invoke();
        private void HandleSignOut() => onSignOutHandler?.Invoke();
    }
}