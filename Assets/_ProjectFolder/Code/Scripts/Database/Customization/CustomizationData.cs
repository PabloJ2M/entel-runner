using System;

namespace Unity.Customization
{
    [Serializable]
    public class CustomizationData
    {
        public string selectedLibrary;
        public NestedStringDictionary equipped;
        public NestedHashSetDictionary unlocked;
    }
}