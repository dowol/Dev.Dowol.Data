using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dev.Dowol.Json;

public class IPAddressJsonConverter : JsonConverter<IPAddress>
{
    public override IPAddress? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String && IPAddress.TryParse(reader.GetString(), out IPAddress? result))
            return result;
        else
            return null;
    }

    public override void Write(Utf8JsonWriter writer, IPAddress value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

public class IPEndPointJsonConverter : JsonConverter<IPEndPoint>
{
    public override IPEndPoint? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String && IPEndPoint.TryParse(reader.GetString() ?? "", out IPEndPoint? result))
            return result;
        else
            return null;
            
    }

    public override void Write(Utf8JsonWriter writer, IPEndPoint value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

public class ContentTypeJsonConverter : JsonConverter<ContentType>
{
    private readonly bool throwIfError;
    public ContentTypeJsonConverter(bool throwIfError = false)
    {
        this.throwIfError = throwIfError;
    }

    public override ContentType? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String && reader.GetString() is string contentType)
            try
            {
                return new(contentType);
            }
            catch
            {
                if (throwIfError) throw;
                else return null;
            }
        else
            return null;
    }

    public override void Write(Utf8JsonWriter writer, ContentType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
