using System.Threading.Tasks;
using UnityEngine;

namespace Unity.Services.Authentication
{
    public abstract class AuthBehaviour : MonoBehaviour
    {
        public abstract void SignInOrLinkAccount();
        protected abstract Task OnSignInAccountServiceAsync(string accessToken);
        protected abstract Task OnLinkAccountServiceAsync(string accessToken);

        protected async Task SignInAccountAsync(string accessToken) =>
            await OnSignInAccountServiceAsync(accessToken).AuthResponse();

        protected async Task LinkAccountAsync(string accessToken) =>
            await OnLinkAccountServiceAsync(accessToken).AuthResponse();
    }
}