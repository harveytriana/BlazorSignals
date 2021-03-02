using System;
using System.Text.Json.Serialization;

namespace BlazorSignals.Shared
{
    public class Pulse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("value")]
        public double Value { get; set; }

        public override string ToString()
        {
            return $"{Id} {Timestamp:HH:mm:ss} {Value:0.00000})";
        }
    }
}
