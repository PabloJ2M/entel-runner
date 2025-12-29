using System;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace Unity.Customization
{
    [Serializable] public class CustomizationDataLocal : IEquatable<CustomizationDataCloud>
    {
        public string selectedLibrary = string.Empty;
        public SerializedDictionary<string, SerializedDictionary<string, string>> equipped = new();
        public SerializedHashSet<string> unlocked = new();

        public CustomizationDataLocal(CustomizationDataCloud cloud)
        {
            selectedLibrary = cloud.library;
            equipped = cloud.equipped.ToSerializable();
            unlocked = new(cloud.unlocked);
        }

        public bool Equals(CustomizationDataCloud other) =>
            SerializedDictionaryExtensions.DeepEquals(equipped, other.equipped) && unlocked.Equals(other.unlocked);
    }
    [Serializable] public struct CustomizationDataCloud
    {
        public string library;
        public Dictionary<string, Dictionary<string, string>> equipped;
        public HashSet<string> unlocked;

        public CustomizationDataCloud(CustomizationDataLocal data)
        {
            library = data.selectedLibrary;
            equipped = data.equipped.ToDictionary();
            unlocked = data.unlocked.HashSet;
        }
    }
}