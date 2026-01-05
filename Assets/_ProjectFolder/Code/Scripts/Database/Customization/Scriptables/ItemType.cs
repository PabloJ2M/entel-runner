using UnityEngine;

namespace Unity.Customization
{
    public enum ItemType
    {
        None = -1,

        [InspectorName("Calidad Baja")]
        LowQuality = 1,

        [InspectorName("Calidad Media")]
        MediumQuality = 2,

        [InspectorName("Calidad Alta")]
        HighQuality = 3
    }
}