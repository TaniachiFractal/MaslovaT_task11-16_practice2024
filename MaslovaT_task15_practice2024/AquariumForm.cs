using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace MaslovaT_task15_practice2024
{
    public partial class AquariumForm : Form
    {

        #region data

        #region consts
        static readonly Bitmap carpL = Properties.Resources.carp_left;
        static readonly Bitmap carpR = Properties.Resources.carp_right;
        static readonly Bitmap pikeL = Properties.Resources.pike_left;
        static readonly Bitmap pikeR = Properties.Resources.pike_right;
        #endregion

        #endregion

        #region Form events

        public AquariumForm()
        {
            InitializeComponent();
        }

        private void AquariumForm_Load(object sender, EventArgs e)
        {
            Fish fish = new(0, 0, 10, carpR, carpL, Screen.PrimaryScreen.Bounds.Height);
            this.Controls.Add(fish.fishPB);

        }

        #endregion
    }
}
