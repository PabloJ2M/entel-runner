using System;

namespace Unity.Customization
{
    [Serializable]
    public class CustomizationData
    {
        public string selectedLibrary;
        public SerializedNestedString equipped;
        public SerializedNestedHashSet unlocked;
    }
}