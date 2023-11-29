using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dev.Dowol.Json;

public static class JsonExtensions
{
    public static JsonSerializerOptions LoadJsonConverters(this JsonSerializerOptions options)
    {
        IEnumerable<Type> converterTypes = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.IsSubclassOf(typeof(JsonConverter)));

        foreach (Type type in converterTypes)
            if(Activator.CreateInstance(type) as JsonConverter is JsonConverter converter)
                options.Converters.Add(converter);

        return options;
    }
}
