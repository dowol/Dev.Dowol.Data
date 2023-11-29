using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dev.Dowol.Json;

public class CultureInfoJsonConverter : JsonConverter<CultureInfo>
{
    public override CultureInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.GetString() is string culture)
            return CultureInfo.GetCultureInfo(culture);
        else
            return default;
    }

    public override void Write(Utf8JsonWriter writer, CultureInfo value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

public class RegionInfoJsonConverter : JsonConverter<RegionInfo>
{
    public override RegionInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.GetString() is string region)
            return new RegionInfo(region);
        else
            return default;
    }

    public override void Write(Utf8JsonWriter writer, RegionInfo value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Name);
    }
}