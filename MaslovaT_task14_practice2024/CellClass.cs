using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaslovaT_task14_practice2024
{
    internal class Cell
    {
        /// <summary>
        /// Normal, immune or infected
        /// </summary>
        public byte state;
        public byte timeLeftImmune;
        public byte timeLeftInfected;

        /// <summary>
        /// Base constructor
        /// </summary>
        public Cell(byte state_) 
        {
            state = state_;
            timeLeftImmune = 0;
            timeLeftInfected = 0;
        }
    }
}
