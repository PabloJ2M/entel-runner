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

        public CloudSchedule(string id, EditableDraft draft, int updateVersion = 0)
        {
            this.id = id;
            name = draft.name;
            eventName = draft.eventName;
            type = draft.type;

            schedule = draft.schedule;

            payloadVersion = draft.payloadVersion + updateVersion;
            payload = draft.payload.ToJsonString();
        }
    }

    [Serializable] public struct TriggerSchedule
    {
        public string name;
        public string eventType;
        public string actionUrn;
        public string actionType;

        public TriggerSchedule(EditableDraft schedule)
        {
            name = $"{schedule.eventName}-trigger";
            eventType = $"com.unity.services.scheduler.{schedule.eventName}.v{schedule.payloadVersion}";
            actionUrn = $"urn:ugs:cloud-code:{schedule.payload.functionName}";
            actionType = "cloud-code";
        }
    }
}