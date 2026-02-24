using System.Threading.Tasks;
using UnityEngine;

namespace Unity.Services
{
    using Core;
    using Authentication;
    using CloudSave;
    using Economy;

    public static class UnityServiceExtension
    {
        public static async Awaitable AuthResponse(this Task action)
        {
            try { await action; }
            catch (AuthenticationException ex) { Debug.LogError(ex); }
            catch (RequestFailedException ex) { Debug.LogError(ex); }
        }
        public static async Awaitable AuthResponse(this Awaitable action)
        {
            try { await action; }
            catch (AuthenticationException ex) { Debug.LogError(ex); }
            catch (RequestFailedException ex) { Debug.LogError(ex); }
        }

        public static async Awaitable CloudCodeResponse(this Task action)
        {
            try { await action; }
            catch (CloudSaveValidationException e) { Debug.LogError(e); }
            catch (CloudSaveRateLimitedException e) { Debug.LogError(e); }
            catch (CloudSaveException e) { Debug.LogError(e); }
        }
        public static async Awaitable CloudCodeResponse(this Awaitable action)
        {
            try { await action; }
            catch (CloudSaveValidationException e) { Debug.LogError(e); }
            catch (CloudSaveRateLimitedException e) { Debug.LogError(e); }
            catch (CloudSaveException e) { Debug.LogError(e); }
        }

        public static async Awaitable<bool> EconomyResponse(this Task action)
        {
            try { await action; return true; }
            catch (EconomyValidationException e) { Debug.LogError(e); }
            catch (EconomyRateLimitedException e) { Debug.LogError(e); }
            catch (EconomyException e) { Debug.LogError(e); }
            return false;
        }
        public static async Awaitable<bool> EconomyResponse(this Awaitable action)
        {
            try { await action; return true; }
            catch (EconomyValidationException e) { Debug.LogError(e); }
            catch (EconomyRateLimitedException e) { Debug.LogError(e); }
            catch (EconomyException e) { Debug.LogError(e); }
            return false;
        }
    }
}