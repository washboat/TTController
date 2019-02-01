﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TTController.Common;

namespace TTController.Service.Config.Converter
{
    public abstract class ObjectToArrayConverter<T> : JsonConverter<T> where T : new()
    {
        public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
        {
            var properties = FilterProperties(value.GetType().GetProperties());
            var array = new JArray(properties.Select(p => p.GetValue(value)));
            writer.WriteRawValue(JsonConvert.SerializeObject(array, Formatting.None));
        }

        public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            JArray array = JArray.Load(reader);
            return (T) Activator.CreateInstance(typeof(T), CreateConstructorArgs(array));
        }

        protected virtual IEnumerable<PropertyInfo> FilterProperties(IEnumerable<PropertyInfo> properties) => properties;
        protected abstract object[] CreateConstructorArgs(JArray array);
    }

    public class PortIdentifierConverter : ObjectToArrayConverter<PortIdentifier>
    {
        protected override object[] CreateConstructorArgs(JArray array) =>
            new object[] { array[0].Value<int>(), array[1].Value<int>(), array[2].Value<byte>() };
    }

    class LedColorConverter : ObjectToArrayConverter<LedColor>
    {
        protected override object[] CreateConstructorArgs(JArray array) =>
            new object[] { array[0].Value<byte>(), array[1].Value<byte>(), array[2].Value<byte>() };
    }

    public class CurvePointConverter : ObjectToArrayConverter<CurvePoint>
    {
        protected override object[] CreateConstructorArgs(JArray array) =>
            new object[] { array[0].Value<int>(), array[1].Value<int>() };
    }
}