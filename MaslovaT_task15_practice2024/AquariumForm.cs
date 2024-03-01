using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

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
        static readonly Random rnd = new Random();
        #endregion

        #region vars

        Fish fish;

        #endregion

        #endregion

        #region Form events

        public AquariumForm()
        {
            InitializeComponent();
        }

        private void AquariumForm_Load(object sender, EventArgs e)
        {
            fish = new Fish(110,110,10,carpR,carpL,Screen.PrimaryScreen.Bounds.Height);
            this.Controls.Add(fish.fishPB);
            mainTimer.Enabled = true;
        }

        /// <summary>
        /// Move fish
        /// </summary>
        private void MainTimer_Tick(object sender, EventArgs e)
        {
            fish.Swim(this.Width, this.Height,rnd);
            this.Text = fish.ToString();
        }

        #endregion

        #region Working with data


        #endregion

    }
}
