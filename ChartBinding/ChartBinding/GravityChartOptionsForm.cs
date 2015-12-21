using System;
using System.Windows.Forms;

namespace ChartBinding
{
    public partial class GravityChartOptionsForm : Form
    {
        private Form1 MainForm = new Form1();

        public GravityChartOptionsForm()
        {
            InitializeComponent();
        }

        private void GravityChartOptionsForm_Load(object sender, EventArgs e)
        {
            this.digitalGravityButton.BackColor = Form1.ChartColors.digitalGravity;
            this.springTensionButton.BackColor = Form1.ChartColors.springTension;
            this.crossCouplingButton.BackColor = Form1.ChartColors.crossCoupling;
            this.rawBeamButton.BackColor = Form1.ChartColors.rawBeam;
            this.totalCorrectionButton.BackColor = Form1.ChartColors.totalCorrection;




            this.digitalGravityCheckBox.Checked = MainForm.GravityChart.Series["Digital Gravity"].Enabled;
            this.springTensionCheckBox.Checked = MainForm.GravityChart.Series["Spring Tension"].Enabled;
            this.crossCouplingCheckBox.Checked = MainForm.GravityChart.Series["Cross Coupling"].Enabled;
            this.rawBeamCheckBox.Checked = MainForm.GravityChart.Series["Raw Beam"].Enabled;
            this.totalCorrectionCheckBox.Checked = MainForm.GravityChart.Series["Total Correction"].Enabled;
        }

  

        private void digitalGravityButton_Click(object sender, EventArgs e)
        {
            DialogResult result = gravityChartColorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.digitalGravityButton.BackColor = gravityChartColorDialog.Color;
                Form1.ChartColors.digitalGravity  = gravityChartColorDialog.Color;
            }
            MainForm.SetChartColors();
        }

        private void digitalGravityCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (digitalGravityCheckBox.Checked)
            {
                Form1.ChartVisibility.digitalGravity = true;
            }
            else
            {
                Form1.ChartVisibility.digitalGravity = false;
            }
            
        }

        private void springTensionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (springTensionCheckBox.Checked)
            {
                Form1.ChartVisibility.springTension = true;
            }
            else
            {
                Form1.ChartVisibility.springTension = false;
            }
            MainForm.GravityChart.Update();
        }

        private void crossCouplingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (crossCouplingCheckBox.Checked)
            {
                MainForm.GravityChart.Series["Cross Coupling"].Enabled = true;
            }
            else
            {
                MainForm.GravityChart.Series["Cross Coupling"].Enabled = false;
            }
        }

        private void rawBeamCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (rawBeamCheckBox.Checked)
            {
                MainForm.GravityChart.Series["Raw Beam"].Enabled = true;
            }
            else
            {
                MainForm.GravityChart.Series["Raw Beam"].Enabled = false;
            }
        }

        private void totalCorrectionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (totalCorrectionCheckBox.Checked)
            {
                MainForm.GravityChart.Series["Total Correction"].Enabled = true;
            }
            else
            {
                MainForm.GravityChart.Series["Total Correction"].Enabled = false;
            }
        }

        private void springTensionButton_Click(object sender, EventArgs e)
        {
            DialogResult result = gravityChartColorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.springTensionButton.BackColor = gravityChartColorDialog.Color;
                Form1.ChartColors.springTension = gravityChartColorDialog.Color;
            }
            MainForm.SetChartColors();
        }

        private void crossCouplingButton_Click(object sender, EventArgs e)
        {
            DialogResult result = gravityChartColorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.crossCouplingButton.BackColor = gravityChartColorDialog.Color;
                Form1.ChartColors.crossCoupling = gravityChartColorDialog.Color;
            }
            MainForm.SetChartColors();
        }

        private void rawBeamButton_Click(object sender, EventArgs e)
        {
            DialogResult result = gravityChartColorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.rawBeamButton.BackColor = gravityChartColorDialog.Color;
                Form1.ChartColors.rawBeam = gravityChartColorDialog.Color;
            }
            MainForm.SetChartColors();
        }

        private void totalCorrectionButton_Click(object sender, EventArgs e)
        {
            DialogResult result = gravityChartColorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.totalCorrectionButton.BackColor = gravityChartColorDialog.Color;
                Form1.ChartColors.totalCorrection = gravityChartColorDialog.Color;
            }
            MainForm.SetChartColors();
        }

       

        private void doneButton_Click(object sender, EventArgs e)
        {
            MainForm.SetChartColors();
           MainForm.SetChartVisibility();
            MainForm.GravityChart.Series["Spring Tension"].Color = System.Drawing.Color.Empty;
            this.Hide();
        }
    }
}