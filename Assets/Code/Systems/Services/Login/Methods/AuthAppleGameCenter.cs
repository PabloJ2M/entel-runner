using System.Threading.Tasks;

namespace Unity.Services.Authentication
{
    public class AuthAppleGameCenter : AuthBehaviour
    {
        private void Awake()
        {
            LogInAppleGameCenterServices();
        }
        private void LogInAppleGameCenterServices()
        {

        }

        public override void SignInOrLinkAccount()
        {
            
        }
        protected override async Task OnSignInAccountServiceAsync(string accessToken)
        {
            await AuthenticationService.Instance.SignInWithAppleGameCenterAsync("", "", "", "", 0);
        }
        protected override async Task OnLinkAccountServiceAsync(string accessToken)
        {
            await AuthenticationService.Instance.LinkWithAppleGameCenterAsync("", "", "", "", 0);
        }
    }
}