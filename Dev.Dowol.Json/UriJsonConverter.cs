﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dev.Dowol.Json;

public class UriJsonConverter : JsonConverter<Uri>
{
    public override Uri? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (Uri.TryCreate(reader.GetString(), UriKind.RelativeOrAbsolute, out Uri? value))
            return value;
        else
            return default;
    }

    public override void Write(Utf8JsonWriter writer, Uri value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
