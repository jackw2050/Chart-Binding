﻿using System;
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
    public partial class EngineeringForm : Form
    {
        public EngineeringForm()
        {
            InitializeComponent();
        }

        private void EngineeringForm_Load(object sender, EventArgs e)
        {
            longDampingFactorTextBox.Text = Convert.ToString( ConfigData.longDampFactor);
        }
    }
}
