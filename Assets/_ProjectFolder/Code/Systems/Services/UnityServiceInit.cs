using System;
using UnityEngine;

namespace Unity.Services
{
    using Core;

    public class UnityServiceInit : Singleton<UnityServiceInit>
    {
        [SerializeField] private bool _debug = true;
        public event Action onServiceInitialized;

        protected async override void Awake()
        {
            base.Awake();
            if (_debug && Application.isEditor)
                PlayerPrefs.DeleteAll();

            if (UnityServices.State == ServicesInitializationState.Uninitialized)
            {
                await UnityServices.InitializeAsync();
                onServiceInitialized?.Invoke();
            }
        }
    }
}