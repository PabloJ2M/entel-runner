using UnityEngine;

namespace Unity.Services.Authentication
{
    public abstract class AuthBehaviour : MonoBehaviour
    {
        private AuthManager _authManager;

        protected virtual void Awake() => _authManager = UnityServiceInit.Instance.GetComponent<AuthManager>();
        protected virtual void OnEnable() => _authManager.onSignInInitialized += OnServiceInitialized;
        protected virtual void OnDisable() => _authManager.onSignInInitialized -= OnServiceInitialized;

        protected abstract void OnServiceInitialized();
        protected abstract Awaitable OnSignInAccountServiceAsync(string accessToken);
        protected abstract Awaitable OnLinkAccountServiceAsync(string accessToken);
        public abstract void SignInOrLinkAccount();

        protected async Awaitable SignInAccountAsync(string accessToken) =>
            await OnSignInAccountServiceAsync(accessToken).AuthResponse();

        protected async Awaitable LinkAccountAsync(string accessToken) =>
            await OnLinkAccountServiceAsync(accessToken).AuthResponse();
    }
}