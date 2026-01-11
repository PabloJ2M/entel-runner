using Newtonsoft.Json;
using UnityEngine;

namespace Unity.Services.CloudSave
{
    public interface ICloudSaveGameData
    {
        string ItemsListToJson();
    }
    public interface IJsonData
    {
        string Json() => JsonUtility.ToJson(this);
        string JsonDictionary() => JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}