﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaslovaT_task12_practice2024
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Close help form
        /// </summary>
        private void btOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
