using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RT500Postprocessor.Data_Classess
{
    public class RobotConfig
    {
        [JsonPropertyName("robot")] public string Robot { get; set; }
        [JsonPropertyName("firmware_version")] public string FirmwareVersion { get; set; }
        [JsonPropertyName("base_frame")] public double[] BaseFrame { get; set; }
        [JsonPropertyName("tool_frame")] public double[] ToolFrame { get; set; }
        [JsonPropertyName("trajectory")] public TrajectoryStep[] Trajectory { get; set; }

    }
}
