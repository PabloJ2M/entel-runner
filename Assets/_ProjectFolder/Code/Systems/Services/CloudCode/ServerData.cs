using System;
using System.Globalization;

namespace Unity.Services.CloudCode
{
    [Serializable] public struct ServerTimeUTC
    {
        public string serverTime;

        public DateTime Time => DateTime.Parse(serverTime, null, DateTimeStyles.RoundtripKind);
        public bool HasResponse => !string.IsNullOrEmpty(serverTime);
    }
}