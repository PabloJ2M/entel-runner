using UnityEngine;

namespace Unity.Services.RemoteConfig
{
    public enum ConfigType
    {
        [InspectorName("Daily Missions")]
        daily_missions,

        [InspectorName("Weekly Missions")]
        weekly_missions,

        [InspectorName("Store Discounts")]
        daily_store_discounts
    }
}