using UnityEngine;

namespace Unity.Services.CloudCode
{
    [CreateAssetMenu(fileName = "schedule", menuName = "system/cloud code/schedules")]
    public class CloudSchedule : ScriptableObject
    {
        public string scheduleId;
        public string cloudCodeFunction;

        [TextArea(2, 5)]
        public string cronExpression;

        [TextArea(3, 10)]
        public string payloadJson;
    }
}