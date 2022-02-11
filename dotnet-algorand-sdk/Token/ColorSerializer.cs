using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace Algorand.Token
{
    public class ColorHexConverter : JsonConverter
    {
        
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var color = (Color)value;
            var hexString = color.IsEmpty ? string.Empty : $"#{color.ToArgb() & 0x00FFFFFF:X6}";
            writer.WriteValue(hexString);
        }


        public override bool CanRead => true;
        public override bool CanWrite => true;
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
           
            
            var hexString = reader.Value.ToString();
            if (hexString == null || !hexString.StartsWith("#")) return Color.Empty;
            return Color.FromArgb(int.Parse(hexString.Replace("#", ""),
                         System.Globalization.NumberStyles.AllowHexSpecifier));
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}
