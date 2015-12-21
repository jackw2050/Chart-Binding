namespace ChartBinding
{
    partial class SurveyForm
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
            this.surveyTextBox = new System.Windows.Forms.TextBox();
            this.surveyLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // surveyTextBox
            // 
            this.surveyTextBox.Location = new System.Drawing.Point(15, 37);
            this.surveyTextBox.Name = "surveyTextBox";
            this.surveyTextBox.Size = new System.Drawing.Size(202, 20);
            this.surveyTextBox.TabIndex = 0;
            // 
            // surveyLabel
            // 
            this.surveyLabel.AutoSize = true;
            this.surveyLabel.Location = new System.Drawing.Point(12, 9);
            this.surveyLabel.Name = "surveyLabel";
            this.surveyLabel.Size = new System.Drawing.Size(95, 13);
            this.surveyLabel.TabIndex = 1;
            this.surveyLabel.Text = "Enter survey name";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(85, 96);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Done";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SurveyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 171);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.surveyLabel);
            this.Controls.Add(this.surveyTextBox);
            this.Name = "SurveyForm";
            this.Text = "Survey Information";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox surveyTextBox;
        private System.Windows.Forms.Label surveyLabel;
        private System.Windows.Forms.Button button1;
    }
}