using UnityEngine;

namespace Unity.Services.Authentication
{
    using PlayerAccounts;

    public class AuthUnityAccount : AuthBehaviour
    {
        protected override async void OnServiceInitialized()
        {
            #if UNITY_EDITOR
            PlayerAccountService.Instance.SignedIn += SignInOrLinkAccount;
            await LoginUnityPlayerServices();
            #else
            await Awaitable.NextFrameAsync();
            #endif
        }

        #if UNITY_EDITOR
        private async Awaitable LoginUnityPlayerServices() => await PlayerAccountService.Instance.StartSignInAsync().AuthResponse();
        #endif

        public override async void SignInOrLinkAccount()
        {
            #if UNITY_EDITOR
            if (!PlayerAccountService.Instance.IsSignedIn)
                await LoginUnityPlayerServices();
            #endif
            
            if (!AuthenticationService.Instance.IsSignedIn) await SignInAccountAsync(PlayerAccountService.Instance.AccessToken);
            else await LinkAccountAsync(PlayerAccountService.Instance.AccessToken);
        }

        protected async override Awaitable OnSignInAccountServiceAsync(string accessToken) =>
            await AuthenticationService.Instance.SignInWithUnityAsync(accessToken);
        protected async override Awaitable OnLinkAccountServiceAsync(string accessToken) =>
            await AuthenticationService.Instance.LinkWithUnityAsync(accessToken);
    }
}