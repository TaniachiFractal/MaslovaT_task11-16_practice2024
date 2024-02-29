using System;
using System.Drawing;

namespace MaslovaT_task16_practice2024
{
    /// <summary>
    /// The object of a spider
    /// </summary>
    internal class Spider
    {
        /// <summary>
        /// Location of a spider
        /// </summary>
        int X, Y;
        /// <summary>
        /// Dimens of a spider
        /// </summary>
        int Width, Height;
        /// <summary>
        /// Speed in 2 directions
        /// </summary>
        int SpeedX, SpeedY;

        /// <summary>
        /// Base constructor
        /// </summary>
        public Spider(int x, int y, int speedX, int speedY, int height, int width)
        {
            X = x;
            Y = y;
            SpeedX = speedX;
            SpeedY = speedY;
            Height = height;
            Width = width;
        }

        /// <summary>
        /// Bounce the spider in ping-pong fashion
        /// </summary>
        public void Move(int fieldHeight, int fieldWidth)
        {
            X += SpeedX;
            Y += SpeedY;
            if ((X + Width + 2) > fieldWidth)
            {
                SpeedX = -SpeedX;
            }
            else if ((Y + Height + 30) > fieldHeight)
            {
                SpeedY *= -1;
            }
            else if ((X - 2) < -4)
            {
                SpeedX *= -1;
            }
            else if ((Y - 2) < -2)
            {
                SpeedY *= -1;
            }
        }

        /// <summary>
        /// Adjust position upon window resize
        /// </summary>
        public void AdjustLocation(int oldFldHei, int oldFldWid, int newFldHei, int newFldWid)
        {
            double percentX = (double)X / oldFldWid;
            double percentY = (double)Y / oldFldHei;

            X = (int)(percentX * newFldWid);
            Y = (int)(percentY * newFldHei);

            if (X > newFldWid) { X = newFldWid - Width; }
            if (Y > newFldHei) { Y = newFldHei - Height; }
            if (X < 0) { X = 0; }
            if (Y < 0) { Y = 0; }
        }

        /// <summary>
        /// True if spider flies away
        /// </summary>
        public bool Lost(int screenHeight, int screenWidth)
        {
            if (X > screenWidth + Width) return true;
            if (Y > screenHeight + Width) return true;
            if (X < 0 - Width) return true;
            if (Y < 0 - Width) return true;
            return false;
        }

        /// <summary>
        /// Make spiders go faster/slower upon window resize
        /// </summary>
        public void ChangeSpeed(int speedX, int speedY)
        {
            SpeedX = speedX;
            SpeedY = speedY;
        }

        /// <summary>
        /// Get distance to another spider
        /// </summary>
        public double DistanceToFriend(Spider friend)
        {
            int myMiddle_X = X + Width / 2;
            int myMiddle_Y = Y + Height / 2;
            int friendMiddle_X = friend.X + friend.Width / 2;
            int friendMiddle_Y = friend.Y + friend.Height / 2;
            return Math.Sqrt(

                   Math.Pow(
                       myMiddle_X - friendMiddle_X
                       , 2)

                   +

                   Math.Pow(
                       myMiddle_Y - friendMiddle_Y
                       , 2)

                   );
        }

        /// <summary>
        /// Get X location of the spider
        /// </summary>
        public int GetX() { return X; }
        /// <summary>
        /// Get Y location of the spider
        /// </summary>
        public int GetY() { return Y; }
        /// <summary>
        /// Return location as a point
        /// </summary>
        public Point GetMiddle() { return new Point(X + Width / 2, Y + Height / 2); }
    }
}
