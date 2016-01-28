using System;
using System.Windows.Forms;

namespace ChartBinding
{
    public partial class RecordingForm : Form
    {
        public RecordingForm()
        {
            InitializeComponent();
        }

        private void RecordingForm_Load(object sender, EventArgs e)
        {
            hourlyComboBox.Visible = false;
            dailyComboBox.Visible = false;
            refreshTimeComboBox.Visible = false;
            userDurationPanel.Visible = false;
            dailyRefreshPanel.Visible = false;

        }

  


        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            hourlyComboBox.Visible = true;
            dailyComboBox.Visible = false;
            refreshTimeComboBox.Visible = false;
            userDurationPanel.Visible = false;
            dailyRefreshPanel.Visible = false;


        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            hourlyComboBox.Visible = false;
            dailyComboBox.Visible = true;
            refreshTimeComboBox.Visible = false;
            userDurationPanel.Visible = false;
            dailyRefreshPanel.Visible = false;


        }

        private void dailyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dailyComboBox.SelectedItem == "At Other Time")
            {
                refreshTimeComboBox.Visible = true;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            userDurationPanel.Visible = true;
            hourlyComboBox.Visible = false;
            dailyComboBox.Visible = false;
            refreshTimeComboBox.Visible = false;
            dailyRefreshPanel.Visible = false;

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            userDurationPanel.Visible = false;
            hourlyComboBox.Visible = false;
            dailyComboBox.Visible = false;
            refreshTimeComboBox.Visible = false;
            dailyRefreshPanel.Visible = false;
        }

        private void refreshTimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (refreshTimeComboBox.SelectedText == "At Other Time")
            {
                dailyRefreshPanel.Visible = true;
            }
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panel1.ClientRectangle, System.Drawing.Color.YellowGreen, ButtonBorderStyle.Solid);

        }



        private void panel1_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }




    }
}