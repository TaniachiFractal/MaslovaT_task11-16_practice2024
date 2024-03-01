
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

        #region coords

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
        /// Coords
        /// </summary>
        public Point GetPoint() { return new Point(X_, Y_); }

        #endregion

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
            destination = new Point(X_ + 10, Y_ + 10);

            spriteL = Lsprite;
            spriteR = Rsprite;

            fishPB = new PictureBox();
            fishPB.Image = spriteL;
            fishPB.SizeMode = PictureBoxSizeMode.StretchImage;

            double abstractPixelSize = screenHeight / 3000.0;
            fishPB.Width = (int)(spriteL.Width * abstractPixelSize);
            fishPB.Height = (int)(spriteL.Height * abstractPixelSize);
        }

        public override string ToString()
        {
            string output = X_ + " " + Y_ + " " + " " + destination.X + " " +
                destination.Y + " "+ Distance(GetPoint(), destination);
            return output;
        }

        /// <summary>
        /// Sets new random destination more than 50 speeds away
        /// </summary>
        public void SetNewDestination(int maxX, int maxY, Random rnd)
        {
            Point newDestination = new(0, 0);
            do
            {
                newDestination = new Point(rnd.Next(maxX), rnd.Next(maxY));
            }
            while (Distance(destination, newDestination) < speed * 50);
 
            if (newDestination.X > maxX) { newDestination.X = maxX; }
            if (newDestination.Y > maxY) { newDestination.Y = maxY; }
            if (newDestination.X < 0) { newDestination.X = 0; }
            if (newDestination.Y < 0) { newDestination.Y = 0; }
            destination = newDestination;
        }

        /// <summary>
        /// Move fish in its direction and maybe change it
        /// </summary>
        public void Swim(int maxX, int maxY, Random rnd)
        {
            int finalDeltaX = X - destination.X;
            int finalDeltaY = Y - destination.Y;

            if (destination.X == 0) { destination.X = 10;}
            if (destination.Y == 0) { destination.Y = 10;}
            double distance = Distance(GetPoint(), destination);

            if (distance < 0.1)
            {
                SetNewDestination(maxX, maxY, rnd);
                distance = Distance(GetPoint(), destination);
            }
            double speedX = (finalDeltaX * (speed / distance));
            double speedY = (finalDeltaY * (speed / distance));

            X = (int)(X - speedX);
            Y = (int)(Y - speedY);
            UpdateSprite();
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
            
            double output = Math.Sqrt(
                     (point1.X - point2.X) * (point1.X - point2.X)
                   +
                     (point1.Y - point2.Y) * (point1.Y - point2.Y)
                            );
            return output;
  
        }
    }

}
