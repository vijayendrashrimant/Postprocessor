using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT500Postprocessor.Data_Classess
{
    public class NovaTechPostprocessor
    {
        private readonly RobotConfig config;
        private readonly double firmware;

        public NovaTechPostprocessor(RobotConfig cfg)
        {
            config = cfg;
            firmware = double.Parse(cfg.FirmwareVersion);
        }

        private double ValidateSpeed(double speed, bool isLinear)
        {
            if (speed <= 0) throw new Exception($"Invalid speed {speed}");
            if (isLinear && speed > 2000) throw new Exception($"Linear speed {speed} too high");
            if (!isLinear && (speed < 1 || speed > 100)) throw new Exception($"Joint speed {speed}% invalid");
            return speed;
        }

        private double NormalizeJoint6(double j6)
        {
            if (j6 < -360 || j6 > 360)
            {
                j6 = ((j6 + 360) % 720) - 360;
            }
            return j6;
        }

        private string FormatSpeed(double speed)
        {
            return firmware < 3.1 ? $"SPD({speed})" : $"SPD={speed}";
        }

        public string Generate()
        {
            var lines = new System.Text.StringBuilder();
            lines.AppendLine($"; Program generated for {config.Robot}");
            lines.AppendLine($"BASE [{string.Join(",", config.BaseFrame)}]");
            lines.AppendLine($"TOOL [{string.Join(",", config.ToolFrame)}]");

            foreach (var step in config.Trajectory)
            {
                if (step.Type == "linear")
                {
                    double speed = ValidateSpeed(step.Speed, true);
                    double acc = step.Acceleration ?? 50;
                    lines.AppendLine(
                        $"MOVL P[{string.Join(",", step.Position)}] {FormatSpeed(speed)} ACC={acc}"
                    );
                }
                else if (step.Type == "joint")
                {
                    double speed = ValidateSpeed(step.Speed, false);
                    step.Joints[5] = NormalizeJoint6(step.Joints[5]);
                    lines.AppendLine(
                        $"MOVJ J[{string.Join(",", step.Joints)}] {FormatSpeed(speed)}"
                    );
                }
                else
                {
                    lines.AppendLine($"; Warning: Unknown trajectory type {step.Type}");
                }
            }

            return lines.ToString();
        }

    }
}
