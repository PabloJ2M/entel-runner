using System;
using Newtonsoft.Json;

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
            payload = JsonConvert.SerializeObject(draft.payload);
        }
    }
}