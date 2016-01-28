namespace ChartBinding
{
    partial class RecordingForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.hourlyComboBox = new System.Windows.Forms.ComboBox();
            this.dailyComboBox = new System.Windows.Forms.ComboBox();
            this.refreshTimeComboBox = new System.Windows.Forms.ComboBox();
            this.userDurationPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.dailyRefreshPanel = new System.Windows.Forms.Panel();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.userDurationPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.dailyRefreshPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(277, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "You can start and stop recording files manually or\r\n have this  software open a n" +
    "ew file a a specified interval.";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(13, 16);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(60, 17);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Manual";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(13, 39);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(99, 17);
            this.radioButton2.TabIndex = 2;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "New File Hourly";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(13, 62);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(92, 17);
            this.radioButton3.TabIndex = 3;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "New File Daily";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(13, 85);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(123, 17);
            this.radioButton4.TabIndex = 4;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "User defined Interval";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.radioButton4);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Location = new System.Drawing.Point(31, 74);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 123);
            this.panel1.TabIndex = 5;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint_1);
            // 
            // hourlyComboBox
            // 
            this.hourlyComboBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.hourlyComboBox.FormattingEnabled = true;
            this.hourlyComboBox.Items.AddRange(new object[] {
            "From Start Time",
            "On The Hour"});
            this.hourlyComboBox.Location = new System.Drawing.Point(274, 109);
            this.hourlyComboBox.Name = "hourlyComboBox";
            this.hourlyComboBox.Size = new System.Drawing.Size(158, 21);
            this.hourlyComboBox.TabIndex = 6;
            this.hourlyComboBox.Text = "Specify File Refresh Rate";
            // 
            // dailyComboBox
            // 
            this.dailyComboBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dailyComboBox.FormattingEnabled = true;
            this.dailyComboBox.Items.AddRange(new object[] {
            "24 Hours From Start Time",
            "At Midnight",
            "At Other Time"});
            this.dailyComboBox.Location = new System.Drawing.Point(274, 137);
            this.dailyComboBox.Name = "dailyComboBox";
            this.dailyComboBox.Size = new System.Drawing.Size(158, 21);
            this.dailyComboBox.TabIndex = 7;
            this.dailyComboBox.Text = "Specify File Refresh Rate";
            this.dailyComboBox.SelectedIndexChanged += new System.EventHandler(this.dailyComboBox_SelectedIndexChanged);
            // 
            // refreshTimeComboBox
            // 
            this.refreshTimeComboBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.refreshTimeComboBox.FormattingEnabled = true;
            this.refreshTimeComboBox.Items.AddRange(new object[] {
            "0100",
            "0200",
            "0300",
            "0400",
            "0500",
            "0600",
            "0700",
            "0800",
            "0900",
            "1000",
            "1100",
            "1200",
            "1300",
            "1400",
            "1500",
            "1600",
            "1700",
            "1800",
            "1900",
            "2000",
            "2100",
            "2200",
            "2300",
            "3400"});
            this.refreshTimeComboBox.Location = new System.Drawing.Point(274, 165);
            this.refreshTimeComboBox.Name = "refreshTimeComboBox";
            this.refreshTimeComboBox.Size = new System.Drawing.Size(158, 21);
            this.refreshTimeComboBox.TabIndex = 8;
            this.refreshTimeComboBox.Text = "Specify Time To Refresh File";
            this.refreshTimeComboBox.SelectedIndexChanged += new System.EventHandler(this.refreshTimeComboBox_SelectedIndexChanged);
            // 
            // userDurationPanel
            // 
            this.userDurationPanel.Controls.Add(this.numericUpDown3);
            this.userDurationPanel.Controls.Add(this.numericUpDown2);
            this.userDurationPanel.Controls.Add(this.numericUpDown1);
            this.userDurationPanel.Controls.Add(this.label5);
            this.userDurationPanel.Controls.Add(this.label4);
            this.userDurationPanel.Controls.Add(this.label3);
            this.userDurationPanel.Controls.Add(this.label2);
            this.userDurationPanel.Location = new System.Drawing.Point(31, 216);
            this.userDurationPanel.Name = "userDurationPanel";
            this.userDurationPanel.Size = new System.Drawing.Size(200, 127);
            this.userDurationPanel.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Please indicate file duration";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(99, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Days";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(95, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Hours";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(86, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Minutes";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(136, 37);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(47, 20);
            this.numericUpDown1.TabIndex = 10;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(136, 60);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(47, 20);
            this.numericUpDown2.TabIndex = 11;
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(136, 83);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(47, 20);
            this.numericUpDown3.TabIndex = 12;
            // 
            // dailyRefreshPanel
            // 
            this.dailyRefreshPanel.Controls.Add(this.label7);
            this.dailyRefreshPanel.Controls.Add(this.label6);
            this.dailyRefreshPanel.Controls.Add(this.numericUpDown5);
            this.dailyRefreshPanel.Controls.Add(this.numericUpDown4);
            this.dailyRefreshPanel.Location = new System.Drawing.Point(274, 196);
            this.dailyRefreshPanel.Name = "dailyRefreshPanel";
            this.dailyRefreshPanel.Size = new System.Drawing.Size(158, 100);
            this.dailyRefreshPanel.TabIndex = 10;
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Location = new System.Drawing.Point(16, 20);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(47, 20);
            this.numericUpDown4.TabIndex = 12;
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.Location = new System.Drawing.Point(16, 52);
            this.numericUpDown5.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(47, 20);
            this.numericUpDown5.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(84, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Hours";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(84, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Minutes";
            // 
            // RecordingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 430);
            this.Controls.Add(this.dailyRefreshPanel);
            this.Controls.Add(this.userDurationPanel);
            this.Controls.Add(this.refreshTimeComboBox);
            this.Controls.Add(this.dailyComboBox);
            this.Controls.Add(this.hourlyComboBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "RecordingForm";
            this.Text = "File Options";
            this.Load += new System.EventHandler(this.RecordingForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.userDurationPanel.ResumeLayout(false);
            this.userDurationPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.dailyRefreshPanel.ResumeLayout(false);
            this.dailyRefreshPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox hourlyComboBox;
        private System.Windows.Forms.ComboBox dailyComboBox;
        private System.Windows.Forms.ComboBox refreshTimeComboBox;
        private System.Windows.Forms.Panel userDurationPanel;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel dailyRefreshPanel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown5;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
    }
}