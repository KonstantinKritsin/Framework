using System;
using System.IO;

// ReSharper disable UnusedMember.Global

namespace Project.Framework.Common.JsonConverter
{
    public interface IJsonConverter
    {
        TResult DeserializeObject<TResult>(string data);
        object DeserializeObject(Type type, string data);
        string SerializeObject(object value);
        void Serialize(TextWriter writer, object data);
        string Null { get; }
    }
}