using System.Threading.Tasks;
using UnityEngine;

namespace Unity.Services.Authentication
{
    using Core;

    public abstract class AuthBehaviour : MonoBehaviour
    {
        public abstract void SignInOrLinkAccount();
        protected abstract Task OnSignInAccountServiceAsync(string accessToken);
        protected abstract Task OnLinkAccountServiceAsync(string accessToken);

        protected async Task SignInAccountAsync(string accessToken) =>
            await TaskResponse(OnSignInAccountServiceAsync(accessToken));

        protected async Task LinkAccountAsync(string accessToken) =>
            await TaskResponse(OnLinkAccountServiceAsync(accessToken));

        private async Task TaskResponse(Task action)
        {
            try { await action; }
            catch (AuthenticationException ex) { Debug.LogError(ex); }
            catch (RequestFailedException ex) { Debug.LogError(ex); }
        }
    }
}