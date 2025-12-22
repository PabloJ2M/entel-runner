using UnityEngine;

namespace Unity.Services.Authentication
{
    using Core;
    using PlayerAccounts;

    public class AuthStateManager : MonoBehaviour
    {
        private async void Awake()
        {
            if (UnityServices.State == ServicesInitializationState.Uninitialized)
                await UnityServices.InitializeAsync();

            PlayerAccountService.Instance.SignedIn += OnLoginServiceCompleted;
        }
        private async void Start()
        {
            if (AuthenticationService.Instance.SessionTokenExists)
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        private void OnDestroy()
        {
            PlayerAccountService.Instance.SignedIn -= OnLoginServiceCompleted;
        }

        private void OnLoginServiceCompleted()
        {
            print("Signed In");
        }
    }
}