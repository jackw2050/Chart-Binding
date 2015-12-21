using System;
using System.Windows.Forms;

namespace ChartBinding
{
    public partial class GravityChartForm : Form
    {
        public GravityChartForm()
        {
            InitializeComponent();
        }

        private void GravityChartForm_Load(object sender, EventArgs e)
        {
        }

        private void hideFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }



        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.GravityChart.Series["Digital Gravity"].Enabled = false;
            this.GravityChart.Series["Spring Tension"].Enabled = false;
            this.GravityChart.Series["Cross Coupling"].Enabled = false;
            this.GravityChart.Series["Raw Beam"].Enabled = false;
            this.GravityChart.Series["Total Correction"].Enabled = false;
        }

        private void digitalGravityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.GravityChart.Series["Digital Gravity"].Enabled == false)
            {
                this.GravityChart.Series["Digital Gravity"].Enabled = false;
            }
            else
            {
                this.GravityChart.Series["Digital Gravity"].Enabled = false;
            }
        }

        private void springTensionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.GravityChart.Series["Spring Tension"].Enabled == false)
            {
                this.GravityChart.Series["Spring Tension"].Enabled = true;
            }
            else
            {
                this.GravityChart.Series["Spring Tension"].Enabled = false;
            }
        }

        private void crossCouplingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.GravityChart.Series["Cross Coupling"].Enabled == false)
            {
                this.GravityChart.Series["Cross Coupling"].Enabled = true;
            }
            else
            {
                this.GravityChart.Series["Cross Coupling"].Enabled = false;
            }
        }

        private void rawBeamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.GravityChart.Series["Raw Beam"].Enabled == false)
            {
                this.GravityChart.Series["Raw Beam"].Enabled = true; 
            }
            else
            {
                this.GravityChart.Series["Raw Beam"].Enabled = false;
            }

        }

        private void totalCorrectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.GravityChart.Series["Total Correction"].Enabled == false)
            {
              this.GravityChart.Series["Total Correction"].Enabled = true;  
            }
            else
            {
                this.GravityChart.Series["Total Correction"].Enabled = false;
            }
        }

        private void hideFormToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
             this.Hide();
        
        }

        private void allToolStripMenuItem_Click_(object sender, EventArgs e)
        {
            this.GravityChart.Series["Digital Gravity"].Enabled = true;
            this.GravityChart.Series["Spring Tension"].Enabled = true;
            this.GravityChart.Series["Cross Coupling"].Enabled = true;
            this.GravityChart.Series["Raw Beam"].Enabled = true;
            this.GravityChart.Series["Total Correction"].Enabled = true;
        }
    }
}