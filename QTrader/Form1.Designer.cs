namespace QLib
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
            this.LogTB = new System.Windows.Forms.RichTextBox();
            this.ExchangesCB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ExchangeControlPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.BotsCB = new System.Windows.Forms.ComboBox();
            this.StartTradingButton = new System.Windows.Forms.Button();
            this.testerUserControl1 = new QLib.TesterUserControl();
            this.SuspendLayout();
            // 
            // LogTB
            // 
            this.LogTB.Location = new System.Drawing.Point(12, 12);
            this.LogTB.Name = "LogTB";
            this.LogTB.ReadOnly = true;
            this.LogTB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.LogTB.Size = new System.Drawing.Size(340, 180);
            this.LogTB.TabIndex = 0;
            this.LogTB.Text = "";
            // 
            // ExchangesCB
            // 
            this.ExchangesCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ExchangesCB.FormattingEnabled = true;
            this.ExchangesCB.Location = new System.Drawing.Point(75, 198);
            this.ExchangesCB.Name = "ExchangesCB";
            this.ExchangesCB.Size = new System.Drawing.Size(116, 21);
            this.ExchangesCB.TabIndex = 1;
            this.ExchangesCB.SelectedValueChanged += new System.EventHandler(this.ExchangesCB_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 201);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Exchanges";
            // 
            // ExchangeControlPanel
            // 
            this.ExchangeControlPanel.Location = new System.Drawing.Point(12, 224);
            this.ExchangeControlPanel.Name = "ExchangeControlPanel";
            this.ExchangeControlPanel.Size = new System.Drawing.Size(340, 240);
            this.ExchangeControlPanel.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(197, 201);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Bots";
            // 
            // BotsCB
            // 
            this.BotsCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BotsCB.FormattingEnabled = true;
            this.BotsCB.Location = new System.Drawing.Point(231, 198);
            this.BotsCB.Name = "BotsCB";
            this.BotsCB.Size = new System.Drawing.Size(121, 21);
            this.BotsCB.TabIndex = 5;
            this.BotsCB.SelectedIndexChanged += new System.EventHandler(this.BotsCB_SelectedIndexChanged);
            // 
            // StartTradingButton
            // 
            this.StartTradingButton.Location = new System.Drawing.Point(358, 438);
            this.StartTradingButton.Name = "StartTradingButton";
            this.StartTradingButton.Size = new System.Drawing.Size(106, 23);
            this.StartTradingButton.TabIndex = 7;
            this.StartTradingButton.Text = "Start Trading";
            this.StartTradingButton.UseVisualStyleBackColor = true;
            this.StartTradingButton.Click += new System.EventHandler(this.StartTradingButton_Click);
            // 
            // testerUserControl1
            // 
            this.testerUserControl1.Location = new System.Drawing.Point(358, 12);
            this.testerUserControl1.Name = "testerUserControl1";
            this.testerUserControl1.Size = new System.Drawing.Size(343, 180);
            this.testerUserControl1.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 473);
            this.Controls.Add(this.StartTradingButton);
            this.Controls.Add(this.testerUserControl1);
            this.Controls.Add(this.BotsCB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ExchangeControlPanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ExchangesCB);
            this.Controls.Add(this.LogTB);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox LogTB;
        private System.Windows.Forms.ComboBox ExchangesCB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel ExchangeControlPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox BotsCB;
        private TesterUserControl testerUserControl1;
        private System.Windows.Forms.Button StartTradingButton;
    }
}

