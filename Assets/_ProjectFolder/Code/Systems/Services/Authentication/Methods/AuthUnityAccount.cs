using System.Threading.Tasks;

namespace Unity.Services.Authentication
{
    using PlayerAccounts;

    public class AuthUnityAccount : AuthBehaviour
    {
        protected override void OnServiceInitialized()
        {
            #if UNITY_EDITOR
            //SignInOrLinkAccount();
            AuthenticationService.Instance.SignInAnonymouslyAsync();
            #endif
        }

        public override async void SignInOrLinkAccount()
        {
            if (!AuthenticationService.Instance.IsSignedIn) {
                await SignInAccountAsync(PlayerAccountService.Instance.AccessToken);
                return;
            }
            if (!HasUnityID()) {
                await LinkAccountAsync(PlayerAccountService.Instance.AccessToken);
                return;
            }
        }
        private bool HasUnityID() => AuthenticationService.Instance.PlayerInfo.GetUnityId() != null;

        protected async override Task OnSignInAccountServiceAsync(string accessToken) =>
            await AuthenticationService.Instance.SignInWithUnityAsync(accessToken);

        protected async override Task OnLinkAccountServiceAsync(string accessToken) =>
            await AuthenticationService.Instance.LinkWithUnityAsync(accessToken);
    }
}