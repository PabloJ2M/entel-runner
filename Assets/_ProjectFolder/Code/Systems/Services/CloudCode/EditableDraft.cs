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
        public int payloadVersion;
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
    }
    [Serializable] public class Payload
    {
        public string functionName;
        public Dictionary<string, object> @params = new();

        public Payload() { }
        //public Payload(string functionName, string @params)
        //{
        //    this.functionName = functionName;
        //    this.@params = @params;
        //}
    }
}