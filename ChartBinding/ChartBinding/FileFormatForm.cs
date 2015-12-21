using System;
using System.Windows.Forms;

namespace ChartBinding
{
    public partial class FileFormatForm : Form
    {
        public FileFormatForm()
        {
            InitializeComponent();
        }

        private void textRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (textRadioButton.Checked == true)
            {
                Form1.fileType = "txt";
                UpdateFileNameText();
            }
        }

        private void commaRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (commaRadioButton.Checked == true)
            {
                Form1.fileType = "cvs";
                UpdateFileNameText();
            }
        }

        private void tabRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (tabRadioButton.Checked == true)
            {
                Form1.fileType = "tvs";
                UpdateFileNameText();
            }
        }

        private void FileFormatForm_Load(object sender, EventArgs e)
        {
            this.CustomNameTextBox.Visible = false;
            DateTime myDateTime = DateTime.Now;


        }

        private void customFileNameRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.customFileNameRadioButton.Checked)
            {
                this.CustomNameTextBox.Visible = true;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void dateFormatRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (dateFormatRadioButton1.Checked == true)
            {
                Form1.fileDateFormat = 1;
                UpdateFileNameText();
            }

        }

        private void dateFormatRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (dateFormatRadioButton2.Checked == true)
            {
                Form1.fileDateFormat = 2;
                UpdateFileNameText();
            }

        }


        private void dateFormatRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (dateFormatRadioButton3.Checked == true)
            {
                Form1.fileDateFormat = 3;
                UpdateFileNameText();
            }

        }

        private void UpdateFileNameText()
        {
            
            if (Form1.fileDateFormat == 1)
            {
                sampleFileNamelabel.Text = Form1.surveyName + " 2015-Jan-1 15-23-34." + Form1.fileType;
            }
            else if (Form1.fileDateFormat == 2)
            {
                sampleFileNamelabel.Text = Form1.surveyName + " 2015-1-1 15-23-34." + Form1.fileType;
            }
            else if (Form1.fileDateFormat == 3)
            {
                sampleFileNamelabel.Text = Form1.surveyName + " 2015-1 15-23-34." + Form1.fileType;
                
            }

            
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void CustomNameTextBox_TextChanged(object sender, EventArgs e)
        {

                sampleFileNamelabel.Text = Form1.surveyName + CustomNameTextBox.Text + Form1.fileType;
          
        }
    }
}