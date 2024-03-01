
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MaslovaT_task15_practice2024
{
    /// <summary>
    /// Fish swims around in an aquarium
    /// </summary>
    class Fish
    {
        #region data

        int X_, Y_;
        /// <summary>
        /// Horizontal coord update
        /// </summary>
        public int X
        {
            get => X_;
            set
            {
                X_ = value;
                fishPB.Left = X_;
            }
        }
        /// <summary>
        /// Vertical coord update
        /// </summary>
        public int Y
        {
            get => Y_; 
            set
            {
                Y_ = value;
                fishPB.Top = Y_;
            }
        }

        /// <summary>
        /// Pixels per tick 
        /// </summary>
        int speed;
        /// <summary>
        /// The point fish wants to get to
        /// </summary>
        Point destination;
        /// <summary>
        /// The picture box that represents the fish
        /// </summary>
        public PictureBox fishPB;
        /// <summary>
        /// The image that represents the fish
        /// </summary>
        Bitmap spriteL, spriteR;

        #endregion

        /// <summary>
        /// Base constructor
        /// </summary>
        public Fish(int x, int y, int speed_, Bitmap Rsprite, Bitmap Lsprite, int screenHeight)
        {
            X_ = x; Y_ = y; speed = speed_;
            destination = new Point(0, 0);
            spriteL = Lsprite;
            spriteR = Rsprite;

            fishPB = new PictureBox();
            fishPB.Image = spriteL;
            fishPB.SizeMode = PictureBoxSizeMode.StretchImage;

            double abstractPixelSize = screenHeight / 3000.0;
            fishPB.Width = (int)(spriteL.Width * abstractPixelSize);
            fishPB.Height = (int)(spriteL.Height * abstractPixelSize);
        }

        /// <summary>
        /// Sets new random destination more than 3 speeds away
        /// </summary>
        public void SetNewDestination(int maxX, int maxY, Random rnd)
        {
            Point newDestination = new(0, 0);
            while (Distance(destination, newDestination) < speed * 3)
            {
                newDestination = new Point(rnd.Next(maxX), rnd.Next(maxY));
            }
            destination = newDestination;
        }

        /// <summary>
        /// Move fish in its direction and maybe change it
        /// </summary>
        public void Swim(Random rnd)
        {

        }

        /// <summary>
        /// Flip spriteL to the horizontal swimming direction
        /// </summary>
        public void UpdateSprite()
        {
            if (destination.Y < Y_) fishPB.Image = spriteL;
            else fishPB.Image = spriteR;
        }
        
        /// <summary>
        /// Distance between 2 points
        /// </summary>
        double Distance(Point point1, Point point2)
        {
            return Math.Sqrt(
                     (point1.X - point2.X) * (point1.X - point2.X)
                   +
                     (point1.Y - point2.Y) * (point1.Y - point2.Y)
                            );
        }
    }

}
