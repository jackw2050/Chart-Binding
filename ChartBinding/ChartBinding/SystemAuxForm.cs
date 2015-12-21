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
    public partial class SystemAuxForm : Form
    {
        public SystemAuxForm()
        {
            InitializeComponent();
        }

        private void hideFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
