import streamlit as st
import json
import math
import io

# -------------------------------
# Postprocessor Class
# -------------------------------
class NovaTechPostprocessor:
    def __init__(self, robot_config, verbose=False):
        self.robot = robot_config["robot"]
        self.firmware = float(robot_config["firmware_version"])
        self.base = robot_config["base_frame"]
        self.tool = robot_config["tool_frame"]
        self.trajectory = robot_config["trajectory"]
        self.verbose = verbose
        self.logs = []

    def log(self, msg):
        if self.verbose:
            self.logs.append(msg)

    def validate_speed(self, speed, is_linear=True):
        if speed <= 0:
            raise ValueError(f"Invalid speed {speed}: must be positive")
        if is_linear and speed > 2000:
            raise ValueError(f"Linear speed {speed} exceeds safe limit")
        if not is_linear and (speed < 1 or speed > 100):
            raise ValueError(f"Joint speed {speed}% must be between 1-100")
        return speed

    def normalize_joint6(self, j6):
        if j6 < -360 or j6 > 360:
            normalized = ((j6 + 360) % 720) - 360
            self.log(f"Joint 6 normalized from {j6} to {normalized}")
            return normalized
        return j6

    def format_speed(self, speed):
        if self.firmware < 3.1:
            return f"SPD({speed})"
        return f"SPD={speed}"

    def generate(self):
        lines = []
        lines.append(f"; Program generated for {self.robot}")
        lines.append(f"BASE {self.base}")
        lines.append(f"TOOL {self.tool}")

        for step in self.trajectory:
            if step["type"] == "linear":
                pos = step["position"]
                speed = self.validate_speed(step["speed"], is_linear=True)
                acc = step.get("acceleration", 50)
                cmd = (
                    f"MOVL P[{','.join(map(str,pos))}] "
                    f"{self.format_speed(speed)} ACC={acc}"
                )
                lines.append(cmd)

            elif step["type"] == "joint":
                joints = step["joints"]
                joints[5] = self.normalize_joint6(joints[5])
                speed = self.validate_speed(step["speed"], is_linear=False)
                cmd = (
                    f"MOVJ J[{','.join(map(str,joints))}] "
                    f"{self.format_speed(speed)}"
                )
                lines.append(cmd)

            else:
                self.log(f"Unknown trajectory type: {step['type']}")

        return "\n".join(lines), self.logs


# -------------------------------
# Streamlit UI
# -------------------------------
st.title("NovaTech RT-500 Postprocessor")

uploaded_file = st.file_uploader("Upload trajectory JSON", type="json")
verbose = st.checkbox("Enable verbose logging")

if uploaded_file:
    try:
        data = json.load(uploaded_file)
        st.subheader("Trajectory Data")
        st.json(data)

        post = NovaTechPostprocessor(data, verbose=verbose)
        program, logs = post.generate()

        st.subheader("Generated Program")
        st.text_area("RT-500 Program", program, height=300)

        # Download button
        prg_bytes = program.encode("utf-8")
        st.download_button(
            label="Download Program File",
            data=prg_bytes,
            file_name="output_rt500.prg",
            mime="text/plain"
        )

        if logs:
            st.subheader("Logs")
            for log in logs:
                st.write(f"- {log}")

    except Exception as e:
        st.error(f"Error processing file: {e}")