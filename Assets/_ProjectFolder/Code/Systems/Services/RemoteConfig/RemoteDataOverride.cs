using System;

namespace Unity.Services.RemoteConfig
{
    [Serializable] public class RemoteDataOverride
    {
        public string[] missions;
        public string[] store_discounts;
    }
}