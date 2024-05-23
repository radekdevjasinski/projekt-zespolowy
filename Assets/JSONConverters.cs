using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JSONConverters
{
    public  class Vector2IntConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Vector2Int vector = (Vector2Int)value;
            writer.WriteStartObject();
            writer.WritePropertyName("x");
            writer.WriteValue(vector.x);
            writer.WritePropertyName("y");
            writer.WriteValue(vector.y);
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            int x = 0;
            int y = 0;

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string propertyName = (string)reader.Value;

                    if (!reader.Read())
                        continue;

                    switch (propertyName)
                    {
                        case "x":
                            x = (int)(long)reader.Value;
                            break;
                        case "y":
                            y = (int)(long)reader.Value;
                            break;
                    }
                }
                else if (reader.TokenType == JsonToken.EndObject)
                {
                    break;
                }
            }

            return new Vector2Int(x, y);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Vector2Int);
        }
    }
    public class DictionaryVector2IntKeyConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var dictionary = (Dictionary<Vector2Int, DungeonRoomSerializable>)value;
            writer.WriteStartObject();
            foreach (var kvp in dictionary)
            {
                writer.WritePropertyName($"({kvp.Key.x}, {kvp.Key.y})");
                serializer.Serialize(writer, kvp.Value);
            }
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var dictionary = new Dictionary<Vector2Int, DungeonRoomSerializable>();
            reader.Read();

            while (reader.TokenType == JsonToken.PropertyName)
            {
                string propertyName = (string)reader.Value;
                var parts = propertyName.Trim('(', ')').Split(',');
                int x = int.Parse(parts[0].Trim());
                int y = int.Parse(parts[1].Trim());

                reader.Read();
                var value = serializer.Deserialize<DungeonRoomSerializable>(reader);

                dictionary[new Vector2Int(x, y)] = value;
                reader.Read();
            }

            return dictionary;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Dictionary<Vector2Int, DungeonRoomSerializable>);
        }
    }
}
