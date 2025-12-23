using System;

namespace Unity.Services
{
    using Core;

    public class UnityServiceInit : Singleton<UnityServiceInit>
    {
        public event Action onServiceInitialized;

        protected async override void Awake()
        {
            base.Awake();

            if (UnityServices.State == ServicesInitializationState.Uninitialized)
            {
                await UnityServices.InitializeAsync();
                onServiceInitialized?.Invoke();
            }
        }
    }
}