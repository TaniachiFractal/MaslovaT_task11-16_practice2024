using System;
using System.Windows.Forms;

namespace MaslovaT_task13_practice2024
{
    public partial class YouLostForm : Form
    {
        public YouLostForm()
        {
            InitializeComponent();
        }

        private void BtRestart_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Restart();
        }
    }
}
