using System;
using System.Windows.Forms;

namespace ChartBinding
{
    public partial class FileFormatForm : Form
    {

        Form1 mainForm = new Form1();
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
                Form1.fileType = "csv";
                UpdateFileNameText();
            }
        }

        private void tabRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (tabRadioButton.Checked == true)
            {
                Form1.fileType = "tsv";
                UpdateFileNameText();
            }
        }

        private void FileFormatForm_Load(object sender, EventArgs e)
        {
            this.CustomNameTextBox.Visible = false;
            DateTime myDateTime = DateTime.Now;
            dateFormatRadioButton1.Checked = true;
            commaRadioButton.Checked = true;
            if (Form1.meterNumber == "")
            {
                Form1.meterNumber = "Unknown meter"; 
            }
            if (Form1.surveyName == "")
            {
                Form1.surveyName = "Survey";
            }


        }

        private void customFileNameRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.customFileNameRadioButton.Checked)
            {
                this.CustomNameTextBox.Visible = true;
                Form1.fileDateFormat = 2;

            }
        }



        private void dateFormatRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (dateFormatRadioButton1.Checked == true)
            {
                Form1.fileDateFormat = 1;
                UpdateFileNameText();
                //open text file to dump data


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
            DateTime now = DateTime.Now;
            if (Form1.fileDateFormat == 1)
            {
                sampleFileNamelabel.Text = Form1.meterNumber + " " + Form1.surveyName + " " + now.ToString("yyyy-MMM-dd-HH-mm-ss") + "." + Form1.fileType;
            }
            else if (Form1.fileDateFormat == 2)
            {
                sampleFileNamelabel.Text = Form1.meterNumber + " " + Form1.surveyName + " " + now.ToString("yyyy-mm-dd-HH-mm-ss") + "." + Form1.fileType;
            }
            else if (Form1.fileDateFormat == 3)
            {
                sampleFileNamelabel.Text = Form1.meterNumber + " " + Form1.surveyName + " " + now.ToString("yyyy-dd-HH-mm-ss") + "." + Form1.fileType;
                
            }
            else if (Form1.fileDateFormat == 4)
            {
              sampleFileNamelabel.Text = CustomNameTextBox.Text + "." + Form1.fileType;
            }

            
        }



        private void CustomNameTextBox_TextChanged(object sender, EventArgs e)
        {

                sampleFileNamelabel.Text = Form1.surveyName + CustomNameTextBox.Text + Form1.fileType;
          
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            FileClass FileClass = new FileClass();
            Form1.gravityFileName = sampleFileNamelabel.Text;
            FileClass.RecordDataToFile("Open");
            this.Hide();
        }
    }
}