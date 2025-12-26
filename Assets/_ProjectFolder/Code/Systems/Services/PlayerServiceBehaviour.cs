using UnityEngine;

namespace Unity.Services
{
    using Authentication;

    [RequireComponent(typeof(AuthManager))]
    public abstract class PlayerServiceBehaviour : MonoBehaviour
    {
        public abstract string DataID { get; }

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

        protected void LoadLocalData<T>(ref T data)
        {
            if (!PlayerPrefs.HasKey(DataID)) return;
            data = JsonUtility.FromJson<T>(PlayerPrefs.GetString(DataID));
        }
        protected void SaveLocalData<T>(T data)
        {
            PlayerPrefs.SetString(DataID, JsonUtility.ToJson(data));
            PlayerPrefs.Save();
        }
        protected void DeleteLocalData()
        {
            if (PlayerPrefs.HasKey(DataID)) PlayerPrefs.DeleteKey(DataID);
            OnSignOutCompleted();
        }
    }
}