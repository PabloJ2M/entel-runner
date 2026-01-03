using UnityEngine;

namespace Unity
{
    public abstract class LocalSaveBehaviour : MonoBehaviour
    {
        protected abstract string _dataID { get; }

        protected virtual void LoadLocalData<T>(ref T data)
        {
            if (!PlayerPrefs.HasKey(_dataID)) return;
            data = JsonUtility.FromJson<T>(PlayerPrefs.GetString(_dataID));
        }
        protected virtual void SaveLocalData<T>(T data)
        {
            PlayerPrefs.SetString(_dataID, JsonUtility.ToJson(data));
            PlayerPrefs.Save();
        }
        protected virtual void DeleteLocalData()
        {
            if (PlayerPrefs.HasKey(_dataID)) PlayerPrefs.DeleteKey(_dataID);
        }
    }
}