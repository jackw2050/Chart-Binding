using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ChartBinding
{
    public partial class DateTimeForm : Form
    {
        public DateTimeForm()
        {
            InitializeComponent();
        }
        public struct SYSTEMTIME
        {
            public ushort wYear, wMonth, wDayOfWeek, wDay, wHour, wMinute, wSecond, wMilliseconds;
        }

        [DllImport("kernel32.dll")]
        public extern static void GetSystemTime(ref SYSTEMTIME lpSystemTime);

        /// <param name="lpSystemTime">[in] Pointer to a SYSTEMTIME structure that
        /// contains the current system date and time.</param>
        [DllImport("kernel32.dll")]
        public extern static uint SetSystemTime(ref SYSTEMTIME lpSystemTime);


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/dd/yyyy     HH:mm:ss";



            dayOfYearLabel.Text = "Day  " + dateTimePicker1.Value.DayOfYear.ToString();

            var d = dateTimePicker1.Value.DayOfYear;
            Console.WriteLine(d);


            DateTime iDate;
            iDate = dateTimePicker1.Value;
            DateTime tempDateTime = iDate;

            //              add code here to set embedded processor date/ time


        }

        private void DateTimeForm_Load(object sender, EventArgs e)
        {
            setDateTimeSuccessLabel.Visible = false;
        }

        private void setDateTimeButton_Click(object sender, EventArgs e)
        {
            setDateTimeSuccessLabel.Text = "Set time successful";
            setDateTimeSuccessLabel.Visible = true;
        }
    }
}
