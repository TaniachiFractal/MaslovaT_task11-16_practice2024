using System;

namespace MaslovaT_task16_practice2024
{
    /// <summary>
    /// The object of a spider
    /// </summary>
    internal class Spider
    {
        /// <summary>
        /// Location of the spider
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
        public void Move(int fieldHeight, int fieldWidth, Random rnd)
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
        /// Make spiders go faster/slower upon window resize
        /// </summary>
        public void ChangeSpeed(int speedX, int speedY)
        {
            SpeedX = speedX;
            SpeedY = speedY;
        }

        /// <summary>
        /// Get X location of the spider
        /// </summary>
        public int GetX() { return X; }
        /// <summary>
        /// Get Y location of the spider
        /// </summary>
        public int GetY() { return Y; }
    }
}
