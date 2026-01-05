using System;
using UnityEngine;

namespace Unity.Services
{
    using Core;

    public class UnityServiceInit : Singleton<UnityServiceInit>
    {
        #if UNITY_EDITOR
        [SerializeField] private bool _debug = true, _disable = false;
        #endif

        public event Action onServiceInitialized;

        protected async override void Awake()
        {
            base.Awake();

            #if UNITY_EDITOR
            if (_debug) PlayerPrefs.DeleteAll();
            if (_disable) return;
            #endif

            if (UnityServices.State == ServicesInitializationState.Uninitialized)
            {
                await UnityServices.InitializeAsync();
                onServiceInitialized?.Invoke();
            }
        }
    }
}