using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaslovaT_task11_practice2024
{
    /// <summary>
    /// Coord of a sudoku digit
    /// </summary>
    class BytePoint
    {
        public byte row;
        public byte col;

        public BytePoint()
        {
            row = 0; col = 0;
        }
        public BytePoint(byte row_, byte col_)
        {
            row = row_; col = col_;
        }
    }
}
