namespace RT500Postprocessor
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnBrowseInput;
        private System.Windows.Forms.Button btnBrowseOutput;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.RichTextBox txtOutputProgram;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.ListBox lstLog;   // NEW log panel


        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtInput = new TextBox();
            txtOutput = new TextBox();
            btnBrowseInput = new Button();
            btnBrowseOutput = new Button();
            btnProcess = new Button();
            txtOutputProgram = new RichTextBox();
            lblStatus = new Label();
            lblInput = new Label();
            lblOutput = new Label();
            lstLog = new ListBox();
            SuspendLayout();
            // 
            // txtInput
            // 
            txtInput.Location = new Point(165, 15);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(300, 27);
            txtInput.TabIndex = 1;
            // 
            // txtOutput
            // 
            txtOutput.Location = new Point(165, 62);
            txtOutput.Name = "txtOutput";
            txtOutput.Size = new Size(300, 27);
            txtOutput.TabIndex = 4;
            // 
            // btnBrowseInput
            // 
            btnBrowseInput.Location = new Point(497, 15);
            btnBrowseInput.Name = "btnBrowseInput";
            btnBrowseInput.Size = new Size(75, 27);
            btnBrowseInput.TabIndex = 2;
            btnBrowseInput.Text = "Browse...";
            btnBrowseInput.UseVisualStyleBackColor = true;
            btnBrowseInput.Click += btnBrowseInput_Click;
            // 
            // btnBrowseOutput
            // 
            btnBrowseOutput.Location = new Point(497, 59);
            btnBrowseOutput.Name = "btnBrowseOutput";
            btnBrowseOutput.Size = new Size(75, 33);
            btnBrowseOutput.TabIndex = 5;
            btnBrowseOutput.Text = "Browse...";
            btnBrowseOutput.UseVisualStyleBackColor = true;
            btnBrowseOutput.Click += btnBrowseOutput_Click;
            // 
            // btnProcess
            // 
            btnProcess.Location = new Point(165, 107);
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new Size(100, 30);
            btnProcess.TabIndex = 6;
            btnProcess.Text = "Generate Program";
            btnProcess.UseVisualStyleBackColor = true;
            btnProcess.Click += btnProcess_Click;
            // 
            // txtOutputProgram
            // 
            txtOutputProgram.Font = new Font("Consolas", 9F);
            txtOutputProgram.Location = new Point(12, 164);
            txtOutputProgram.Name = "txtOutputProgram";
            txtOutputProgram.ReadOnly = true;
            txtOutputProgram.Size = new Size(470, 250);
            txtOutputProgram.TabIndex = 7;
            txtOutputProgram.Text = "";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 417);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(97, 20);
            lblStatus.TabIndex = 8;
            lblStatus.Text = "Status: Ready";
            // 
            // lblInput
            // 
            lblInput.AutoSize = true;
            lblInput.Location = new Point(12, 15);
            lblInput.Name = "lblInput";
            lblInput.Size = new Size(112, 20);
            lblInput.TabIndex = 0;
            lblInput.Text = "Input JSON File:";
            // 
            // lblOutput
            // 
            lblOutput.AutoSize = true;
            lblOutput.Location = new Point(12, 69);
            lblOutput.Name = "lblOutput";
            lblOutput.Size = new Size(119, 20);
            lblOutput.TabIndex = 3;
            lblOutput.Text = "Output Program:";
            // 
            // lstLog
            // 
            lstLog.Font = new Font("Consolas", 9F);
            lstLog.Location = new Point(15, 330);
            lstLog.Name = "lstLog";
            lstLog.Size = new Size(470, 100);
            lstLog.TabIndex = 0;
            // 
            // MainForm
            // 
            ClientSize = new Size(693, 497);
            Controls.Add(lblInput);
            Controls.Add(txtInput);
            Controls.Add(btnBrowseInput);
            Controls.Add(lblOutput);
            Controls.Add(txtOutput);
            Controls.Add(btnBrowseOutput);
            Controls.Add(btnProcess);
            Controls.Add(txtOutputProgram);
            Controls.Add(lblStatus);
            Name = "MainForm";
            Text = "NovaTech RT-500 Postprocessor";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
