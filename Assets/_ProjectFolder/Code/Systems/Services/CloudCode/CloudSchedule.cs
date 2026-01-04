using System;

namespace Unity.Services.CloudCode
{
    [Serializable] public struct CloudSchedule
    {
        public string id;
        public string name;
        public string eventName;
        public string type;

        public string schedule;

        public int payloadVersion;
        public string payload;
    }
}