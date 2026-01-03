using UnityEngine;

namespace Unity.Services
{
    using Authentication;

    [DefaultExecutionOrder(-50), RequireComponent(typeof(AuthManager))]
    public abstract class PlayerServiceBehaviour : LocalSaveBehaviour
    {
        private AuthManager _auth;

        protected virtual void Awake() => _auth = GetComponent<AuthManager>();
        protected virtual void OnEnable()
        {
            _auth.onSignInCompleted += OnSignInCompleted;
            _auth.onSignOutHandler += DeleteLocalData;
        }
        protected virtual void OnDisable()
        {
            _auth.onSignInCompleted -= OnSignInCompleted;
            _auth.onSignOutHandler -= DeleteLocalData;
        }

        protected abstract void OnSignInCompleted();
        protected abstract void OnSignOutCompleted();
        protected override void DeleteLocalData()
        {
            base.DeleteLocalData();
            OnSignOutCompleted();
        }
    }
}