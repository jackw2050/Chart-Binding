namespace ChartBinding
{
    partial class SwitchesForm
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
            this.SwitchesAlarmsCheckBox = new System.Windows.Forms.CheckBox();
            this.SwitchesSpringTensionCheckBox = new System.Windows.Forms.CheckBox();
            this.SwitchesTorqueMotorsCheckBox = new System.Windows.Forms.CheckBox();
            this.switchesGyroCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.doneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SwitchesAlarmsCheckBox
            // 
            this.SwitchesAlarmsCheckBox.AutoSize = true;
            this.SwitchesAlarmsCheckBox.Location = new System.Drawing.Point(12, 133);
            this.SwitchesAlarmsCheckBox.Name = "SwitchesAlarmsCheckBox";
            this.SwitchesAlarmsCheckBox.Size = new System.Drawing.Size(80, 17);
            this.SwitchesAlarmsCheckBox.TabIndex = 9;
            this.SwitchesAlarmsCheckBox.Text = "Alarms OFF";
            this.SwitchesAlarmsCheckBox.UseVisualStyleBackColor = true;
            // 
            // SwitchesSpringTensionCheckBox
            // 
            this.SwitchesSpringTensionCheckBox.AutoSize = true;
            this.SwitchesSpringTensionCheckBox.Enabled = false;
            this.SwitchesSpringTensionCheckBox.Location = new System.Drawing.Point(12, 110);
            this.SwitchesSpringTensionCheckBox.Name = "SwitchesSpringTensionCheckBox";
            this.SwitchesSpringTensionCheckBox.Size = new System.Drawing.Size(120, 17);
            this.SwitchesSpringTensionCheckBox.TabIndex = 8;
            this.SwitchesSpringTensionCheckBox.Text = "Spring Tension OFF";
            this.SwitchesSpringTensionCheckBox.UseVisualStyleBackColor = true;
            // 
            // SwitchesTorqueMotorsCheckBox
            // 
            this.SwitchesTorqueMotorsCheckBox.AutoSize = true;
            this.SwitchesTorqueMotorsCheckBox.Enabled = false;
            this.SwitchesTorqueMotorsCheckBox.Location = new System.Drawing.Point(12, 87);
            this.SwitchesTorqueMotorsCheckBox.Name = "SwitchesTorqueMotorsCheckBox";
            this.SwitchesTorqueMotorsCheckBox.Size = new System.Drawing.Size(118, 17);
            this.SwitchesTorqueMotorsCheckBox.TabIndex = 7;
            this.SwitchesTorqueMotorsCheckBox.Text = "Torque Motors OFF";
            this.SwitchesTorqueMotorsCheckBox.UseVisualStyleBackColor = true;
            // 
            // switchesGyroCheckBox
            // 
            this.switchesGyroCheckBox.AutoSize = true;
            this.switchesGyroCheckBox.Location = new System.Drawing.Point(12, 64);
            this.switchesGyroCheckBox.Name = "switchesGyroCheckBox";
            this.switchesGyroCheckBox.Size = new System.Drawing.Size(111, 17);
            this.switchesGyroCheckBox.TabIndex = 6;
            this.switchesGyroCheckBox.Text = "Gyro (200Hz) OFF";
            this.switchesGyroCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(148, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Please wait for gyros to spin up";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.doneToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(348, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // doneToolStripMenuItem
            // 
            this.doneToolStripMenuItem.Name = "doneToolStripMenuItem";
            this.doneToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.doneToolStripMenuItem.Text = "Done";
            this.doneToolStripMenuItem.Click += new System.EventHandler(this.doneToolStripMenuItem_Click);
            // 
            // SwitchesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 207);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SwitchesAlarmsCheckBox);
            this.Controls.Add(this.SwitchesSpringTensionCheckBox);
            this.Controls.Add(this.SwitchesTorqueMotorsCheckBox);
            this.Controls.Add(this.switchesGyroCheckBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SwitchesForm";
            this.Text = "Manual Startup";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox SwitchesAlarmsCheckBox;
        private System.Windows.Forms.CheckBox SwitchesSpringTensionCheckBox;
        private System.Windows.Forms.CheckBox SwitchesTorqueMotorsCheckBox;
        private System.Windows.Forms.CheckBox switchesGyroCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem doneToolStripMenuItem;
    }
}