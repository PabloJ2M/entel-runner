using System;
using Newtonsoft.Json.Linq;

namespace Unity.Services.CloudSave
{
    [Serializable] public struct CloudCustomKey
    {
        public string key;
        public JObject value;
        public string writeLock;

        public CloudCustomKey(string key, string value, string writeLock = null)
        {
            this.key = key;
            this.value = JObject.Parse(value);
            this.writeLock = writeLock;
        }
    }
}