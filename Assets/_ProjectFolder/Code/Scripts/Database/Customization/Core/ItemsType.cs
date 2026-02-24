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
    public enum ItemSortingGroup
    {
        Head, Torso, Cadera,
        Hombro_L, Antebrazo_L, Mano_L,
        Hombro_R, Antebrazo_R, Mano_R,
        Muslo_L, Pierna_L, Pie_L,
        Muslo_R, Pierna_R, Pie_R
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