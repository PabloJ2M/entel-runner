using System;
using UnityEngine.Rendering;

namespace Unity.Services.RemoteConfig
{
    [Serializable] public class AchievementsData
    {
        public SerializedDictionary<string, AchievementWrapper> groups = new();
    }
    [Serializable] public class AchievementWrapper
    {
        public string date;
        public string[] missions;
    }
}