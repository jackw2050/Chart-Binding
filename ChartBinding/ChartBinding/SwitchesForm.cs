using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace ChartBinding
{
    public partial class SwitchesForm : Form
    {
        public SwitchesForm()
        {
            InitializeComponent();
        }

        private void doneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void SwitchesForm_Load(object sender, EventArgs e)
        {
            gyroLabel.Visible = false;
        }

        private void switchesGyroCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (switchesGyroCheckBox.Checked == true)
            {
                gyroLabel.Visible = true;
                //Call to gyro startup function here
                Thread.Sleep(100);
                gyroLabel.Visible = false;
                SwitchesTorqueMotorsCheckBox.Enabled = true;
                
            }
        }

        private void SwitchesTorqueMotorsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SwitchesTorqueMotorsCheckBox.Checked == true)
            {
                // call to torqu motor start function here
            }
        }
    }
}
