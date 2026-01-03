using System;
using UnityEngine;

namespace Unity.Services.CloudCode
{
    [Serializable] public struct Schedule
    {
        public string functionName;
        [TextArea(3, 10)] public string @params;
    }

    [CreateAssetMenu(fileName = "schedule", menuName = "system/cloud code/schedules")]
    public class CloudSchedule : ScriptableObject
    {
        public string name;
        public string eventName;
        public string type = "recurring";

        public string schedule;
        public uint payloadVersion = 1;

        public Schedule payload;
    }
}