using UnityEngine;

namespace Unity
{
    public abstract class SaveLocalBehaviour : MonoBehaviour
    {
        protected abstract string _localDataID { get; }

        protected virtual void LoadLocalData<T>(ref T data)
        {
            if (!PlayerPrefs.HasKey(_localDataID)) return;
            data = JsonUtility.FromJson<T>(PlayerPrefs.GetString(_localDataID));
        }
        protected virtual void SaveLocalData<T>(T data)
        {
            PlayerPrefs.SetString(_localDataID, JsonUtility.ToJson(data));
            PlayerPrefs.Save();
        }
        protected virtual void DeleteLocalData()
        {
            if (PlayerPrefs.HasKey(_localDataID)) PlayerPrefs.DeleteKey(_localDataID);
        }
    }
}