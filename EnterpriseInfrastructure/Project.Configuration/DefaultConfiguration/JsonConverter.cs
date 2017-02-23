using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Framework.Common.JsonConverter;

namespace Project.Configuration.DefaultConfiguration
{
    public class JsonConverter : IJsonConverter
    {
        public TResult DeserializeObject<TResult>(string data)
        {
            return JsonConvert.DeserializeObject<TResult>(data);
        }

        public object DeserializeObject(Type type, string data)
        {
            return JsonConvert.DeserializeObject(data, type, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        public string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        public void Serialize(TextWriter writer, object data)
        {
            var str = JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            writer.Write(str);
        }

        public string Null { get; } = string.Empty;
    }
}