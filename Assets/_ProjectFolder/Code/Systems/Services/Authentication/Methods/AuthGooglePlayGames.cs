using UnityEngine;

#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif

namespace Unity.Services.Authentication
{
    public class AuthGooglePlayGames : AuthBehaviour
    {
        private string _tokenID;

        protected override void OnServiceInitialized()
        {
            #if UNITY_ANDROID
            //PlayGamesPlatform.Activate();
            //LogInGooglePlayGamesServices();
            #endif
        }

        #if UNITY_ANDROID
        private void LogInGooglePlayGamesServices() => PlayGamesPlatform.Instance.Authenticate(OnAuthenticate);
        private void OnAuthenticate(SignInStatus status)
        {
            if (status == SignInStatus.Success)
                PlayGamesPlatform.Instance.RequestServerSideAccess(true, RequestAccessToken);
        }
        private void RequestAccessToken(string code)
        {
            _tokenID = code;
            SignInOrLinkAccount();
        }
        #endif

        public override async void SignInOrLinkAccount()
        {
            #if UNITY_ANDROID
            if (!PlayGamesPlatform.Instance.IsAuthenticated()) {
                LogInGooglePlayGamesServices();
                return;
            }
            #endif

            if (string.IsNullOrEmpty(_tokenID)) return;

            if (!AuthenticationService.Instance.IsSignedIn) await SignInAccountAsync(_tokenID);
            else await LinkAccountAsync(_tokenID);
        }

        protected override async Awaitable OnSignInAccountServiceAsync(string accessToken) =>
            await AuthenticationService.Instance.SignInWithGooglePlayGamesAsync(accessToken);
        protected override async Awaitable OnLinkAccountServiceAsync(string accessToken) =>
            await AuthenticationService.Instance.LinkWithGooglePlayGamesAsync(accessToken);
    }
}