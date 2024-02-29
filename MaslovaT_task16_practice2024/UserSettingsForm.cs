using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaslovaT_task16_practice2024
{
    public partial class UserSettingsForm : Form
    {
        public UserSettingsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load the web length and spider count
        /// </summary>
        private void UserSettingsForm_Load(object sender, EventArgs e)
        {
            webNUD.Value = SpideysForm.maxSpiderwebLength;
        }

        /// <summary>
        /// Change web length
        /// </summary>
        private void webNUD_ValueChanged(object sender, EventArgs e)
        {
            SpideysForm.maxSpiderwebLength = (int)webNUD.Value;
        }

    }
}
