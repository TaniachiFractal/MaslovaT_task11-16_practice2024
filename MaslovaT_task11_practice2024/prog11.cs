using System;

namespace MaslovaT_task11_practice2024
{
    internal class Program
    {
        /// <summary>
        /// Main array
        /// </summary>
        static byte[,] sudoku = new byte[9, 9];

        static void Main()
        {
            FillSudoku(sudoku);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(GetFormattedSudoku(sudoku));

            Console.ReadKey();
        }

        #region done

        /// <summary>
        /// Randomly returns any digit 0 - 8
        /// </summary>
        /// <returns></returns>
        static byte GenerateDigit()
        {
            Random rnd = new Random();
            return (byte)rnd.Next(1, 10);
        }

        /// <summary>
        /// Randomly fill 1 cell
        /// </summary>
        static void FillCell(byte[,] sudoku, byte startRow, byte startCol)
        {
            for (byte i = startRow; i < startRow + 3; i++)
            {
                for (byte j = startCol; j < startCol + 3; j++)
                {
                ReGen:
                    byte tmpDigit = GenerateDigit();
                    if (DigitAlreadyIn_Cell(sudoku, i, j, tmpDigit)) goto ReGen;
                    sudoku[i, j] = tmpDigit;
                }
            }
        }

        /// <summary>
        /// Get formatted sudoku string
        /// </summary>
        static string GetFormattedSudoku(byte[,] sudoku)
        {
            string output = String.Empty;

            string GetHorizontalDivider()
            {
                string outDiv = string.Empty;
                outDiv += (" ┼");
                for (byte j = 0; j < 18; j++)
                {
                    outDiv += ('─');
                    if (j % 6 == 5)
                        outDiv += ('┼');
                }
                outDiv += Environment.NewLine;
                return outDiv;
            }

            output += Environment.NewLine;
            output += GetHorizontalDivider();
            for (byte i = 0; i < 9; i++)
            {
                output += (" │");
                for (byte j = 0; j < 9; j++)
                {
                    output += (sudoku[i, j].ToString() + ' ');
                    if (j % 3 == 2)
                        output += ('│');
                }
                output += Environment.NewLine;
                if (i % 3 == 2)
                {
                    output += GetHorizontalDivider();
                }
            }
            return output;
        }

        static void SetRandomConsoleColor()
        {
            Random rnd = new();
            int possibleColor = rnd.Next(1, 14);
            if (possibleColor == (int)ConsoleColor.Cyan || Console.ForegroundColor == (ConsoleColor)possibleColor)
            {
                possibleColor++;
            }
            Console.ForegroundColor = (ConsoleColor)possibleColor;
        }

        #endregion

        #region Check

        /// <summary>
        /// Returns the start row/cell of a cell by any digit coords in it: (5,2)->(0,3)  (1,4)->(3,0)
        /// </summary>
        static byte StartOfCell(byte row_cell)
        {
            return (byte)(row_cell - row_cell % 3);
        }

        /// <summary>
        /// True if the newly generated digit is already in the row
        /// </summary>
        static bool DigitAlreadyIn_Row(byte[,] sudoku, byte row, byte digit)
        {
            for (byte i = 0; i < 9; i++)
            {
                if (sudoku[row, i] == digit)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// True if the newly generated digit is already in the col
        /// </summary>
        static bool DigitAlreadyIn_Col(byte[,] sudoku, byte col, byte digit)
        {
            for (byte i = 0; i < 9; i++)
            {
                if (sudoku[i, col] == digit)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// True if the newly generated digit is already in the cell
        /// </summary>
        static bool DigitAlreadyIn_Cell(byte[,] sudoku, byte row, byte col, byte digit)
        {
            byte startCol = StartOfCell(col);
            byte startRow = StartOfCell(row);

            for (byte i = startRow; i < startRow + 3; i++)
            {
                for (byte j = startCol; j < startCol + 3; j++)
                {
                    if (sudoku[i, j] == digit)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// True if the newly generated digit is already in the row, col or cell
        /// </summary>
        static bool DigitIsWrong(byte[,] sudoku, byte row, byte col, byte digit)
        {
            return DigitAlreadyIn_Cell(sudoku, row, col, digit) || DigitAlreadyIn_Col(sudoku, col, digit) || DigitAlreadyIn_Row(sudoku, row, digit);
        }

        /// <summary>
        /// True if coord is in cell
        /// </summary>
        static bool InCell(byte[,] sudoku, byte startRow, byte startCol, byte row, byte col)
        {
            return startCol == StartOfCell(col) && startRow == StartOfCell(row);
        }

        #endregion

        /// <summary>
        /// Fill sudoku with numbers
        /// </summary>
        static void FillSudoku(byte[,] sudoku)
        {
            #region fill three cells in the main diagonal
            FillCell(sudoku, 0, 0);
            FillCell(sudoku, 3, 3);
            FillCell(sudoku, 6, 6);
            #endregion

            byte i = 0;
            while (i < 9)
            {
                byte j = 0;
            Redo:
                while (j < 9)
                {
                    if (InCell(sudoku, 0, 0, i, j) || InCell(sudoku, 3, 3, i, j) || InCell(sudoku, 6, 6, i, j))
                    {
                        j++;
                        continue;
                    }

                    byte oldDigit = sudoku[i, j];
                    byte newDigit = oldDigit;

                ReDigit:
                    newDigit++;
                    if (!DigitIsWrong(sudoku, i, j, newDigit))
                    {
                        sudoku[i, j] = newDigit;
                    }
                    else if (newDigit < 9)
                    {
                        goto ReDigit;
                    }

                    if (newDigit > 9)
                    {
                        SetRandomConsoleColor();
                        i = 0; j = 3; goto Redo;
                    }
                    
                    if (sudoku[i, j] == 0)
                    {
                        SetRandomConsoleColor();
                        i = 0; j = 3; goto Redo;
                    }

                    Console.WriteLine(GetFormattedSudoku(sudoku));
                    j++;
                }
                i++;
            }
        }

    }
}
