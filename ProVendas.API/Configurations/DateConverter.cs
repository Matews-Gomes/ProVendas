﻿using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProVendas.API.Configurations
{
    public class DateConverter : JsonConverter<DateTime>
    {
        private readonly string formatDate = "dd/MM/yyyy"; 
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), formatDate, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(formatDate));
        }
    }
}
