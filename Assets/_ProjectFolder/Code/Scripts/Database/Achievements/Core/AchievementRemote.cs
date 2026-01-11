using System;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace Unity.Achievements
{
    using Services.CloudSave;

    [Serializable] public class AchievementRemoteGroup
    {
        public SerializedDictionary<string, AchievementRemote> groups = new();
    }
    [Serializable] public struct AchievementRemote
    {
        public string date;
        public string[] missions;
    }

    public struct AchievementCloud : IJsonData
    {
        public IList<string> missions_list;

        public AchievementCloud(IEnumerable<SO_Achievement> achievements)
        {
            missions_list = new List<string>();

            foreach (var achievement in achievements)
                missions_list.Add(achievement.ID);
        }
    }
}