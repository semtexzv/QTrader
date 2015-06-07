namespace QLib
{
    partial class TesterUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.StartDateTime = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.StartStopButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ConfDataTB = new System.Windows.Forms.RichTextBox();
            this.ProfitLabel = new System.Windows.Forms.Label();
            this.PPMLabel = new System.Windows.Forms.Label();
            this.GenLabel = new System.Windows.Forms.Label();
            this.ApplyConfig = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StartDateTime
            // 
            this.StartDateTime.CustomFormat = "dd.MM.yyyy";
            this.StartDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.StartDateTime.Location = new System.Drawing.Point(6, 16);
            this.StartDateTime.Name = "StartDateTime";
            this.StartDateTime.Size = new System.Drawing.Size(93, 20);
            this.StartDateTime.TabIndex = 4;
            this.StartDateTime.Value = new System.DateTime(2015, 1, 1, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Trade Since";
            // 
            // StartStopButton
            // 
            this.StartStopButton.Location = new System.Drawing.Point(6, 42);
            this.StartStopButton.Name = "StartStopButton";
            this.StartStopButton.Size = new System.Drawing.Size(93, 23);
            this.StartStopButton.TabIndex = 8;
            this.StartStopButton.Text = "Start Simulation";
            this.StartStopButton.UseVisualStyleBackColor = true;
            this.StartStopButton.Click += new System.EventHandler(this.StartStopButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(183, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Best Settings stats:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(122, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Profit:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(122, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Profit per month";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(183, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Configuration data:";
            // 
            // ConfDataTB
            // 
            this.ConfDataTB.Location = new System.Drawing.Point(105, 78);
            this.ConfDataTB.Name = "ConfDataTB";
            this.ConfDataTB.ReadOnly = true;
            this.ConfDataTB.Size = new System.Drawing.Size(229, 88);
            this.ConfDataTB.TabIndex = 13;
            this.ConfDataTB.Text = "";
            // 
            // ProfitLabel
            // 
            this.ProfitLabel.AutoSize = true;
            this.ProfitLabel.Location = new System.Drawing.Point(245, 16);
            this.ProfitLabel.Name = "ProfitLabel";
            this.ProfitLabel.Size = new System.Drawing.Size(25, 13);
            this.ProfitLabel.TabIndex = 14;
            this.ProfitLabel.Text = "000";
            // 
            // PPMLabel
            // 
            this.PPMLabel.AutoSize = true;
            this.PPMLabel.Location = new System.Drawing.Point(245, 29);
            this.PPMLabel.Name = "PPMLabel";
            this.PPMLabel.Size = new System.Drawing.Size(25, 13);
            this.PPMLabel.TabIndex = 15;
            this.PPMLabel.Text = "000";
            // 
            // GenLabel
            // 
            this.GenLabel.AutoSize = true;
            this.GenLabel.Location = new System.Drawing.Point(122, 42);
            this.GenLabel.Name = "GenLabel";
            this.GenLabel.Size = new System.Drawing.Size(36, 13);
            this.GenLabel.TabIndex = 16;
            this.GenLabel.Text = "Gen : ";
            // 
            // ApplyConfig
            // 
            this.ApplyConfig.Location = new System.Drawing.Point(4, 102);
            this.ApplyConfig.Name = "ApplyConfig";
            this.ApplyConfig.Size = new System.Drawing.Size(95, 64);
            this.ApplyConfig.TabIndex = 17;
            this.ApplyConfig.Text = "Apply configuration";
            this.ApplyConfig.UseVisualStyleBackColor = true;
            this.ApplyConfig.Click += new System.EventHandler(this.ApplyConfig_Click);
            // 
            // TesterUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ApplyConfig);
            this.Controls.Add(this.GenLabel);
            this.Controls.Add(this.PPMLabel);
            this.Controls.Add(this.ProfitLabel);
            this.Controls.Add(this.ConfDataTB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.StartStopButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StartDateTime);
            this.Name = "TesterUserControl";
            this.Size = new System.Drawing.Size(343, 216);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker StartDateTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button StartStopButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox ConfDataTB;
        private System.Windows.Forms.Label ProfitLabel;
        private System.Windows.Forms.Label PPMLabel;
        private System.Windows.Forms.Label GenLabel;
        private System.Windows.Forms.Button ApplyConfig;
    }
}
