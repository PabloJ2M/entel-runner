using System.Threading.Tasks;
using UnityEngine;

namespace Unity.Services
{
    using Core;
    using Authentication;
    using CloudSave;
    using Economy;

    public static class UnityServicesExtension
    {
        public static async Task AuthResponse(this Task action)
        {
            try { await action; }
            catch (AuthenticationException ex) { Debug.LogError(ex); }
            catch (RequestFailedException ex) { Debug.LogError(ex); }
        }
        public static async Task CloudCodeResponse(this Task action)
        {
            try { await action; }
            catch (CloudSaveValidationException e) { Debug.LogError(e); }
            catch (CloudSaveRateLimitedException e) { Debug.LogError(e); }
            catch (CloudSaveException e) { Debug.LogError(e); }
        }
        public static async Task<bool> EconomyResponse(this Task action)
        {
            try { await action; return true; }
            catch (EconomyValidationException e) { Debug.LogError(e); }
            catch (EconomyRateLimitedException e) { Debug.LogError(e); }
            catch (EconomyException e) { Debug.LogError(e); }
            return false;
        }
    }
}