using System;
using System.Threading.Tasks;

#if UNITY_IOS
using Apple.GameKit;
#endif

namespace Unity.Services.Authentication
{
    public class AuthAppleGameCenter : AuthBehaviour
    {
        private string Signature, TeamPlayerID, PublicKeyUrl, Salt;
        private ulong Timestamp;

        protected override async void OnServiceInitialized()
        {
            #if UNITY_IOS
            await LogInAppleGameCenterServices();
            #else
            await Task.Yield();
            #endif
        }

        #if UNITY_IOS
        private async Task LogInAppleGameCenterServices()
        {
            var player = await GKLocalPlayer.Authenticate();
            var localPlayer = GKLocalPlayer.Local;

            var fetchItemsResponse =  await GKLocalPlayer.Local.FetchItems();

            Signature = Convert.ToBase64String(fetchItemsResponse.GetSignature());
            TeamPlayerID = localPlayer.TeamPlayerId;

            Salt = Convert.ToBase64String(fetchItemsResponse.GetSalt());
            PublicKeyUrl = fetchItemsResponse.PublicKeyUrl;
            Timestamp = fetchItemsResponse.Timestamp;

            SignInOrLinkAccount();
        }
        #endif

        public override async void SignInOrLinkAccount()
        {
            #if UNITY_IOS
            if (!GKLocalPlayer.Local.IsAuthenticated) {
                await LogInAppleGameCenterServices();
                return;
            }
            #endif

            if (string.IsNullOrEmpty(Signature)) return;

            if (!AuthenticationService.Instance.IsSignedIn) await SignInAccountAsync(Signature);
            else await LinkAccountAsync(Signature);
        }

        protected override async Task OnSignInAccountServiceAsync(string accessToken) =>
            await AuthenticationService.Instance.SignInWithAppleGameCenterAsync(accessToken, TeamPlayerID, PublicKeyUrl, Salt, Timestamp);
        protected override async Task OnLinkAccountServiceAsync(string accessToken) =>
            await AuthenticationService.Instance.LinkWithAppleGameCenterAsync(accessToken, TeamPlayerID, PublicKeyUrl, Salt, Timestamp);
    }
}