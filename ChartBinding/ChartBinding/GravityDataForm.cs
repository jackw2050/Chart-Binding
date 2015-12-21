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
    public partial class GravityDataForm : Form
    {
        public GravityDataForm()
        {
            InitializeComponent();
        }

        private void hideFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
