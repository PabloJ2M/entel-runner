using System;

namespace Unity.Services.RemoteConfig
{
    [Serializable] public class RemoteConfigData
    {
        public string date;
        public string[] missions;
        public string[] store_discounts;
    }
}