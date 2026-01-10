using System;
using System.Linq;
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
            payload = new(cloud.payload);
        }

        public object ToJson() => new { name, eventName, type, schedule, payloadVersion, payload = payload.ToJsonString() };
    }
    [Serializable] public class Payload
    {
        public string functionName;
        public List<ParamEntry> @params = new();

        public Payload() { }
        public Payload(string cloudPayload)
        {
            var raw = JsonConvert.DeserializeObject<RawPayload>(cloudPayload);

            functionName = raw.functionName;
            @params = raw.@params?.ToParamEntries().ToList() ?? new();
        }

        public object ToJson() => new { functionName, @params = @params.BuildParams() };
        public string ToJsonString() => JsonConvert.SerializeObject(ToJson());
    }

    public enum ParamType { String, Int, Float, Bool }
    [Serializable] public struct RawPayload
    {
        public string functionName;
        public Dictionary<string, object> @params;
    }
    [Serializable] public class ParamEntry
    {
        public string key;
        public ParamType type;
        public string value;
    }
}