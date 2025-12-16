using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RT500Postprocessor.Data_Classess
{
    public class TrajectoryStep
    {
        [JsonPropertyName("type")] public string Type { get; set; }
        [JsonPropertyName("position")] public double[] Position { get; set; }
        [JsonPropertyName("joints")] public double[] Joints { get; set; }
        [JsonPropertyName("speed")] public double Speed { get; set; }
        [JsonPropertyName("acceleration")] public double? Acceleration { get; set; }

    }
}
