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
    public partial class PasswordForm : Form
    {
        public PasswordForm()
        {
            InitializeComponent();
            passWordWrongLabel.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string passWord = "zlseng";
            if (passWordTextBox.Text ==  passWord)
            {
                this.Hide();
                EngineeringForm EngineeringForm = new EngineeringForm();
                EngineeringForm.Show();
            }
            else
            {
                passWordWrongLabel.Visible = true;
                passWordTextBox.Text = null;
            }

          //  Please enter password to access this form
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
