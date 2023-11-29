using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dev.Dowol.Json;


public class ColorJsonConverter : JsonConverter<Color>
{
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.GetString() is string str)
        {
            if (Enum.TryParse(str, true, out KnownColor color))
                return Color.FromKnownColor(color);
            else if (str is not null && Regex.IsMatch(str, "^#[0-9a-f]{3,8}$", RegexOptions.IgnoreCase))
            {
                return (str.Length - 1) switch
                {
                    3 or 6 => ColorTranslator.FromHtml(str),
                    8 => ColorTranslator.FromHtml('#' + str[7..9] + str[1..7]),
                    _ => default,
                };
            }
            else return default;
        }
        else return default;
    }

    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        if (value.IsKnownColor)
            writer.WriteStringValue(value.Name.ToLowerInvariant());
        else if (value.A == 255)
            ColorTranslator.ToHtml(value);
        else
            writer.WriteStringValue($"#{value.R:X2}{value.G:X2}{value.B:X2}{value.A:X2}");
    }
}
