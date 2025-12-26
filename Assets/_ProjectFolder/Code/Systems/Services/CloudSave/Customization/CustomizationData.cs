using System;
using System.Collections.Generic;

namespace Unity.Customization
{
    [Serializable]
    public class CustomizationData : IEquatable<CustomizationDataCloud>
    {
        public SerializableDictionary<string, string> equipped = new();
        public SerializableHashSet<string> unlocked = new();

        public CustomizationData(CustomizationDataCloud cloud)
        {
            equipped = new(cloud.equipped);
            unlocked = new(cloud.unlocked);
        }

        public bool Equals(CustomizationDataCloud other) => equipped.Equals(other.equipped) && unlocked.Equals(other.unlocked);
    }

    [Serializable]
    public struct CustomizationDataCloud
    {
        public Dictionary<string, string> equipped;
        public HashSet<string> unlocked;

        public CustomizationDataCloud(CustomizationData data)
        {
            equipped = new(data.equipped);
            unlocked = new(data.unlocked);
        }
    }
}