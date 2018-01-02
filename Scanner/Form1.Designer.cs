namespace Scanner
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbInput = new System.Windows.Forms.GroupBox();
            this.gbOutput = new System.Windows.Forms.GroupBox();
            this.btnScanner = new System.Windows.Forms.Button();
            this.txbInput = new System.Windows.Forms.TextBox();
            this.txbOutput = new System.Windows.Forms.TextBox();
            this.gbInput.SuspendLayout();
            this.gbOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbInput
            // 
            this.gbInput.Controls.Add(this.txbInput);
            this.gbInput.Location = new System.Drawing.Point(2, 3);
            this.gbInput.Name = "gbInput";
            this.gbInput.Size = new System.Drawing.Size(365, 442);
            this.gbInput.TabIndex = 0;
            this.gbInput.TabStop = false;
            this.gbInput.Text = "Input";
            // 
            // gbOutput
            // 
            this.gbOutput.Controls.Add(this.txbOutput);
            this.gbOutput.Location = new System.Drawing.Point(560, 3);
            this.gbOutput.Name = "gbOutput";
            this.gbOutput.Size = new System.Drawing.Size(332, 442);
            this.gbOutput.TabIndex = 1;
            this.gbOutput.TabStop = false;
            this.gbOutput.Text = "Output";
            // 
            // btnScanner
            // 
            this.btnScanner.Location = new System.Drawing.Point(424, 207);
            this.btnScanner.Name = "btnScanner";
            this.btnScanner.Size = new System.Drawing.Size(75, 23);
            this.btnScanner.TabIndex = 2;
            this.btnScanner.Text = "Scan>>";
            this.btnScanner.UseVisualStyleBackColor = true;
            this.btnScanner.Click += new System.EventHandler(this.btnScanner_Click);
            // 
            // txbInput
            // 
            this.txbInput.AcceptsTab = true;
            this.txbInput.Location = new System.Drawing.Point(7, 15);
            this.txbInput.Multiline = true;
            this.txbInput.Name = "txbInput";
            this.txbInput.Size = new System.Drawing.Size(352, 418);
            this.txbInput.TabIndex = 0;
            // 
            // txbOutput
            // 
            this.txbOutput.Location = new System.Drawing.Point(7, 15);
            this.txbOutput.Multiline = true;
            this.txbOutput.Name = "txbOutput";
            this.txbOutput.ReadOnly = true;
            this.txbOutput.Size = new System.Drawing.Size(319, 421);
            this.txbOutput.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 448);
            this.Controls.Add(this.btnScanner);
            this.Controls.Add(this.gbOutput);
            this.Controls.Add(this.gbInput);
            this.Name = "Form1";
            this.Text = "Form1";
            this.gbInput.ResumeLayout(false);
            this.gbInput.PerformLayout();
            this.gbOutput.ResumeLayout(false);
            this.gbOutput.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbInput;
        private System.Windows.Forms.TextBox txbInput;
        private System.Windows.Forms.GroupBox gbOutput;
        private System.Windows.Forms.TextBox txbOutput;
        private System.Windows.Forms.Button btnScanner;
    }
}

