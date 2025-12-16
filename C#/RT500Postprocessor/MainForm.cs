using RT500Postprocessor.Data_Classess;
using System.Text.Json;

namespace RT500Postprocessor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnBrowseInput_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                    txtInput.Text = ofd.FileName;
            }
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Program files (*.prg)|*.prg|All files (*.*)|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                    txtOutput.Text = sfd.FileName;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                string inputPath = txtInput.Text;
                string outputPath = txtOutput.Text;

                if (!File.Exists(inputPath))
                {
                    Log("Input file not found.");
                    return;
                }

                string json = File.ReadAllText(inputPath);
                RobotConfig config = JsonSerializer.Deserialize<RobotConfig>(json);

                var post = new NovaTechPostprocessor(config);
                string program = post.Generate();

                File.WriteAllText(outputPath, program);
                txtOutputProgram.Text = program;
                Log($"Program written to {outputPath}");
            }
            catch (Exception ex)
            {
                Log($"Error: {ex.Message}");
            }
        }

        private void Log(string message)
        {
            lblStatus.Text = message;
        }

    }
}
