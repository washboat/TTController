﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TTController.Common;

namespace TTController.Service.Config.Converters
{
    public class LedColorGradientConverter : JsonConverter<LedColorGradient>
    {
        public override void WriteJson(JsonWriter writer, LedColorGradient value, JsonSerializer serializer) =>
            writer.WriteRawValue(JsonConvert.SerializeObject(value.Points));

        public override LedColorGradient ReadJson(JsonReader reader, Type objectType, LedColorGradient existingValue, bool hasExistingValue,
            JsonSerializer serializer) =>
            new LedColorGradient(JArray.Load(reader).ToObject<List<LedColorGradientPoint>>());
    }

    public class LedColorGradientPointConverter : AbstractObjectToArrayConverter<LedColorGradientPoint>
    {
        protected override object[] CreateConstructorArgs(JArray array) =>
            new object[] { array[0].Value<double>(), array[1].ToObject<LedColor>() };
    }
}
