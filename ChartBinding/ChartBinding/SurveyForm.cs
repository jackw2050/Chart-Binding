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
    public partial class SurveyForm : Form
    {
        public SurveyForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.surveyName = surveyTextBox.Text;
            this.Hide();
        }
    }
}
