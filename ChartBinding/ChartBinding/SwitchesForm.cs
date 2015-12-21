using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}
