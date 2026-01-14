using UnityEngine;

namespace Unity.Customization
{
    public enum ItemGroup
    {
        None = -1,
        Head,
        Top,
        Pants,
        Shoes
    }

    public enum ItemQuality
    {
        None = -1,

        [InspectorName("Calidad Baja")]
        LowQuality = 0,

        [InspectorName("Calidad Media")]
        MediumQuality = 1,

        [InspectorName("Calidad Alta")]
        HighQuality = 2
    }
}