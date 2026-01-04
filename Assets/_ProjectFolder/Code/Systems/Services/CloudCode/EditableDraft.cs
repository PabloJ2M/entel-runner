using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Unity.Services.CloudCode
{
    [Serializable] public class EditableDraft
    {
        public string name;
        public string eventName;
        public string type = "recurring";
        public string schedule = "0 0 * * *";
        public int payloadVersion = 1;
        public Payload payload = new();

        public EditableDraft() { }
        public EditableDraft(CloudSchedule cloud)
        {
            name = cloud.name;
            eventName = cloud.eventName;
            type = cloud.type;
            schedule = cloud.schedule;
            payloadVersion = cloud.payloadVersion;
            payload = JsonConvert.DeserializeObject<Payload>(cloud.payload);
        }

        public object ToJson()
        {
            return new {
                name,
                eventName,
                type,
                schedule,
                payloadVersion,
                payload = JsonConvert.SerializeObject(payload)
            };
        }
    }
    [Serializable] public class Payload
    {
        public string functionName;
        public Dictionary<string, object> @params = new();
    }
}