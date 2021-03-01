using System;
using System.Text.Json.Serialization;

namespace BlazorSignals.Shared
{
    public class Pulse
    {
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("value")]
        public double Value { get; set; }

        public override string ToString()
        {
            return $"Measurement (Timestamp = {Timestamp:HH:mm:ss}, Value = {Value:0.00000})";
        }
    }
}
