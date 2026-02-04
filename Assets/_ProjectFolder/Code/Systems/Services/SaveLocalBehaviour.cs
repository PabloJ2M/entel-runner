using UnityEngine;

namespace Unity
{
    public abstract class SaveLocalBehaviour : MonoBehaviour
    {
        protected abstract string _localDataID { get; }

        protected void LoadLocalData(ref string data) => data = PlayerPrefs.GetString(_localDataID, string.Empty);
        protected void LoadLocalData(ref bool data) => data = PlayerPrefs.GetInt(_localDataID, 0) == 1;
        protected void LoadLocalData<T>(ref T data)
        {
            if (!PlayerPrefs.HasKey(_localDataID)) return;
            data = JsonUtility.FromJson<T>(PlayerPrefs.GetString(_localDataID));
        }

        protected void SaveLocalData(string data)
        {
            PlayerPrefs.SetString(_localDataID, data);
            PlayerPrefs.Save();
        }
        protected void SaveLocalData(bool data)
        {
            PlayerPrefs.SetInt(_localDataID, data ? 1 : 0);
            PlayerPrefs.Save();
        }
        protected void SaveLocalData<T>(T data)
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