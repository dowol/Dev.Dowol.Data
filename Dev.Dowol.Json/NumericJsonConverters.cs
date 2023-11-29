using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dev.Dowol.Json;

internal static class JsonIntRange
{
    internal static long Max => 9007199254740991;
    internal static long Min => -9007199254740992;

}

public class BigIntegerJsonConverter : JsonConverter<BigInteger>
{
    public override BigInteger Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt64(out long value))
            return value;
        else if (reader.TokenType == JsonTokenType.String && BigInteger.TryParse(reader.GetString(), out BigInteger value_bi))
            return value_bi;
        else return default;
    }

    public override void Write(Utf8JsonWriter writer, BigInteger value, JsonSerializerOptions options)
    {
        if (value < JsonIntRange.Min || value > JsonIntRange.Max)
            writer.WriteStringValue(value.ToString());
        else
            writer.WriteNumberValue((long)value);
    }
}

public class UInt64JsonConverter : JsonConverter<ulong>
{
    public override ulong Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number && reader.TryGetUInt64(out ulong value))
            return value;
        else if (reader.TokenType == JsonTokenType.String && ulong.TryParse(reader.GetString(), out value))
            return value;
        else
            return default;
    }

    public override void Write(Utf8JsonWriter writer, ulong value, JsonSerializerOptions options)
    {
        if (value > (ulong)JsonIntRange.Max)
            writer.WriteStringValue(value.ToString());
        else
            writer.WriteNumberValue(value);
    }
}

public class Int64JsonConverter : JsonConverter<long>
{
    public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt64(out long value))
            return value;
        else if (reader.TokenType == JsonTokenType.String && long.TryParse(reader.GetString(), out value))
            return value;
        else
            return default;
    }

    public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
    {
        if (value > JsonIntRange.Max || value < JsonIntRange.Min)
            writer.WriteStringValue(value.ToString());
        else
            writer.WriteNumberValue(value);
    }
}

