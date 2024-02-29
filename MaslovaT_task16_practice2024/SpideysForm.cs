using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MaslovaT_task16_practice2024
{
    public partial class SpideysForm : Form
    {
        public SpideysForm()
        {
            InitializeComponent();
        }

        #region data

        #region consts

        /// <summary>
        /// Dimens of the spider.png
        /// </summary>
        const byte spiderHeight = 26, spiderWidth = 32;

        #endregion

        #region vars

        /// <summary>
        /// The amount of spiders
        /// </summary>
        static int SpiderCount;

        /// <summary>
        /// The distance between 2 spiders at which the web starts drawing
        /// </summary>
        static int maxSpiderwebLength;

        /// <summary>
        /// Main randomizer
        /// </summary>
        Random rnd = new Random();

        /// <summary>
        /// The field to draw spiderwebs on
        /// </summary>
        Graphics FLDgr;
        /// <summary>
        /// Data of a spiderwebPN stroke
        /// </summary>
        Pen spiderwebPN;
        /// <summary>
        /// How to erase the field
        /// </summary>
        Brush eraserBR;

        #endregion

        #region arrays

        /// <summary>
        /// Main data array
        /// </summary>
        static List<Spider> spiders = new List<Spider>();
        /// <summary>
        /// PictureBox array
        /// </summary>
        static List<PictureBox> pictureboxes = new List<PictureBox>();

        #endregion

        #endregion

        #region Form events

        private void SpideysForm_Load(object sender, EventArgs e)
        {
            // SpiderCount = Screen.PrimaryScreen.Bounds.Height / 40;
            SpiderCount = 2;

            InitPictureBoxArray();
            GenerateSpiderData(this.Width, this.Height);
            RenderSpiders();
            InitGraphics();

            mainTimer.Enabled = true;
        }

        /// <summary>
        /// Reset spiders speed
        /// </summary>
        private void SpideysForm_ResizeEnd(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int Xmax = this.Width, Ymax = this.Height;
            for (int i = 0; i < SpiderCount; i++)
            {
                int newSpeedX = rnd.Next(Xmax / 400, Xmax / 200) + 1, newSpeedY = rnd.Next(Ymax / 400, Ymax / 200) + 1;
                spiders[i].ChangeSpeed(OneOrMinusone() * newSpeedX, OneOrMinusone() * newSpeedY);
            }
        }

        /// <summary>
        /// Main cycle
        /// </summary>
        private void MainTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < SpiderCount; i++)
            {
                spiders[i].Move(this.Height, this.Width, rnd);
            }
            RenderSpiders();
        }

        #endregion

        #region working with data

        /// <summary>
        /// Initialize spider array
        /// </summary>
        private void GenerateSpiderData(int Xmax, int Ymax)
        {
            Xmax -= 20; Ymax -= 20;
            for (int i = 0; i < SpiderCount; i++)
            {
                int newSpeedX = rnd.Next(Xmax / 400, Xmax / 200) + 1, newSpeedY = rnd.Next(Ymax / 400, Ymax / 200) + 1;
                spiders.Add(new Spider(rnd.Next(Xmax), rnd.Next(Ymax),
                                       OneOrMinusone() * newSpeedX, OneOrMinusone() * newSpeedY,
                                       spiderHeight, spiderWidth));
            }
        }

        /// <summary>
        /// Randomly return 1 or -1
        /// </summary>
        private int OneOrMinusone()
        {
            if (rnd.NextDouble() > 0.65) return 1;
            return -1;
        }

        #endregion

        #region Render

        /// <summary>
        /// Generate the pictureboxes
        /// </summary>
        private void InitPictureBoxArray()
        {
            for (int i = 0; i < SpiderCount; i++)
            {
                pictureboxes.Add(new PictureBox());
                var currPB = pictureboxes[i];

                currPB.Name = "pb" + i;
                currPB.BackColor = Color.Transparent;
                currPB.Image = Properties.Resources.spider;
                currPB.SizeMode = PictureBoxSizeMode.StretchImage;
                currPB.Size = new Size(spiderWidth, spiderHeight);
                currPB.Location = new Point(i * spiderWidth, i * spiderHeight);               

                this.Controls.Add(currPB);
            }
        }

        /// <summary>
        /// Read data array and draw spiders
        /// </summary>
        private void RenderSpiders()
        {
            for (int i = 0; i < SpiderCount; i++)
            {
                pictureboxes[i].Left = spiders[i].GetX();
                pictureboxes[i].Top = spiders[i].GetY();
            }
        }

        /// <summary>
        /// Initialize the graphics for drawing spiderwebs
        /// </summary>
        private void InitGraphics()
        {
            FLDgr = this.CreateGraphics();
            spiderwebPN = new Pen(Color.Lavender, 2);
            eraserBR = new SolidBrush(this.BackColor);
            FLDgr.Clear(SystemColors.Control);
        }

        #endregion
    }
}
