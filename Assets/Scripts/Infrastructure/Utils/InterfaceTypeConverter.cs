using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Utils
{
    public class InterfaceTypeConverter<TInterface> : JsonConverter
    {
        private static readonly Dictionary<string, Type> TypeCache;

        static InterfaceTypeConverter()
        {
            TypeCache = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(TInterface).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToDictionary(t => t.Name, t => t);
        }

        public override bool CanConvert(Type objectType) => typeof(TInterface).IsAssignableFrom(objectType);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var jsonObject = JObject.Load(reader);
            if (jsonObject.TryGetValue("Type", out var typeToken))
            {
                var typeName = typeToken.Value<string>();
                if (TypeCache.TryGetValue(typeName, out var concreteType))
                {
                    var instance = Activator.CreateInstance(concreteType);
                    serializer.Populate(jsonObject.CreateReader(), instance);
                    return instance;
                }
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var safeSerializer = new JsonSerializer
            {
                Formatting = serializer.Formatting,
                TypeNameHandling = serializer.TypeNameHandling
            };

            foreach (var conv in serializer.Converters)
            {
                if (conv != this)
                    safeSerializer.Converters.Add(conv);
            }

            var jsonObject = JObject.FromObject(value, safeSerializer);
            jsonObject.AddFirst(new JProperty("Type", value.GetType().Name));
            jsonObject.WriteTo(writer);
        }
    }
}