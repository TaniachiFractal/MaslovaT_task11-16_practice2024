using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaslovaT_task12_practice2024
{
    /// <summary>
    /// Hold the digit and whether the element is locked
    /// </summary>
    internal class SudokuElement
    {
        const byte EMPTYCELL = 0;

        /// <summary>
        /// The digit sudoku element has
        /// </summary>
        public byte digit;
        /// <summary>
        /// Whether the elemnt is from an unsolved state and should not be editable
        /// </summary>
        public bool locked;

        /// <summary>
        /// Base constructor
        /// </summary>
        public SudokuElement()
        {
            digit = EMPTYCELL;
            locked = false;
        }

        /// <summary>
        /// Data setup constructor
        /// </summary>
        public SudokuElement(byte digit, bool locked)
        {
            this.digit = digit;
            this.locked = locked;
        }

        /// <summary>
        /// Returns digit as a string or nothing if the digit is 0
        /// </summary>
        public override string ToString() 
        {
            if (digit == EMPTYCELL) { return string.Empty; }
            return digit.ToString();
        }
    }
}
