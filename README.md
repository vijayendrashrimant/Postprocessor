# NovaTech RT-500 Postprocessor

## Overview
This project implements a **trajectory postprocessor** for the fictional NovaTech RT‑500 industrial robot.  
It converts motion trajectory JSON files into valid NovaTech RT‑500 program code (`.prg`), handling firmware quirks, defaults, and edge cases.  

The tool is available in multiple forms:
- **Streamlit UI** — browser-based interface for interactive uploads and downloads.
- **C# WinForms GUI** — desktop application with file browsing and logging panel.

---

## Features
- ✅ Parse trajectory JSON files (linear and joint moves).
- ✅ Generate valid NovaTech RT‑500 program syntax (`MOVL`, `MOVJ`).
- ✅ Handle firmware differences (`SPD(v)` vs `SPD=v`).
- ✅ Apply defaults (acceleration = 50 if missing).
- ✅ Normalize Joint 6 values to respect 720° range.
- ✅ Validate speed ranges and reject invalid values.
- ✅ Logging of warnings and errors (CLI, Streamlit, WinForms).
- ✅ Downloadable `.prg` output file.
- ⚡ Optional trajectory optimization (collinear waypoint compression).

---

## Input Format
Example JSON trajectory:

```json
{
  "robot": "NovaTech RT-500",
  "firmware_version": "3.2",
  "base_frame": [0, 0, 0, 0, 0, 0],
  "tool_frame": [0, 0, 150, 0, 0, 0],
  "trajectory": [
    {
      "type": "linear",
      "position": [500.0, 200.0, 300.0, 0.0, 90.0, 0.0],
      "speed": 100,
      "acceleration": 75
    },
    {
      "type": "joint",
      "joints": [45.0, -30.0, 60.0, 0.0, 45.0, 180.0],
      "speed": 50
    },
    {
      "type": "linear",
      "position": [600.0, 250.0, 100.0, 45.0, 90.0, -45.0],
      "speed": 200
    }
  ]
}
```


## Output Format

Generated program file (output_rt500.prg):

````
; Program generated for NovaTech RT-500
BASE [0,0,0,0,0,0]
TOOL [0,0,150,0,0,0]
MOVL P[500.0,200.0,300.0,0.0,90.0,0.0] SPD=100 ACC=75
MOVJ J[45.0,-30.0,60.0,0.0,45.0,180.0] SPD=50
MOVL P[600.0,250.0,100.0,45.0,90.0,-45.0] SPD=200 ACC=50

````

---
## Usage
Python CLI

````
python rt500_post.py -i sample_trajectory.json -o output_rt500.prg
# Verbose logging
python rt500_post.py -i sample_trajectory.json -o output_rt500.prg -v
````

---

Exit codes:

0 → success

1 → invalid input file

2 → validation error

3 → unknown error

---

## Streamlit UI

````

streamlit run rt500_streamlit.py

````
Upload trajectory JSON via browser.

View the parsed data and generated program.

Download .prg file.

Enable verbose logging with the checkbox.

---

## C# WinForms GUI
Open the solution in Visual Studio.
Run Mainorm.
Browse for input JSON and output file.

Click Generate Program.

View generated code in RichTextBox.

Logs and warnings appear in the ListBox panel.

---

## Known Limitations
Speed limits are assumed (2000 mm/s for linear, 1–100% for joint).

---

## ⚠️ Ambiguities & Missing Info
SPD limits: Max/min speed values not documented (what’s “reasonable”?).

Acceleration units: % of what? Max acceleration per axis?

Tool/Base precedence: Is TOOL additive to BASE or replaces it?

Gripper commands: Missing entirely (page torn).

Error handling: What happens if SPD or ACC is omitted entirely?

---

## ❓ Questions for Manufacturer
What are the valid ranges for SPD in both linear and joint modes?

Is the acceleration percentage relative to the robot’s max acceleration or a fixed scale?

How does the TOOL frame interact with BASE — additive transform or override?

For firmware ≥3.1, is SPD=v always required, or can it be omitted?

How should we handle J6 values beyond 720° — wraparound or error?

Are gripper commands part of the motion or a separate channel?

Gripper commands are missing (documentation incomplete).

TOOL vs BASE precedence may need clarification from the vendor.


